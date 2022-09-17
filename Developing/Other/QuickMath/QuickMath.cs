using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developing.Other;

internal static class QuickMath
{
    /// <summary>
    /// Finds the Greatest Common Divisor of two numbers
    /// </summary>
    public static int Gcd(int a, int b)
    {
        while (b != 0)
        {
            var temp = a % b;
            a = b;
            b = temp;
        }
            
        return a;
    }

    /// <summary>
    /// Finds the Least Common Denominator of two numbers
    /// </summary>
    public static int Lcm(int a, int b)
        => a * b / Gcd(a, b);

    public static int FractionSum(int a, int dena, int b, int denb)
    {
        var lcd = Lcm(dena, denb);

        return a * (lcd / dena) + b * (lcd / denb);
    }

    public static unsafe float QrSqrt(float number)
    {
        const float th = 1.5f;

        float x2 = number * 0.5f;

        int i = 0x5f3759df - (*(int*)&number >> 1);
        float y = *(float*)&i;

        return y * (th - x2 * y * y);
    }
    public static unsafe float QrSqrt(float number, int iterations)
    {
        const float th = 1.5f;

        float x2 = number * 0.5f;

        int i = 0x5f3759df - (*(int*)&number >> 1);
        float y = *(float*)&i;

        for (int j = 0; j < iterations; j++)
            y *= th - x2 * y * y;

        return y;
    }
}