public class Matrix 
{
    private double[,] data;

    public int Size { get; }

    public Matrix(int size)
    {
        Size = size;
        data = new double[size, size];
    }
}
