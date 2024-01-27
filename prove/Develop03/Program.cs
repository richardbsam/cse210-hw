using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        // Create a sample scripture
        var scripture = new Scripture("Doctrine and Covenants 88:118", "And as all have not faith, seek ye diligently and teach one another words of wisdom; yea, seek ye out of the best books words of wisdom; seek learning, even by study and also by faith.");

        // Run the scripture memorizer
        Memorizer memorizer = new Memorizer(scripture);
        memorizer.Run();
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