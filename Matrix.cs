public Matrix(int size, int min, int max)
{
    Size = size;
    data = new double[size, size];

    Random random = new Random();

    for (int i = 0; i < size; i++)
    {
        for (int j = 0; j < size; j++)
        {
            data[i, j] = random.Next(min, max);
        }
    }
}
