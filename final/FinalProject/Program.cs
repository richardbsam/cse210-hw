using System;
using System.Collections.Generic;


// Task Class (Base Class)
public class Task
{
    // Properties
    private string Title { get; set; }
    private string Description { get; set; }
    private DateTime StartDate { get; set; }
    private TimeSpan Duration { get; set; }
    private DateTime DueDate => StartDate + Duration;
    private Priority Priority { get; set; }

    // Constructor
    public Task(string title, string description, DateTime startDate, TimeSpan duration, Priority priority)
    {
        Title = title;
        Description = description;
        StartDate = startDate;
        Duration = duration;
        Priority = priority;
    }

    // Method
    public virtual void DisplayTaskDetails()
    {
        Console.WriteLine($"Title: {Title}");
        Console.WriteLine($"Description: {Description}");
        Console.WriteLine($"Start Date: {StartDate.ToShortDateString()}");
        Console.WriteLine($"Due Date: {DueDate.ToShortDateString()}");
        Console.WriteLine($"Priority: {Priority}");
    }
}

// RegularTask Class (Derived from Task)
public class RegularTask : Task
{
    // Constructor
    public RegularTask(string title, string description, DateTime startDate, TimeSpan duration, Priority priority)
        : base(title, description, startDate, duration, priority) { }

    // Overrides
    public override void DisplayTaskDetails()
    {
        base.DisplayTaskDetails();
        Console.WriteLine("Type: Regular Task");
    }
}

// UrgentTask Class (Derived from Task)
public class UrgentTask : Task
{
    // Constructor
    public UrgentTask(string title, string description, DateTime startDate, TimeSpan duration, Priority priority)
        : base(title, description, startDate, duration, priority) { }

    // Overrides
    public override void DisplayTaskDetails()
    {
        base.DisplayTaskDetails();
        Console.WriteLine("Type: Urgent Task");
    }
}

// RecurringTask Class (Derived from Task)
public class RecurringTask : Task
{
    // Constructor
    public RecurringTask(string title, string description, DateTime startDate, TimeSpan duration, Priority priority)
        : base(title, description, startDate, duration, priority) { }

    // Overrides
    public override void DisplayTaskDetails()
    {
        base.DisplayTaskDetails();
        Console.WriteLine("Type: Recurring Task");
    }
}

// TaskManager Class
public class TaskManager
{
    private List<Task> tasks;

    // Constructor
    public TaskManager()
    {
        tasks = new List<Task>();
    }

    // Methods
    public void AddTask(Task task)
    {
        tasks.Add(task);
    }

    public void DisplayAllTasks()
    {
        foreach (var task in tasks)
        {
            task.DisplayTaskDetails();
            Console.WriteLine();
        }
    }
}

// Priority Enum
public enum Priority
{
    Low,
    Medium,
    High
}

// Main Application Class
class Program
{
    static void Main()
    {
        TaskManager taskManager = new TaskManager();

        do
        {
            // Get user input for task details
            Console.Write("Enter Task Title: ");
            string title = Console.ReadLine();

            Console.Write("Enter Task Description: ");
            string description = Console.ReadLine();

            Console.Write("Enter Start Date (yyyy-mm-dd): ");
            DateTime startDate;
            while (!DateTime.TryParse(Console.ReadLine(), out startDate))
            {
                Console.WriteLine("Invalid date format. Please try again.");
                Console.Write("Enter Start Date (yyyy-mm-dd): ");
            }

            Console.Write("Enter Duration in days: ");
            int duration;
            while (!int.TryParse(Console.ReadLine(), out duration) || duration <= 0)
            {
                Console.WriteLine("Invalid duration. Please enter a positive integer.");
                Console.Write("Enter Duration in days: ");
            }

            Console.Write("Enter Priority (Low/Medium/High): ");
            Priority priority;
            while (!Enum.TryParse(Console.ReadLine(), true, out priority))
            {
                Console.WriteLine("Invalid priority. Please enter Low, Medium, or High.");
                Console.Write("Enter Priority (Low/Medium/High): ");
            }

            // Create task based on user input
            Console.Write("Select Task Type (Regular/Urgent/Recurring): ");
            string taskType = Console.ReadLine();

            Task newTask;
            switch (taskType.ToLower())
            {
                case "regular":
                    newTask = new RegularTask(title, description, startDate, TimeSpan.FromDays(duration), priority);
                    break;
                case "urgent":
                    newTask = new UrgentTask(title, description, startDate, TimeSpan.FromDays(duration), priority);
                    break;
                case "recurring":
                    newTask = new RecurringTask(title, description, startDate, TimeSpan.FromDays(duration), priority);
                    break;
                default:
                    Console.WriteLine("Invalid task type. Defaulting to Regular Task.");
                    newTask = new RegularTask(title, description, startDate, TimeSpan.FromDays(duration), priority);
                    break;
            }

            // Add the task to the task manager
            taskManager.AddTask(newTask);

            // Ask the user if they want to add another task
            Console.Write("Do you want to add another task? (yes/no): ");
        } while (Console.ReadLine().ToLower() == "yes");

        // Display all tasks
        Console.WriteLine("\nAll Tasks:\n");
        taskManager.DisplayAllTasks();
    }
}
