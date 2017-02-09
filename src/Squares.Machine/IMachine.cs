namespace Squares.Machine
{
    public interface IMachine<in TRequest>
    {
        bool CreateList(string listName);
        bool RemoveList(string listName);
        bool AddPoints(TRequest request);
        bool RemovePoints(TRequest request);
    }
}
