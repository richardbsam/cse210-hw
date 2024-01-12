using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        List<double> numbers = new List<double>();

        // Assignment: Ask the user for a series of numbers, and append each one to a list. Stop when they enter 0.
        double input;
        do
        {
            Console.Write("Enter a number (0 to stop): ");
            if (double.TryParse(Console.ReadLine(), out input))
            {
                numbers.Add(input);
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        } while (input != 0);

        // Core Requirements:

        // Compute the sum of the numbers in the list.
        double sum = numbers.Sum();
        Console.WriteLine($"Sum of the numbers: {sum}");

        // Compute the average of the numbers in the list.
        double average = numbers.Count > 0 ? numbers.Average() : 0;
        Console.WriteLine($"Average of the numbers: {average}");

        // Find the maximum number in the list.
        double maxNumber = numbers.Count > 0 ? numbers.Max() : 0;
        Console.WriteLine($"Maximum number: {maxNumber}");

        // Stretch Challenge:

        // Find the smallest positive number (the positive number that is closest to zero).
        double smallestPositive = numbers.Where(num => num > 0).DefaultIfEmpty(0).Min();
        Console.WriteLine($"Smallest positive number: {smallestPositive}");

        // Sort the numbers in the list and display the new, sorted list.
        List<double> sortedNumbers = numbers.OrderBy(num => num).ToList();
        Console.WriteLine("Sorted list:");
        foreach (var num in sortedNumbers)
        {
            Console.Write($"{num} ");
        }
    }
}
