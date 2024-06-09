using System;

public class Solution : IComparable<Solution>
{
    public int Cost { get; set; }
    public string Path
    {
        get
        {
            string Path = "";
            for(int i = 0; i < 99; i++)
            {
                Path += $"{Map[i]};";
            }
            Path += $"{Map[99]}";
            return Path;
        }
    }
    public Dictionary<int, int> Map { get; set; }

    private int[,] data;

    public Solution(int[] travel, int[,] data)
    {
        Map = new Dictionary<int, int>();
        Random random = new Random();
        this.data = data;
        Map[0] = travel[0];
        Cost = data[0, travel[0]];
        for (int i = 1; i < travel.Length - 1; i++)
        {
            Map[i] = travel[i];
            Cost += data[travel[i], travel[i + 1]];
        }
        Cost += data[travel[travel.Length - 1], 0];
        Map[99] = travel[travel.Length - 1];
    }

    public override string ToString() => $"Cost: {Cost}\nPath: {Path}";

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