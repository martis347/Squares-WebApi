
using System;

namespace Squares.Contracts
{
    public interface IListable : IComparable
    {
    }

    public abstract class Listable : IListable
    {
        public Listable(string line)
        {
        }

        public Listable()
        {
        }

        public abstract int CompareTo(object obj);
    }
}