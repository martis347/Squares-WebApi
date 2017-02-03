
namespace Squares.Contracts
{
    public interface IListable
    {
        bool IsGreaterThan(string line);
    }

    public class Listable : IListable
    {
        public Listable(string line)
        {
        }

        public Listable()
        {
        }

        public virtual bool IsGreaterThan(string line)
        {
            return true;
        }
    }
}