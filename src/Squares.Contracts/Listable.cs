using System;

namespace Squares.Contracts
{
    public interface IListable
    {
        string ToString();
        IListable FormatLine(string line);
    }

    public class Listable : IListable
    {
        private IListable _listableImplementation;

        public Listable(string line)
        {
        }

        public Listable()
        {
        }

        public Listable Test(string v)
        {
            throw new NotImplementedException();
        }

        public IListable FormatLine(string line)
        {
            return _listableImplementation.FormatLine(line);
        }
    }
}