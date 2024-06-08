public class Solution : IComparable<Solution>
{
    public int Cost { get; set; }
    public string Path { get; set; }

    public Solution(int[] travel, int[,] data, int index)
    {
        Path = "Path: ";
        foreach (int i in travel)
        {
            Cost += data[index, i];
            Path += $"{data[index, i]};";
        }
    }

    public override string ToString() => $"Cost: {Cost}\n{Path}";

    public int CompareTo(Solution? other)
    {
        if (other.Cost > Cost) return -1;
        if (other.Cost < Cost) return 1;
        return 0;
    }
}