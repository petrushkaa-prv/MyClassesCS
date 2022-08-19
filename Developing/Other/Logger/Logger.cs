using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Developing.Interfaces;
using Developing.Lists;

namespace Developing.Other;





public class LogFormatter
{
    public Func<string, string> Formatter { get; set; } = input => $"{input}\n";

    public LogFormatter(Func<string, string> formatter)
    {
        Formatter = formatter;
    }

    public string Format(string input) => Formatter(input);

    public string FormatCollection(IEnumerable<string> collection)
    {
        var sb = new StringBuilder();

        foreach (var item in collection)
        {
            sb.Append(Formatter(item));
        }

        return sb.ToString();
    }
}



//public class LoggedCollection<TElement, TCollection> : IMyCollection<TElement>, IOrderedContainer<TElement>
//    where TCollection : IMyCollection<TElement>, IOrderedContainer<TElement>
//{
//    protected LoggedObject<TCollection> _logger;

//    public TCollection Collection => _logger.Data;
//    public SlStack<string> Logs => _logger.Logs;

//    public LoggedCollection(TCollection collection)
//    {
//        _logger = new LoggedObject<TCollection>(collection);
//        _logger.Log("Collection created");
//    }

//    public SlStack<string> Deconstruct()
//        => _logger
//            .Run(c => (c, "Collection deconstructed"))
//            .Logs;

//    public bool IsEmpty
//        => _logger
//            .Run(c => (c, "Checked if the collection was empty"))
//            .Data.IsEmpty;

//    public int Size
//        => _logger
//            .Run(c => (c, $"Checked size of the collection: {c.Size}"))
//            .Data.Size;

//    /// <inheritdoc />
//    public bool Contains(TElement element)
//        => _logger
//            .Run(c => (c, $"Checked for {element} in the collection"))
//            .Data.Contains(element);

//    /// <inheritdoc />
//    public void Clear()
//        => _logger
//            .Run(c => (c, "Cleared collection"))
//            .Data.Clear();


//    /// <inheritdoc />
//    public IEnumerator<TElement> GetEnumerator()
//        => _logger
//            .Run(c => (c, "GetEnumerator invoked"))
//            .Data.GetEnumerator();

//    /// <inheritdoc />
//    IEnumerator IEnumerable.GetEnumerator()
//        => this.GetEnumerator();

//    /// <inheritdoc />
//    public void Push(TElement value)
//        => _logger
//            .Run(c => (c, $"Pushed {value}"))
//            .Data.Push(value);

//    /// <inheritdoc />
//    public void Push(params TElement[] values)
//    {
//        foreach (var value in values)
//            this.Push(value);
//    }

//    /// <inheritdoc />
//    public TElement Peek
//        => _logger
//            .Run(c => (c, $"Peeked {c.Peek}"))
//            .Data.Peek;

//    /// <inheritdoc />
//    public TElement Pop()
//    {
//        var popped = _logger.Data.Pop();
//        _logger.Run(c => (c, $"Popped {popped}"));
//        return popped;
//    }

//    /// <inheritdoc />
//    public override string ToString()
//        => _logger
//            .Run(c => (c, "ToString Invoked"))
//            .ToString();

//    public string ToString(LogFormatter formatter)
//        => _logger
//            .Run(c => (c, "ToString with formatter invoked"))
//            .ToString(formatter);
//}