// See https://aka.ms/new-console-template for more information
using HashtableProject;
using System.ComponentModel;
using System.Diagnostics;

Console.WriteLine("Hello, World!");


int numberOfElements = 100000;
int numberOfTests = 10000000;

ChainingHashtable<string, int> hashtable = new ChainingHashtable<string, int>();
OpenAddressingHashtable<string, int> openAddressingHashtable = new OpenAddressingHashtable<string, int>((int)Math.Floor(numberOfElements * 2.5));


List<Guid> addedKeys = new List<Guid>();
Random random = new Random();

for (int i = 0; i < numberOfElements; i++)
{
    Guid newGuid = Guid.NewGuid();

    addedKeys.Add(newGuid);

    hashtable.Add(
        newGuid.ToString(),
        random.Next());

    openAddressingHashtable.Add(
        newGuid.ToString(),
        random.Next());
}

Stopwatch swChaining = new Stopwatch();
Stopwatch swOpenAddressing = new Stopwatch();

for (int i = 0; i < 100; i++)
{
    Guid randomKey = addedKeys[random.Next(0, addedKeys.Count)];
    string key = randomKey.ToString();
    
    hashtable.Get(key);
    openAddressingHashtable.Get(key);
}


for (int i = 0; i < numberOfTests; i++)
{
    Guid randomKey = addedKeys[random.Next(0, addedKeys.Count)];
    string key = randomKey.ToString();

    swChaining.Start();
    hashtable.Get(key);
    swChaining.Stop();

    swOpenAddressing.Start();
    openAddressingHashtable.Get(key);
    swOpenAddressing.Stop();
}

Console.WriteLine($"The mean time for a Get using the chaining hashtable is {swChaining.ElapsedTicks / (double)numberOfTests}");
Console.WriteLine($"The mean time for a Get using the open addressing hashtable is {swOpenAddressing.ElapsedTicks / (double)numberOfTests}");

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

/*
ChainingHashtable<string, int> hashtable = new ChainingHashtable<string, int>();

ThreadSafeChainingHashTable<string, int> threadSafeChainingHashTable = new ThreadSafeChainingHashTable<string, int>();

Random random = new Random();

for (int i = 0; i < 100; i++)
{
    hashtable.Add(
        Guid.NewGuid().ToString(),
        random.Next());
}

// Multi-threading
Parallel.For(0, 10000, (i) =>
{
    Console.WriteLine(i);
    // implementation is NOT thread-safe and this will cause issues
    threadSafeChainingHashTable.Add(
        Guid.NewGuid().ToString(),
        i);
});
*/


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

