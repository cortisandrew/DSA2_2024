// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

Dictionary<string, int> studentGrade = new Dictionary<string, int>();

studentGrade.Add("Albert", 80);
studentGrade.Add("Bernard", 60);


// the number of available spaces in my hashtable
int arrayLength = 10;

// studentName is the key
string studentName = "Albert";
int arrayPosition = Math.Abs(studentName.GetHashCode()) % arrayLength;

// the value 80, gets placed in array position

Console.ReadLine();
