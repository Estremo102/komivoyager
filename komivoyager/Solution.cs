using System;

public class Solution : IComparable<Solution>
{
    public int Cost { get; set; }
    public string Path { get; set; }
    private int[,] data;

    public Solution(int[] travel, int[,] data)
    {
        Random random = new Random();
        this.data = data;
        Path = $"{travel[0]};";
        Cost = data[0, travel[0]];
        for (int i = 0; i < travel.Length - 2; i++)
        {
            Path += $"{travel[i]};";
            Cost += data[travel[i], travel[i + 1]];
        }
        Cost += data[travel[travel.Length - 1], 0];
        Path += $"{travel[travel.Length - 1]}";

        string[] tmpPath = Path.Split(';');
        int[] intPath = new int[tmpPath.Length];
        for (int i = 0; i < intPath.Length; i++)
        {
            intPath[i] = int.Parse(tmpPath[i]);
        }
        for (int i = 1; i < intPath.Length - 1; i++)
        {
            if (intPath[i] == i)
            {
                int j = random.Next(99);
                var a = intPath[i];
                intPath[i] = intPath[j];
                intPath[j] = a;
            }
        }
        for (int i = 0; i < intPath.Length - 1; i++)
        {
            if (intPath[i] == i)
            {
                var tmp = new Solution(intPath, data);
                Path = tmp.Path;
            }
        }
    }

    public override string ToString() => $"Cost: {Cost}\nPath: {Path}";
    public string IncrementToString()
    {
        var a = Path.Split(';');
        var b = "";
        foreach (var i in a)
            b += (int.Parse(i) + 1).ToString() + ";";
        return b;
    }
    public int CompareTo(Solution? other)
    {
        if (other == null) return 1;
        if (other.Cost > Cost) return -1;
        if (other.Cost < Cost) return 1;
        return 0;
    }

    public Solution CreateChild(int mutationpercent = 3)
    {
        //copy DNA
        string[] path = Path.Split(';');
        int[] intPath = new int[path.Length];
        for (int i = 0; i < intPath.Length; i++)
        {
            intPath[i] = int.Parse(path[i]);
        }
        //mutation
        Random random = new Random();
        for (int i = 0; i < path.Length * mutationpercent / 100; i++)
        {
            int a = random.Next(99);
            int b = random.Next(99);
            int tmp = intPath[a];
            intPath[a] = intPath[b];
            intPath[b] = tmp;
        }
        //return child
        return new Solution(intPath, data);
    }
}