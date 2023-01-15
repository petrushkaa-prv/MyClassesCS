/*
 * TODO: Implement:                             Status:
 * TODO:            2D printing for BTrees              Done
 * TODO:            2D pr. vertical
 * TODO:            Biparental heap                     InProgress
 * TODO:            Leftist heap
 * TODO:            Binomial queue
 * TODO:            BST tree                            Done
 * TODO:            AVL tree                            Done
 * TODO:            Splay tree                          BUG!
 * TODO:            BW tree
 * TODO:            AA tree
 * TODO:            RST tree
 * TODO:            Seq. matcher                        Done
 * TODO:            Sequence class                      Done
 * TODO:            Sequence for complex cl.            
 * TODO:            Graph                               Done
 * TODO:            DirectedGraph                       Done
 * TODO:            WeightedGraph                       Done
 * TODO:            WeightedDirectedGraph               Done
 * TODO:            MatrixGraphRepresentation           Done
 * TODO:            DFS & BFS                           Done
 * TODO:            Add extensions for graphs           InProgress
 * TODO:            HashTable
 *
 * TODO: Experiment:
 * TODO:            Try unsafe on BST<int>
 * TODO:            And comp. unit tests
 *
 * TODO: Refactor:
 * TODO:            Heap (completely)                   Done
 * TODO:            Heap (IComparable T)
 * TODO:            Generic cl. add comp. dependence
 * TODO:            Rethink the tree nodes imp.         InProgress
 */



#nullable enable

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using static System.Math;
using static System.Console;

using Developing.Arrays;
using Developing.GeneralExtensions;
using Developing.Interfaces;
using Developing.Lists;
using Developing.Nodes;
using Developing.Other;
using Developing.Trees;
using Developing.Graphs;
using Developing.Other.Logger;
using Developing.Testing;
using static Developing.Other.QuickMath;
using static Developing.Graphs.GraphAlgorithms;
using static System.Net.WebRequestMethods;
using Console = System.Console;

namespace Chamber;

internal static class Program
{
    private static readonly Sequence<int> Rand =
        new(
            count: 6, 
            min: 0, 
            max: 10, 
            strLength: 5, 
            seed: 0
        );

    public static void Main(string[] args)
    {
        
    }

}

public static class KnightTourSolving
{
    public static void TrySolve(int boardSize, int startI, int startJ)
    {
        var board = new bool[boardSize, boardSize];

        if (KnightsTour(board, startI, startJ))
            foreach (var valueTuple in Steps)
                Console.Write($"({valueTuple.i}, {valueTuple.j})\t");
        else
            Console.WriteLine("No Solution");
    }

    public static Stack<(int i, int j)> Steps = new();

    public static (int i, int j)[] NextStep = { (1, 2), (-1, 2), (2, 1), (2, -1), (1, -2), (-1, -2), (-2, 1), (-2, -1) };

    public static bool IsSafe(bool[,] board, int i, int j)
        => (i >= 0 && i < board.GetLength(0) && j >= 0 && j < board.GetLength(1) && board[i, j] == false);

    public static bool KnightsTour(bool[,] board, int i, int j)
    {
        if (IsToured(board)) return true;

        Steps.Push((i, j));
        board[i, j] = true;

        if (!KnightsTourSolver(board, 0, 0, 1))
            return false;

        return true;
    }

    public static bool KnightsTourSolver(bool[,] board, int i, int j, int currMove)
    {
        if (currMove == board.GetLength(0) * board.GetLongLength(1))
            return true;

        for (int k = 0; k < board.GetLength(0); k++)
        {
            var (ni, nj) = (i + NextStep[k].i, j + NextStep[k].j);

            if (IsSafe(board, ni, nj))
            {
                Steps.Push((ni, nj));
                board[ni, nj] = true;

                if (KnightsTourSolver(board, ni, nj, currMove + 1))
                    return true;
                else
                {
                    board[ni, nj] = false;
                    Steps.Pop();
                }
            }
        }

        return false;
    }

    public static bool IsToured(bool[,] board)
    {
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                if (!board[i, j]) return false;
            }
        }

        return true;
    }
}

public static class DuoCheater
{
    public static string DuoUrl = "https://www.duolingo.com/learn";

    public static string TranslationSelector = "span class=\"ryNqvb\"";

    public static string RequestTranslation(string input)
        => UrlScraper.Scrap($"https://translate.google.com/?hl=en&sl=en&tl=de&text={input}&op=translate")
    //.Split("<")
    //.Where(row => row[..(TranslationSelector.Length)].Equals(TranslationSelector))
    //.First()
    //.SkipWhile(c => c != '>')
    //.TakeWhile(c => c != '<')
    //.ToString()
    ;

    public static string TakeResult(string input)
        => input.Split('<', StringSplitOptions.None)
            .First(str => str.Take(TranslationSelector.Length).Equals(TranslationSelector));


}