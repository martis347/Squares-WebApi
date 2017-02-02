using System.Collections.Generic;
using Squares.Contracts.Points;

namespace Squares.Storage.Client
{
    public interface IStorage
    {
        IList<string> RetrieveListNames();
        IList<Point> RetrieveList(string listName);
        bool RemoveList(string listName);
        bool CreateList(string listName);
        bool AddToList(IList<Point> points, string listName);
        int RemoveFromList(IList<Point> points, string listName);
    }
}