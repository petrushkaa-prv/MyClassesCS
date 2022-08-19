using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developing.Other.Logger;

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