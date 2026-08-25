using System;
using System.Collections.Generic;

interface IInvestigatable
{
    void Investigate();
}
abstract class Person
{
    public string Name ;
    public Person(string n)
    {
        Name = n;
    }
    public virtual void DisplayInfo()
    {
        Console.WriteLine("Name: " + Name);
    }
}
class Detective : Person
{
    public int Points ;
        public Detective(string n) : base(n)
    {
        Points = 0;
    }
    public Detective(Detective d) : base(d.Name)
    {
        Points = d.Points;
    }
    public void AddPoints(int p)
    {
        Points += p;
    }

    public void AddPoints(int p, string r)
    {
        Points += p;
        Console.WriteLine("+" + p + " points: " + r);
    }

    public static Detective operator +(Detective d, int p)
    {
        d.Points += p;
        return d;
    }
    public override void DisplayInfo()
    {
        Console.WriteLine("Detective: " + Name);
        Console.WriteLine("Points: " + Points);
    }

    public void Investigate()
    {
        Console.WriteLine(Name + " is investigating the mystery.");
    }
}
class Suspect : Person, IInvestigatable
{
    public string Occupation ;
    public bool IsCriminal ; 

    public Suspect(string n, string o, bool c)
        : base(n)
    {
        Occupation = o;
        IsCriminal = c;
    }
    public override void DisplayInfo()
    {
        Console.WriteLine("Suspect: " + Name);
        Console.WriteLine("Occupation: " + Occupation);
    }
    public void Investigate()
    {
        Console.WriteLine("Examining suspect: " + Name);
    }
}
class Clue
{
    public string Description ;
    public bool IsCorrect ;
    public int Points;
    public Clue(string d, bool c, int p)
    {
        Description = d;
        IsCorrect = c;
        Points = p;
    }

    public void DisplayClue()
    {
        Console.WriteLine("Clue: " + Description);
    }
}
class Evidence
{
    public string Name;
    public string Description; 
    public Evidence(string n, string d)
    {
        Name = n;
        Description = d;
    }
    public Evidence(string n)
    {
        Name = n;
        Description = "No description.";
    }

    public void DisplayEvidence()
    {
        Console.WriteLine(Name + " - " + Description);
    }
}
class Location
{
    public string Name;
    public Clue Clue1;
    public Clue Clue2;
    public Location(string n)
    {
        Name = n;
    }

    public void Investigate()
    {
        Console.WriteLine("Investigating location: " + Name);

        if (Clue1 != null)
        {
            Clue1.DisplayClue();
        }

        if (Clue2 != null)
        {
            Clue2.DisplayClue();
        }
    }
}
class Case
{
    public string CaseName;
    public string Crime;

    public Location Location1;
    public Location Location2;

    public Suspect Suspect1;
    public Suspect Suspect2;
    public Suspect Suspect3;

    public Case(string cn, string cr)
    {
        CaseName = cn;
        Crime = cr;
    }
    public void DisplayCase()
    {
        Console.WriteLine("Case: " + CaseName);
        Console.WriteLine("Crime: " + Crime);
    }
}
class MysteryGame
{
    public Detective Detective;
    public Case CurrentCase;
    public Evidence Evidence1;
    public MysteryGame(Detective d)
    {
        Detective = d;
    }
    public void SelectCase(Case c)
    {
        CurrentCase = c;

        Console.WriteLine("Case Selected!");
        CurrentCase.DisplayCase();
    }
    public void InvestigateLocation(Location l)
    {
        l.Investigate();
    }
    public void CollectEvidence(Evidence e)
    {
        Evidence1 = e;

        Console.WriteLine("Evidence Collected: " + e.Name);
    }
    public void ShowEvidence()
    {
        if (Evidence1 != null)
        {
            Evidence1.DisplayEvidence();
        }
    }
    public void ExamineSuspect(Suspect s)
    {
        s.Investigate();
        s.DisplayInfo();
    }
    public void IdentifyCriminal(Suspect s)
    {
        if (s.IsCriminal)
        {
            Console.WriteLine("Correct! You caught the criminal!");
            Console.WriteLine("YOU WIN!");
        }
        else
        {
            Console.WriteLine("Wrong suspect!");
            Console.WriteLine("GAME OVER!");
        }
    }
}
class Program
{
    static void Main(string[] args)
    {
       Console.WriteLine("DETECTIVE MYSTERY GAME");
        
        Detective detective = new Detective("Antor");

        Clue clue1 = new Clue("A broken window was found.",true,10);

        Clue clue2 = new Clue("A fingerprint was found.",true,15);

        Clue clue3 = new Clue("A strange letter was found.",false, 5);

        Clue clue4 = new Clue("A shoe print was found.",true,10);

        Location crimeScene = new Location("Crime Scene");
        crimeScene.Clue1 = clue1;
        crimeScene.Clue2 = clue2;

        Location library = new Location("Library");
        library.Clue1 = clue3;
        library.Clue2 = clue4;

        Suspect suspect1 = new Suspect("EVA","Businesswoman",false);

        Suspect suspect2 = new Suspect("RUBAIYA","Tutor",true);

        Suspect suspect3 = new Suspect("NILL","Teacher",false);

        Case mysteryCase = new Case("The Missing Diamond","Diamond Theft");

        mysteryCase.Location1 = crimeScene;
        mysteryCase.Location2 = library;

        mysteryCase.Suspect1 = suspect1;
        mysteryCase.Suspect2 = suspect2;
        mysteryCase.Suspect3 = suspect3;

        
        Evidence evidence = new Evidence("Fingerprint","A fingerprint found.");


        MysteryGame game = new MysteryGame(detective);

        Console.WriteLine("CASE SELECTION ");
        game.SelectCase(mysteryCase);
        Console.WriteLine("INVESTIGATION");
        game.InvestigateLocation(crimeScene);
        detective.AddPoints(clue1.Points,"Found broken window clue");
        detective.AddPoints(clue2.Points,"Found fingerprint clue");
        game.InvestigateLocation(library);
        detective.AddPoints(clue4.Points,"Found shoe print");
        Console.WriteLine("EVIDENCE");
        game.CollectEvidence(evidence);
        Console.WriteLine("Evidence collected:");
        game.ShowEvidence();
        Console.WriteLine(" SUSPECT EXAMINATION");
        game.ExamineSuspect(suspect1);
        game.ExamineSuspect(suspect2);
        game.ExamineSuspect(suspect3);
        detective.DisplayInfo();
        Detective detectiveCopy = new Detective(detective);
        detective.DisplayInfo();
        detectiveCopy.DisplayInfo();
        detective = detective + 10;
        detective.DisplayInfo();
        Console.WriteLine("FINAL DECISION");
        Console.WriteLine("Who is the criminal?");
        Console.WriteLine("1. EVA");
        Console.WriteLine("2. RUBAIYA");
        Console.WriteLine("3. NILL");
        Console.Write("Enter your choice: ");
        string choice = Console.ReadLine();

        if (choice == "1")
        {
            game.IdentifyCriminal(suspect1);
        }
        else if (choice == "2")
        {
            game.IdentifyCriminal(suspect2);
        }
        else if (choice == "3")
        {
            game.IdentifyCriminal(suspect3);
        }
        else
        {
            Console.WriteLine("Invalid choice");
        }

        Console.WriteLine("THANK YOU");

        Console.ReadKey();
    }
}