using CsvHelper;
using System;
using System.Globalization;

class Program
{
    static void Main()
    {
        using (var reader = new StreamReader("dane.csv"))
        {
            int[,] data = new int[100,100];
            for (int i = 0; i < 100; i++)
            {
                var line = reader.ReadLine().Split(';');
                for(int j = 0; j < line.Length; j++)
                    data[i,j] = int.Parse(line[j]);
            }
        }
    }
}