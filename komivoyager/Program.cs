using CsvHelper;
using System;
using System.Globalization;

class Program
{
    static void Main()
    {
        int[,] data = new int[100, 100];
        using (var reader = new StreamReader("dane.csv"))
        {
            for (int i = 0; i < 100; i++)
            {
                string[] line = reader.ReadLine().Split(';');
                for (int j = 0; j < line.Length; j++)
                    data[i, j] = int.Parse(line[j]);
            }
        }

    }
}