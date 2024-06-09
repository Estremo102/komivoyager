using System;

class Program
{
    static void Main()
    {
        Random random = new Random();
        //config
        int countOfPopulation = 5000;
        int mutationPercent;
        int normalMutation = 3;
        int extremeMutation = 100;
        int solutionSavingFrequency = 500;
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
                    if (j == 0) k++;
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
            }
        }
        Solution[] solutions = new Solution[countOfPopulation];
        for (int i = 0; i < solutions.Length; i++)
            solutions[i] = new Solution(travelOrderPopulation[i], data);
        Array.Sort(solutions);

        int solutionNumber = 0;
        while (true)
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
                if (i % 10 == 0) mutationPercent = extremeMutation;
                else mutationPercent = normalMutation;
                newGeneration[i] = newGeneration[newGeneration.Length / i].CreateChild(random.Next(1, mutationPercent));
            }
            solutions = newGeneration;
            Array.Sort(solutions);
            //save best solution
            if (solutionNumber % solutionSavingFrequency == 1)
            {
                using (StreamWriter reader = new StreamWriter($"solutions/solution{solutionNumber}.txt"))
                {
                    reader.WriteLine(solutions[0].ToString() + "\n");
                    Console.WriteLine(solutions[0].Cost);
                }
            }
        }
    }
}

