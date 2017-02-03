using System.Collections.Generic;
using Squares.Contracts;

namespace Squares.Storage.Client
{
    public interface IStorage<T>
        where T : Listable , new ()
    {
        IList<string> RetrieveListNames();
        IList<T> RetrieveItems(string listName, int pageSize, int pageNumber);
        int RetrieveItemsCount(string listName);
        bool RemoveList(string listName);
        bool CreateList(string listName);
        bool AddToList(IList<T> points, string listName);
        int RemoveFromList(IList<T> points, string listName);
    }
}