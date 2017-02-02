using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using Squares.Contracts.Exceptions;
using Squares.Contracts.Lists;
using Squares.Contracts.Points;

namespace Squares.Storage.Client
{
    public class FileStorage : IStorage
    {
        private volatile IDictionary<string, FileInfo> _fileLocks;

        private static string _fileLocation;
        private static int _fileMaxRows;

        public FileStorage()
        {
            _fileLocation = ConfigurationManager.AppSettings["FileLocation"];
            Int32.TryParse(ConfigurationManager.AppSettings["FileMaxRows"], out _fileMaxRows);

            if (_fileLocation == null)
                throw new Exception("FileLocation not defined in appSettings");
            if (_fileMaxRows == 0)
                throw new Exception("FileMaxRows not defined in appSettings");

            _fileLocks = ReadExistingFiles();
        }

        public IList<Point> RetrieveList(string listName)
        {
            var result = new List<Point>();
            if (!_fileLocks.ContainsKey(listName))
            {
                throw new FileStorageException("List with given name does not exist.", "listNotFound");
            }

            lock (_fileLocks[listName])
            {
                using (Stream fs = new FileStream($"{_fileLocation}{listName}", FileMode.Open, FileAccess.Read, FileShare.None))
                using (StreamReader sr = new StreamReader(fs))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        if (line != null)
                        {
                            result.Add(new Point(line));
                        }
                    }
                }
            }

            return result;
        }

        public IList<string> RetrieveListNames()
        {
            return _fileLocks.Keys.ToList();
        }

        public bool RemoveList(string listName)
        {
            if (!_fileLocks.ContainsKey(listName))
            {
                throw new FileStorageException("List with given name does not exist.", "listNotFound");
            }

            lock (_fileLocks[listName])
            {
                File.Delete($"{_fileLocation}{listName}");
                _fileLocks.Remove(listName);

            }

            return true;
        }

        public bool CreateList(string listName)
        {
            if (_fileLocks.ContainsKey(listName))
            {
                throw new FileStorageException("List with given name already exists.", "listExists");
            }
            _fileLocks.Add(listName, new FileInfo());
            lock (_fileLocks[listName])
            {
                using (File.Create($"{_fileLocation}{listName}"))
                {
                    _fileLocks.Add(listName, new FileInfo());
                }
            }

            return true;
        }

        public bool AddToList(IList<Point> points, string listName)
        {
            if (!_fileLocks.ContainsKey(listName))
            {
                throw new FileStorageException("List with given name does not exist.", "listNotFound");
            }

            lock (_fileLocks[listName])
            {
                var lines = File.ReadAllLines($"{_fileLocation}{listName}");
                var pointLines = points.Select(point => $"{point.X} {point.Y}");
                var existingLines = pointLines.Intersect(lines).ToList();
                        
                if (existingLines.Any())
                {
                    throw new FileStorageException($"Point(s): {existingLines.Aggregate((i, j) =>$@"{{{i}}} {{{j}}}")} already exist in the list", "pointsExist");
                }

                int writtenLinesCount = 0;
                int existingLinesCount = _fileLocks[listName].LinesCount;

                using (Stream fs = new FileStream($"{_fileLocation}{listName}", FileMode.Append, FileAccess.Write, FileShare.None))
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    foreach (var point in points)
                    {
                        sw.WriteLine($"{point.X} {point.Y}");
                        writtenLinesCount++;

                        if (existingLinesCount + writtenLinesCount >= _fileMaxRows)
                        {
                            throw new FileStorageException("List elements count limit exceeded", "listSizeExceeded");
                        }
                    }
                }

                _fileLocks[listName].LinesCount += writtenLinesCount;
            }

            return true;
        }

        public int RemoveFromList(IList<Point> points, string listName)
        {
            if (!_fileLocks.ContainsKey(listName))
            {
                throw new FileStorageException("List with given name does not exist.", "listNotFound");
            }

            List<string> linesToDelete = new List<string>();

            lock (_fileLocks[listName])
            {
                using (Stream fsRead = new FileStream($"{_fileLocation}{listName}", FileMode.Open, FileAccess.Read, FileShare.None))
                using (Stream fs2Write = new FileStream($"{_fileLocation}{listName}.temp", FileMode.Create, FileAccess.Write, FileShare.None))
                using (StreamReader sr = new StreamReader(fsRead))
                using (StreamWriter sw = new StreamWriter(fs2Write))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        foreach (Point point in points)
                        {
                            string pointString = $"{point.X} {point.Y}";
                            if (line == pointString)
                            {
                                linesToDelete.Add(line);
                                break;
                            }
                        }

                        if (!linesToDelete.Contains(line))
                        {
                            sw.WriteLine(line);
                        }
                    }
                }

                _fileLocks[listName].LinesCount -= linesToDelete.Count;
                File.Delete($"{_fileLocation}{listName}");
                File.Move($"{_fileLocation}{listName}.temp", $"{_fileLocation}{listName}");
            }

            return linesToDelete.Count;
        }

        private Dictionary<string, FileInfo> ReadExistingFiles()
        {
            Dictionary<string, FileInfo> result = new Dictionary<string, FileInfo>();

            var files = Directory.GetFiles(_fileLocation, "*").Select(Path.GetFileName);
            foreach (var file in files)
            {
                var lineCount = File.ReadLines($"{_fileLocation}{file}").Count();
                result.Add(file, new FileInfo {LinesCount = lineCount});
            }

            return result;
        }
    }
}