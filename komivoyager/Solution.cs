public class Solution : IComparable<Solution>
{
    public int Cost { get; set; }
    public string Path { get; set; }
    private int[,] data;

    public Solution(int[] travel, int[,] data)
    {
        Path = $"{data[0, travel[0]]};";
        Cost = data[0, travel[0]];
        for (int i = 0; i < travel.Length - 1; i++)
        {
            Path += $"{travel[i]};";
            Cost += data[travel[i], travel[i + 1]];
            this.data = data;
        }
        Cost += data[travel[travel.Length - 1], 0];
        Path += $"{data[travel[travel.Length - 1], 0]}";
    }

    public override string ToString() => $"Cost: {Cost}\nPath: {Path}";
    public string IncrementToString()
    {
        var a = Path.Split(';') ;
        var b = "";
        foreach (var i in a)
            b += (int.Parse(i) + 1).ToString() + ";";
        return b ;
    }
    public int CompareTo(Solution? other)
    {
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
            int a = random.Next(100);
            int b = random.Next(100);
            int tmp = intPath[a];
            intPath[a] = intPath[b];
            intPath[b] = tmp;
        }
        //return child
        return new Solution(intPath, data);
    }
}