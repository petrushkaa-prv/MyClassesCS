using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using Developing.GeneralInterfaces;
using Developing.Lists;

namespace Developing.Testing;

public class Tester
{
    protected IMyList<(string name, Func<object> test, object expectedResult)> _tests;
    protected bool[] _testPasssed = null;

    protected Tester(IMyList<(string name, Func<object> test, object expectedResult)> tests)
    {
        _tests = tests;
    }

    public Tester() : this(new SlList<(string name, Func<object> test, object expectedResult)>())
    {
    }

    public void AddTestCase(string name, Func<object> test, object expectedResult)
    {
        //if (expectedResult.GetType() != test.Target!.GetType())
        //    throw new Exception($"Type mismatch in test {name}");

        _tests.AddFront((name, test, expectedResult));
    }

    public void PrepareTests()
    {
        _testPasssed = new bool[_tests.Count()];
    }

    public virtual void PerformTests()
    {
        int i = 0;
        foreach (var (name, test, expectedResult) in _tests)
        {
            Console.Write($"Performing test {i + 1}: [{name}]\tValue expected: {expectedResult}\t");
            var result = test.Invoke();
            Console.Write($"Result: {result}\t");
            var status = _testPasssed[i] = result.Equals(expectedResult);
            if (status)
            {
                Console.Write("Status: ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Passed");
            }
            else
            {
                Console.Write("Status: ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Failed");
            }

            Console.WriteLine();

            Console.ResetColor();
        }
    }
}

