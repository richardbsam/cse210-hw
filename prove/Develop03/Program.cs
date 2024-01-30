using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


// Exceed requirements.
// The program now loads scriptures from a files named scriptures.txt


class Program
{
    static void Main(string[] args)
    {
        // Load scriptures from file
        List<Scripture> scriptures = LoadScripturesFromFile("scriptures.txt");

        if (scriptures.Count == 0)
        {
            Console.WriteLine("No scriptures found in the file. Exiting program.");
            return;
        }

        // Run the scripture memorizer for each scripture
        foreach (var scripture in scriptures)
        {
            Memorizer memorizer = new Memorizer(scripture);
            memorizer.Run();
        }
    }

    static List<Scripture> LoadScripturesFromFile(string filePath)
    {
        List<Scripture> scriptures = new List<Scripture>();

        try
        {
            // Read all lines from the file
            string[] lines = File.ReadAllLines(filePath);

            // Process each line as a scripture entry
            foreach (string line in lines)
            {
                // Split the line into reference and text
                string[] parts = line.Split(new[] { ':' }, 2);
                if (parts.Length == 2)
                {
                    string reference = parts[0].Trim();
                    string text = parts[1].Trim();
                    scriptures.Add(new Scripture(reference, text));
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading scriptures from file: {ex.Message}");
        }

        return scriptures;
    }
}

class Scripture
{
    public Reference Reference { get; private set; }
    public string Text { get; private set; }

    public Scripture(string reference, string text)
    {
        Reference = new Reference(reference);
        Text = text;
    }

    public bool AllWordsHidden()
    {
        return Text.Split(' ').All(word => word == "_" || word == "");
    }

    public void HideRandomWords()
    {
        List<string> words = Text.Split(' ').ToList();

        // Select a few words to hide
        for (int i = 0; i < words.Count / 3; i++)
        {
            int index = new Random().Next(0, words.Count);
            words[index] = "_";
        }

        Text = string.Join(" ", words);
    }
}

class Reference
{
    public string Verse { get; private set; }

    public Reference(string verse)
    {
        Verse = verse;
    }
}

class Memorizer
{
    private Scripture scripture;

    public Memorizer(Scripture scripture)
    {
        this.scripture = scripture;
    }

    public void Run()
    {
        do
        {
            Console.Clear();
            Console.WriteLine($"{scripture.Reference.Verse}: {scripture.Text}");
            Console.WriteLine("\nPress Enter to continue or type 'quit' to finish.");

            string input = Console.ReadLine();

            if (input.ToLower() == "quit")
            {
                break;
            }

            scripture.HideRandomWords();
        }
        while (!scripture.AllWordsHidden());
    }
}


