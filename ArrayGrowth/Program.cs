// See https://aka.ms/new-console-template for more information
using ArrayGrowth;

Console.WriteLine("Hello, World!");

ArrayBasedVector<int> vector = new ArrayBasedVector<int>();

vector.Append(100);
vector.Append(200);
vector.Append(300);
vector.Append(400);
vector.Append(500);
vector.Append(600);

Console.WriteLine(vector[3]);
vector[4] = 555;
Console.WriteLine(vector[4]);

Console.WriteLine(vector);