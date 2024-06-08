public class Solution : IComparable<Solution>
{
    public int Cost { get; set; }
    public string Path { get; set; }

    public Solution(int[] travel, int[,] data)
    {
        Path = "Path: ";
        for(int i = 0; i < travel.Length-1; i++)
        {
            Path += $"{travel[i]};";
            Cost+= data[travel[i],travel[i+1]];
        }
    }

    public override string ToString() => $"Cost: {Cost}\n{Path}";

    public int CompareTo(Solution? other)
    {
        if(other.Cost > Cost) return -1 ;
        if (other.Cost < Cost) return 1;
        return 0 ;
    }
}