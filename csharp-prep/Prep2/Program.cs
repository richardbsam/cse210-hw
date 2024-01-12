using System;

class Program
{
    static void Main(string[] args)
    {
     
        // Ask the user for their grade percentage
        Console.Write("Enter your grade percentage: ");
        double gradePercentage = Convert.ToDouble(Console.ReadLine());

        // Initialize variables
        string letter;
        string sign = "";

        // Determine the letter grade
        if (gradePercentage >= 90)
        {
            letter = "A";
        }
        else if (gradePercentage >= 80)
        {
            letter = "B";
        }
        else if (gradePercentage >= 70)
        {
            letter = "C";
        }
        else if (gradePercentage >= 60)
        {
            letter = "D";
        }
        else
        {
            letter = "F";
        }

        // Determine the sign for stretch challenge
        int lastDigit = (int)gradePercentage % 10;
        if (letter != "F" && (lastDigit >= 7))
        {
            sign = "+";
        }
        else if (letter != "F" && (lastDigit < 3))
        {
            sign = "-";
        }

        // Display the grade and message
        Console.WriteLine($"Your grade is: {letter}{sign}");

        // Check if the user passed the course
        if (letter != "F" && gradePercentage >= 70)
        {
            Console.WriteLine("Congratulations! You passed the course.");
        }
        else
        {
            Console.WriteLine("Keep up the good work! Try again next time.");
        }
    }
}
