using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using Squares.Contracts;
using Squares.Contracts.Exceptions;

namespace Squares.Storage.Client
{
    public class FileStorage<T> : IStorage<T>
        where T : Listable, new()
    {
        private volatile IDictionary<string, FileInfo> _fileLocks;
        private readonly string _fileLocation;
        private readonly int _fileMaxRows;

        public FileStorage(string fileLocation)
        {
            if (fileLocation == null)
                throw new ArgumentNullException(nameof(fileLocation));

            _fileLocation = fileLocation;

            Int32.TryParse(ConfigurationManager.AppSettings["FileMaxRows"], out _fileMaxRows);
            if (_fileMaxRows == 0)
                throw new Exception("FileMaxRows not defined in appSettings");

            _fileLocks = ReadExistingFiles();
        }

        public IList<T> RetrieveItems(string listName, int pageSize, int pageNumber)
        {
            if (!_fileLocks.ContainsKey(listName))
            {
                throw new FileStorageException("List with given name does not exist.", "listNotFound");
            }

            var result = new List<T>();
            int startingLineNumber = pageSize * (pageNumber - 1);

            lock (_fileLocks[listName])
            {
                using (Stream fs = new FileStream($"{_fileLocation}{listName}", FileMode.Open, FileAccess.Read, FileShare.None))
                using (StreamReader sr = new StreamReader(fs))
                {
                    for (int i = 0; i < startingLineNumber; i++)
                    {
                        sr.ReadLine();
                    }
                    for (int i = 0; i < pageSize; i++)
                    {
                        string line = sr.ReadLine();
                        if (line != null)
                        {
                            result.Add(Activator.CreateInstance(typeof(T), line) as T);
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
                }
            }

            return true;
        }

        public bool AddToList(IList<T> items, string listName)
        {
            if (!_fileLocks.ContainsKey(listName))
            {
                throw new FileStorageException("List with given name does not exist.", "listNotFound");
            }

            lock (_fileLocks[listName])
            {
                var lines = File.ReadAllLines($"{_fileLocation}{listName}");
                var itemLines = items.Select(item => $"{item.ToString()}");
                var existingLines = itemLines.Intersect(lines).ToList();
                        
                if (existingLines.Any())
                {
                    throw new FileStorageException($"Items(s): {existingLines.Aggregate((i, j) =>$@"{{{i}}} {{{j}}}")} already exist in the list", "itemsExist");
                }

                int writtenLinesCount = 0;
                int existingLinesCount = _fileLocks[listName].LinesCount;

                using (Stream fs = new FileStream($"{_fileLocation}{listName}", FileMode.Append, FileAccess.Write, FileShare.None))
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    foreach (var item in items)
                    {
                        sw.WriteLine($"{item}");
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

        public int RemoveFromList(IList<T> items, string listName)
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
                        foreach (Listable item in items)
                        {
                            string itemString = $"{item}";
                            if (line == itemString)
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