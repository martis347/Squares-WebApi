using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using Squares.Contracts.Lists;

namespace Squares.Storage.Client
{
    public class FileStorage : IStorage
    {
        private static readonly FileStorage FileStorageInstance = new FileStorage();
        private static readonly string FileLocation = ConfigurationManager.AppSettings["FileLocation"];
        private readonly IDictionary<string, object> _fileLocks = new Dictionary<string, object>();

        private FileStorage()
        {
        }

        public FileStorage GetInstance()
        {
            return FileStorageInstance;
        }

        public PointsList RetrieveList(string listName)
        {
            throw new System.NotImplementedException();
        }

        public bool RemoveList(string listName)
        {
            throw new System.NotImplementedException();
        }

        public bool CreateList(string listName)
        {
            if (_fileLocks[listName] == null)
            {
                return false;
            }

            File.Create($"{FileLocation}{listName}");
            _fileLocks.Add(listName, new object());

            return true;
        }

        public bool AddToList(PointsList points)
        {
            throw new System.NotImplementedException();
        }

        public bool RemoveFromList(PointsList points)
        {
            throw new System.NotImplementedException();
        }
    }
}