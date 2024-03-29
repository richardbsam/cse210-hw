using System;
using System.Collections.Generic;
using System.IO;

// Base class for goals
public abstract class Goal
{
    public string Name { get; set; }
    public int Points { get; protected set; }

    public Goal(string name)
    {
        Name = name;
        Points = 0;
    }

    public virtual void RecordEvent()
    {
        Console.WriteLine($"Goal achieved: {Name} (+{Points} points)");
    }

    public abstract string GetStatus();
}

// SimpleGoal class for goals that can be marked complete
public class SimpleGoal : Goal
{
    public SimpleGoal(string name, int points) : base(name)
    {
        Points = points;
    }

    public override string GetStatus()
    {
        return "[ ]";
    }
}

// EternalGoal class for goals that are never complete
public class EternalGoal : Goal
{
    public EternalGoal(string name, int points) : base(name)
    {
        Points = points;
    }

    public override void RecordEvent()
    {
        base.RecordEvent();
        Console.WriteLine($"({Name} is eternal, can be recorded multiple times)");
    }

    public override string GetStatus()
    {
        return "[X]";
    }
}

// ChecklistGoal class for goals with a checklist
public class ChecklistGoal : Goal
{
    private int completedCount;
    private int requiredCount;

    public ChecklistGoal(string name, int points, int requiredCount) : base(name)
    {
        Points = points;
        this.requiredCount = requiredCount;
        completedCount = 0;
    }

    public override void RecordEvent()
    {
        base.RecordEvent();
        completedCount++;

        if (completedCount >= requiredCount)
        {
            Points += 500; // Bonus points for completing the checklist
            Console.WriteLine($"Bonus: +500 points for completing {Name} {requiredCount} times!");
        }
    }

    public override string GetStatus()
    {
        return $"Completed {completedCount}/{requiredCount} times";
    }
}

// Main program
class Program
{
    static List<Goal> goals = new List<Goal>();
    static int userPoints = 0;

    static void Main()
    {
        LoadGoals();
        int choice;

        do
        {
            DisplayMenu();
            Console.Write("Select a Choice from the menu: ");
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 6)
            {
                Console.Write("Invalid input. Enter a valid choice: ");
            }

            ProcessChoice(choice);
        } while (choice != 6);

        SaveGoals();
    }

    static void DisplayMenu()
    {
        Console.WriteLine($"\nYou have {userPoints} Points\n");
        Console.WriteLine("Menu Options:");
        Console.WriteLine("1. Create New Goal");
        Console.WriteLine("2. List Goals");
        Console.WriteLine("3. Save Goals");
        Console.WriteLine("4. Load Goals");
        Console.WriteLine("5. Record Event");
        Console.WriteLine("6. Quit");
    }

    static void ProcessChoice(int choice)
    {
        switch (choice)
        {
            case 1:
                CreateNewGoal();
                break;
            case 2:
                ListGoals();
                break;
            case 3:
                SaveGoals();
                break;
            case 4:
                LoadGoals();
                break;
            case 5:
                RecordEvent();
                break;
            default:
                Console.WriteLine("Exiting program.");
                break;
        }
    }

    static void CreateNewGoal()
    {
        Console.Write("Enter goal name: ");
        string name = Console.ReadLine();

        Console.WriteLine("Select goal type:");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");

        int goalType;
        while (!int.TryParse(Console.ReadLine(), out goalType) || goalType < 1 || goalType > 3)
        {
            Console.Write("Invalid input. Enter a valid goal type: ");
        }

        Goal newGoal;

        switch (goalType)
        {
            case 1:
                Console.Write("Enter points for completing the goal: ");
                int pointsSimple;
                while (!int.TryParse(Console.ReadLine(), out pointsSimple) || pointsSimple < 0)
                {
                    Console.Write("Invalid input. Enter a valid points value: ");
                }
                newGoal = new SimpleGoal(name, pointsSimple);
                break;
            case 2:
                Console.Write("Enter points for each recording of the goal: ");
                int pointsEternal;
                while (!int.TryParse(Console.ReadLine(), out pointsEternal) || pointsEternal < 0)
                {
                    Console.Write("Invalid input. Enter a valid points value: ");
                }
                newGoal = new EternalGoal(name, pointsEternal);
                break;
            case 3:
                Console.Write("Enter points for each completion of the goal: ");
                int pointsChecklist;
                while (!int.TryParse(Console.ReadLine(), out pointsChecklist) || pointsChecklist < 0)
                {
                    Console.Write("Invalid input. Enter a valid points value: ");
                }
                Console.Write("Enter required completion count for the checklist goal: ");
                int requiredCount;
                while (!int.TryParse(Console.ReadLine(), out requiredCount) || requiredCount < 1)
                {
                    Console.Write("Invalid input. Enter a valid count value: ");
                }
                newGoal = new ChecklistGoal(name, pointsChecklist, requiredCount);
                break;
            default:
                throw new InvalidOperationException("Invalid goal type.");
        }

        goals.Add(newGoal);
        Console.WriteLine($"New goal '{name}' created successfully!");
    }

    static void ListGoals()
    {
        Console.WriteLine("\nList of Goals:");
        foreach (var goal in goals)
        {
            Console.WriteLine($"{goal.GetStatus()} {goal.Name} ({goal.Points} points)");
        }
    }

    static void RecordEvent()
    {
        Console.WriteLine("\nRecord Event - Choose a goal to record:");
        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {goals[i].Name} ({goals[i].Points} points)");
        }

        int goalIndex;
        while (!int.TryParse(Console.ReadLine(), out goalIndex) || goalIndex < 1 || goalIndex > goals.Count)
        {
            Console.Write("Invalid input. Enter a valid goal index: ");
        }

        goals[goalIndex - 1].RecordEvent();
        userPoints += goals[goalIndex - 1].Points;
    }

    static void SaveGoals()
    {
        using (StreamWriter writer = new StreamWriter("goals.txt"))
        {
            foreach (var goal in goals)
            {
                writer.WriteLine($"{goal.GetType().Name},{goal.Name},{goal.Points}");
            }
        }

        Console.WriteLine("Goals saved successfully!");
    }

    static void LoadGoals()
    {
        goals.Clear();
        userPoints = 0;

        if (File.Exists("goals.txt"))
        {
            using (StreamReader reader = new StreamReader("goals.txt"))
            {
                while (!reader.EndOfStream)
                {
                    string[] goalData = reader.ReadLine().Split(',');
                    string goalType = goalData[0];
                    string goalName = goalData[1];
                    int goalPoints = int.Parse(goalData[2]);

                    Goal loadedGoal;

                    switch (goalType)
                    {
                        case "SimpleGoal":
                            loadedGoal = new SimpleGoal(goalName, goalPoints);
                            break;
                        case "EternalGoal":
                            loadedGoal = new EternalGoal(goalName, goalPoints);
                            break;
                        case "ChecklistGoal":
                            int requiredCount = int.Parse(goalData[3]);
                            loadedGoal = new ChecklistGoal(goalName, goalPoints, requiredCount);
                            break;
                        default:
                            throw new InvalidOperationException("Invalid goal type during loading.");
                    }

                    goals.Add(loadedGoal);
                }
            }

            Console.WriteLine("Goals loaded successfully!");
        }
        else
        {
            Console.WriteLine("No saved goals found.");
        }
    }
}
