using System.Collections.Generic;
using Squares.Contracts.Lists;

namespace Squares.Storage.Client
{
    public interface IStorage
    {
        List<string> RetrieveListNames();
        PointsList RetrieveList(string listName);
        bool RemoveList(string listName);
        bool CreateList(string listName);
        bool AddToList(PointsList points, string listName);
        int RemoveFromList(PointsList points, string listName);
    }
}