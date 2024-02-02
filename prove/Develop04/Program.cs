using System;
using System.Threading;

// Base Activity class
class Activity
{
    private string activityName;
    private string description;
    protected int duration;

    public Activity(string name, string desc)
    {
        activityName = name;
        description = desc;
    }

    public void Start()
    {
        Console.WriteLine($"{activityName} Activity");
        Console.WriteLine(description);
        SetDuration();
        Countdown(3);
    }

    protected virtual void SetDuration()
    {
        Console.Write("How long, in seconds, would you like for your session? ");
        while (!int.TryParse(Console.ReadLine(), out duration) || duration <= 0)
        {
            Console.Write("Invalid input. Please enter a positive integer for duration: ");
        }
    }

    protected void Countdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.WriteLine($"{i}...");
            Thread.Sleep(1000);
        }
    }

    public void Finish()
    {
        Console.WriteLine("Great job!");
        Console.WriteLine($"You have completed the {activityName} Activity in {duration} seconds.");
        Thread.Sleep(3000);
    }
}

// Breathing Activity class
class BreathingActivity : Activity
{
    public BreathingActivity() : base("Breathing", "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
    {
    }

    protected override void SetDuration()
    {
        base.SetDuration();
    }

    public void PerformBreathing()
    {
        int secondsElapsed = 0;
        while (secondsElapsed < duration)
        {
            Console.WriteLine("Breathe in...");
            Thread.Sleep(2000);
            Console.WriteLine("Now Breathe out...");
            Thread.Sleep(2000);
            secondsElapsed += 4;
        }
    }
}

// Reflection Activity class
class ReflectionActivity : Activity
{
    private string[] prompts = {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private string[] reflectionQuestions = {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };

    public ReflectionActivity() : base("Reflection", "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.")
    {
    }

    protected override void SetDuration()
    {
        base.SetDuration();
    }

    public void PerformReflection()
    {
        int secondsElapsed = 0;
        Random random = new Random();

        while (secondsElapsed < duration)
        {
            string prompt = prompts[random.Next(prompts.Length)];
            Console.WriteLine(prompt);
            Thread.Sleep(3000);

            foreach (var question in reflectionQuestions)
            {
                Console.WriteLine(question);
                Thread.Sleep(3000);
            }

            secondsElapsed += reflectionQuestions.Length * 3;
        }
    }
}

// Listing Activity class
class ListingActivity : Activity
{
    private string[] prompts = {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity() : base("Listing", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
    {
    }

    protected override void SetDuration()
    {
        base.SetDuration();
    }

    public void PerformListing()
    {
        int secondsElapsed = 0;
        Random random = new Random();

        while (secondsElapsed < duration)
        {
            string prompt = prompts[random.Next(prompts.Length)];
            Console.WriteLine(prompt);
            Thread.Sleep(3000);

            Console.WriteLine("Get ready to list! Starting in 3...");
            Countdown(3);

            int itemsCount = 0;
            while (true)
            {
                Console.Write("Enter an item (or type 'done' to finish): ");
                string input = Console.ReadLine();
                if (input.ToLower() == "done")
                    break;
                else
                    itemsCount++;
            }

            Console.WriteLine($"You listed {itemsCount} items.");
            secondsElapsed += itemsCount * 3;
        }
    }
}

// Exceeding Requirement by Adding another kind of activity

// Visualization Activity class
class VisualizationActivity : Activity
{
    private string[] prompts = {
        "Imagine yourself on a beautiful beach with white sand and clear blue water.",
        "Visualize a serene mountain landscape with snow-capped peaks and a calm lake.",
        "Picture yourself in a lush, green forest with sunlight streaming through the leaves.",
        "Envision a tranquil garden with vibrant flowers and a gentle breeze.",
        "See yourself in a cozy room with a crackling fireplace and comfortable furniture."
    };

    public VisualizationActivity() : base("Visualization", "This activity will guide you through a creative visualization exercise to promote relaxation and mindfulness.")
    {
    }

    protected override void SetDuration()
    {
        base.SetDuration();
    }

    public void PerformVisualization()
    {
        int secondsElapsed = 0;
        Random random = new Random();

        while (secondsElapsed < duration)
        {
            string prompt = prompts[random.Next(prompts.Length)];
            Console.WriteLine(prompt);
            Thread.Sleep(3000);

            Console.WriteLine("Close your eyes and take a deep breath. Get ready to immerse yourself in the visualization. Starting in 3...");
            Countdown(3);

            // Add more pauses and countdowns as the user engages in the visualization
            Console.WriteLine("2...");
            Thread.Sleep(1000);
            Console.WriteLine("1...");
            Thread.Sleep(1000);

            Console.WriteLine("Now, focus on the details of the scene. Take your time and enjoy the experience.");

            // You can customize this part to add more prompts or guide the user through the visualization
            Thread.Sleep(duration * 1000);

            secondsElapsed += duration;
        }
    }
}


class Program
{
    static void Main()
    {
        while (true)
        {
            ShowMenu();
            int choice = GetChoice();

            switch (choice)
            {
                case 1:
                    BreathingActivity breathingActivity = new BreathingActivity();
                    breathingActivity.Start();
                    breathingActivity.PerformBreathing();
                    breathingActivity.Finish();
                    break;
                case 2:
                    ReflectionActivity reflectionActivity = new ReflectionActivity();
                    reflectionActivity.Start();
                    reflectionActivity.PerformReflection();
                    reflectionActivity.Finish();
                    break;
                case 3:
                    ListingActivity listingActivity = new ListingActivity();
                    listingActivity.Start();
                    listingActivity.PerformListing();
                    listingActivity.Finish();
                    break;
                case 4:
                    VisualizationActivity visualizationActivity = new VisualizationActivity();
                    visualizationActivity.Start();
                    visualizationActivity.PerformVisualization();
                    visualizationActivity.Finish();
                    break;

                case 5:
                    Console.WriteLine("Exiting program. Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static void ShowMenu()
    {
        Console.WriteLine("Menu Options:");
        Console.WriteLine("1. Start Breathing Activity");
        Console.WriteLine("2. Start Reflection Activity");
        Console.WriteLine("3. Start Listing Activity");
        Console.WriteLine("4. Start Visualization Activity");
        Console.WriteLine("5. Quit");
    }

    static int GetChoice()
    {
        Console.Write("Enter your choice (1-5): ");
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= 5)
            {
                return choice;
            }
            else
            {
                Console.Write("Invalid input. Please enter a number between 1 and 5: ");
            }
        }
    }
}

