using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Developing.Interfaces;
using Developing.Lists;

namespace Developing.Other.Logger;

public class LoggedObject<T>
{
    public T Object { get; init; }
    public IOrderedContainer<string> Logs { get; init; }

    public LoggedObject(T obj) : this(obj, new DlQueue<string>())
    { }

    public LoggedObject(T obj, IOrderedContainer<string> container)
    {
        Object = obj;
        Logs = container;
    }

    public virtual void Log(string log) => Logs.Push(log);

    public virtual LoggedObject<T> Run(Func<T, string> func)
    {
        this.Log(func(Object));

        return this;
    }

    /// <inheritdoc />
    public override string ToString()
        => this.ToString(new LogFormatter(s => $"\t[{s}]\n"));

    public virtual string ToString(LogFormatter formatter)
        => $"[<Type: {typeof(T)} >:<Data: {Object} >:<Logs: \n{formatter.FormatCollection(Logs)}>]";
}