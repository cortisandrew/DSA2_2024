// See https://aka.ms/new-console-template for more information
using HashtableProject;
using System.ComponentModel;

Console.WriteLine("Hello, World!");

/*
Dictionary<string, int> studentGrade = new Dictionary<string, int>();

studentGrade.Add("Albert", 80);
studentGrade.Add("Bernard", 60);


// the number of available spaces in my hashtable
int arrayLength = 10;

// studentName is the key
string studentName = "Albert";
int arrayPosition = Math.Abs(studentName.GetHashCode()) % arrayLength;

// the value 80, gets placed in array position
*/

ChainingHashtable<string, int> hashtable = new ChainingHashtable<string, int>();

Random random = new Random();

for (int i = 0; i < 100; i++)
{
    hashtable.Add(
        Guid.NewGuid().ToString(),
        random.Next());
}

//hashtable.Add("Albert", 100);
//hashtable.Add("Bernice", 200);
//hashtable.Add("Charles", 300);
//hashtable.Add("Dylan", 500);
//hashtable.Add("Eve", 600);
//hashtable.Add("Francine", 700);
//hashtable.Add("Gerald", 800);
//// hashtable.Add("Albert", 100);

//Console.WriteLine($"Charles:{hashtable.Get("Charles")}");
//Console.WriteLine($"Gerald:{hashtable.Get("Gerald")}");
//// Console.WriteLine($"Harry:{hashtable.Get("Harry")}");


//hashtable.Remove("Eve");
//hashtable.Remove("Charles");
//hashtable.Remove("Albert");
//Console.WriteLine(hashtable.Remove("Eve")); // already removed, returns false!
//hashtable.Remove("Bernice");
//hashtable.Remove("Dylan");
//hashtable.Remove("Gerald");
//hashtable.Remove("Francine");




Console.ReadLine();
