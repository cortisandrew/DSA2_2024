// See https://aka.ms/new-console-template for more information
using ArrayGrowth;
using System.Diagnostics;

Console.WriteLine("Hello, World!");

Stopwatch sw = Stopwatch.StartNew();

ArrayBasedVector<int> vector = new ArrayBasedVector<int>(new DoublingGrowthStrategy());

sw.Start();
vector.Append(100);
vector.Append(200);
vector.Append(300);
vector.Append(400);
vector.Append(500);
vector.Append(600);
sw.Stop();

//Console.WriteLine(vector[3]);
vector[4] = 555;
//Console.WriteLine(vector[4]);

//Console.WriteLine(vector);
//Console.WriteLine($"The operations took {sw.ElapsedTicks} ticks");
//Console.WriteLine($"The total number of write and copy operations is {vector.WorkCarriedOut}");

int[] numberOfAppendOperationsArray = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 };

Console.WriteLine("Doubling Strategy");
foreach (int numberOfAppendOperations in numberOfAppendOperationsArray)
{
    vector = new ArrayBasedVector<int>(new DoublingGrowthStrategy());
    
    // Carry out numberOfAppendOperations Append Operations and see how long it takes
    for (int i = 0; i<numberOfAppendOperations;i++)
        vector.Append(1);

    Console.WriteLine($"{numberOfAppendOperations}, {vector.WorkCarriedOut}");
}

Console.WriteLine("Incremental Strategy, c=2");
foreach (int numberOfAppendOperations in numberOfAppendOperationsArray)
{
    vector = new ArrayBasedVector<int>(new IncrementalGrowthStrategy(2));

    // Carry out numberOfAppendOperations Append Operations and see how long it takes
    for (int i = 0; i < numberOfAppendOperations; i++)
        vector.Append(1);

    Console.WriteLine($"{numberOfAppendOperations}, {vector.WorkCarriedOut}");
}

//Console.Clear();

int largeProblemSize = 100000000;

vector = new ArrayBasedVector<int>(new DoublingGrowthStrategy());
for (int i = 0; i < largeProblemSize; i++)
{
    vector.Append(1);
}
Console.WriteLine($"To append {largeProblemSize} elements, it took {vector.WorkCarriedOut} operations");


vector = new ArrayBasedVector<int>(new IncrementalGrowthStrategy(10000));
for (int i = 0; i < largeProblemSize; i++)
{
    vector.Append(1);
}
Console.WriteLine($"To append {largeProblemSize} elements, it took {vector.WorkCarriedOut} operations");


