using System;
namespace Stage0;

partial class Program
{
    private static void Main(string[] args)
    {
        Welcome7970();
        Welcome5202();
        Console.ReadKey();
    }

    static partial void Welcome5202();
    private static void Welcome7970()
    {
        Console.WriteLine("Enter your name: ");
        string userName = Console.ReadLine();
        Console.WriteLine("{0}, welcome to my first console application", userName);
    }
}