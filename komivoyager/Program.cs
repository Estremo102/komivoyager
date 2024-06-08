using CsvHelper;
using System;
using System.Globalization;

class Program
{
    static void Main()
    {
        Random random = new Random();
        //config
        int countOfPopulation = 5000;
        int mutationPercent = 3; //in %
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
        int[][] travelOrderPopulation = new int[countOfPopulation][];
        for (int i = 0; i < travelOrderPopulation.Length; i++)
        {
            travelOrderPopulation[i] = new int[100];
            for (int j = 0; j < travelOrderPopulation[i].Length; j++)
                travelOrderPopulation[i][j] = 99 - j;
            for (int j = 0; j < travelOrderPopulation[i].Length - 1; j++)
            {
                int k = random.Next(99);
                if (k == j)
                {
                    if (j == 0)
                    {
                        k++;
                    }
                    else k--;
                }
                int tmp = travelOrderPopulation[i][j];
                travelOrderPopulation[i][j] = travelOrderPopulation[i][k];
                travelOrderPopulation[i][k] = tmp;
                bool noDouble = false;
                while (!noDouble)
                {
                    for (int l = 1; l < travelOrderPopulation[i].Length - 1; l++)
                    {
                        if (travelOrderPopulation[i][l] == l)
                        {
                            var r = random.Next(99);
                            var a = travelOrderPopulation[i][l];
                            travelOrderPopulation[i][l] = travelOrderPopulation[i][r];
                            travelOrderPopulation[i][r] = a;
                        }
                    }
                    noDouble = true;
                    for (int l = 1; l < travelOrderPopulation[i].Length - 1; l++)
                    {
                        if (travelOrderPopulation[i][l] == l)
                            noDouble = false;
                    }

                }
                for (int l = 1; l < travelOrderPopulation[i].Length - 1; l++)
                {
                    if (travelOrderPopulation[i][l] == l)
                        throw new Exception();
                }
            }
        }
        Solution[] solutions = new Solution[countOfPopulation];
        for (int i = 0; i < solutions.Length; i++)
            solutions[i] = new Solution(travelOrderPopulation[i], data);
        Array.Sort(solutions);
        //////Console.WriteLine(solutions[0].Cost);
        //////string[] a = solutions[0].Path.Split(';');

        int solutionNumber = 0;
        //while (true)
        {
            solutionNumber++;
            //choose best 10% and create children
            int tenPercent = countOfPopulation / 10;
            Solution[] newGeneration = new Solution[countOfPopulation];
            for (int i = 0; i < tenPercent; i++)
            {
                newGeneration[i] = solutions[i];
            }
            for (int i = tenPercent; i < newGeneration.Length; i++)
            {
                if (i % 10 == 0) mutationPercent = 100;
                else mutationPercent = 5;
                newGeneration[i] = newGeneration[newGeneration.Length / i].CreateChild(random.Next(1, mutationPercent));
            }
            solutions = newGeneration;
            Array.Sort(solutions);
            //save best solution
            //if (solutionNumber % 500 == 0)
            {
                using (StreamWriter reader = new StreamWriter($"solutions/solution{solutionNumber}.txt"))
                {
                    reader.WriteLine(solutions[0].ToString() + "\n");
                    reader.WriteLine(solutions[0].IncrementToString());
                    Console.WriteLine(solutions[0].Cost);
                }
            }

            string[] a = solutions[0].Path.Split(';');
            int[] b = new int[a.Length];
            for (int i = 0; i < b.Length; i++)
                b[i] = int.Parse(a[i]);
            Array.Sort(b);
            Console.WriteLine(b.Length);
            int aaa = 0;
            foreach (int i in b)
                if (aaa == i) Console.WriteLine(aaa);
        }
    }

    public static int CostOfTravel(int[] travel, int[,] data, int index)
    {
        int cost = 0;
        foreach (int i in travel)
            cost += data[index, i];
        return cost;
    }
}

