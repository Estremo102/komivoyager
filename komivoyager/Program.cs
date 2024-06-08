using CsvHelper;
using System;
using System.Globalization;

class Program
{
    static void Main()
    {
        //Read data from csv
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
        //Initialize Random Population
        int[][] travelOrderPopulation = new int[100][];
        for (int i = 0;i < travelOrderPopulation.Length; i++)
        {
            travelOrderPopulation[i] = new int[100];
            for (int j = 0; j < travelOrderPopulation[i].Length; j++)
                travelOrderPopulation[i][j] = j;
            Random random = new Random();
            for(int j = 0;j < travelOrderPopulation[i].Length; j++)
            {
                int k = random.Next(100);
                int tmp = travelOrderPopulation[i][j];
                travelOrderPopulation[i][j] = travelOrderPopulation[i][k];
                travelOrderPopulation[i][k] = tmp;
            }
        }

        //Debug
        for(int i = 0; i < 100; i++)
        {
            Console.WriteLine(CostOfTravel(travelOrderPopulation[i], data, i));
        }
    }

    public static int CostOfTravel(int[] travel, int[,] data, int index)
    {
        int cost = 0;
        foreach(int i in travel)
            cost += data[index,i];
        return cost;
    }
}