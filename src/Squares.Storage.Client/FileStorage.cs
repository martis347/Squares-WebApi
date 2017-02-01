﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using Squares.Contracts.Exceptions;
using Squares.Contracts.Lists;
using Squares.Contracts.Points;

namespace Squares.Storage.Client
{
    public class FileStorage : IStorage
    {
        private static readonly FileStorage FileStorageInstance = new FileStorage();
        private readonly IDictionary<string, FileInfo> _fileLocks = new Dictionary<string, FileInfo>();

        private static string _fileLocation;
        private static int _fileMaxRows;

        private FileStorage()
        {
            _fileLocation = ConfigurationManager.AppSettings["FileLocation"];
            Int32.TryParse(ConfigurationManager.AppSettings["FileMaxRows"], out _fileMaxRows);

            if (_fileLocation == null)
                throw new Exception("FileLocation not defined in appSettings");
            if (_fileMaxRows == 0)
                throw new Exception("FileMaxRows not defined in appSettings");
        }

        public FileStorage GetInstance()
        {
            return FileStorageInstance;
        }

        public PointsList RetrieveList(string listName)
        {
            PointsList result = new PointsList { List = new List<Point>() };

            lock (_fileLocks[listName])
            {
                if (_fileLocks[listName] != null)
                {
                    throw new FileStorageException("List with given name does not exist.", "listNotFound");
                }

                using (Stream fs = new FileStream($"{_fileLocation}{listName}", FileMode.Open, FileAccess.Read, FileShare.None))
                using (StreamReader sr = new StreamReader(fs))
                {
                    string line = sr.ReadLine();
                    result.List.Add(new Point(line));
                }
            }

            return result;
        }

        public bool RemoveList(string listName)
        {
            lock (_fileLocks[listName])
            {
                if (_fileLocks[listName] != null)
                {
                    throw new FileStorageException("List with given name does not exist.", "listNotFound");
                }

                File.Delete($"{_fileLocation}{listName}");
                _fileLocks.Remove(listName);

            }

            return true;
        }

        public bool CreateList(string listName)
        {
            lock (_fileLocks[listName])
            {
                if (_fileLocks[listName] == null)
                {
                    throw new FileStorageException("List with given name does not exist.", "listNotFound");
                }

                File.Create($"{_fileLocation}{listName}");
                _fileLocks.Add(listName, new FileInfo());
            }

            return true;
        }

        public bool AddToList(PointsList points, string listName)
        {
            lock (_fileLocks[listName])
            {
                if (_fileLocks[listName] == null)
                {
                    throw new FileStorageException("List with given name does not exist.", "listNotFound");
                }

                int writtenLinesCount = 0;
                int existingLinesCount = _fileLocks[listName].LinesCount;

                using (Stream fs = new FileStream($"{_fileLocation}{listName}", FileMode.Append, FileAccess.Write, FileShare.None))
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    foreach (var point in points.List)
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

        public int RemoveFromList(PointsList points, string listName)
        {
            List<string> linesToDelete = new List<string>();

            lock (_fileLocks[listName])
            {
                if (_fileLocks[listName] == null)
                {
                    throw new FileStorageException("List with given name does not exist.", "listNotFound");
                }

                using (Stream fsRead = new FileStream($"{_fileLocation}{listName}", FileMode.Open, FileAccess.Read, FileShare.None))
                using (Stream fs2Write = new FileStream($"{_fileLocation}{listName}.temp", FileMode.Create, FileAccess.Write, FileShare.None))
                using (StreamReader sr = new StreamReader(fsRead))
                using (StreamWriter sw = new StreamWriter(fs2Write))
                {
                    string line = sr.ReadLine();
                    foreach (Point point in points.List)
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

                _fileLocks[listName].LinesCount -= linesToDelete.Count;
                File.Delete($"{_fileLocation}{listName}.temp");
            }

            return linesToDelete.Count;
        }
    }
}