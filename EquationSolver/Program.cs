using System;
using EquationSolver.Model;

namespace EquationSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = "4E-3";
            var result = EquationBuilder.Calculate(input);
            System.Console.WriteLine($"{input}={result}");
        }
    }
}