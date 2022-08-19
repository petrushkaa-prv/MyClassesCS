using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developing.Other.Logger;

public static class LogWrapper<T>
{
    public static LoggedObject<T> WrapWithLogs(T obj) => new(obj);

    public static LoggedObject<T> RunUnMutated(LoggedObject<T> obj, Func<T, (T obj, string log)> func)
    {
        var newLogEntry = func(obj.Object);

        obj.Log(newLogEntry.log);

        return new LoggedObject<T>(newLogEntry.obj, obj.Logs);
    }
}