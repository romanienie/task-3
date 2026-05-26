using System;

class Program
{
    static void Main()
    {
        try
        {
            Matrix a = new Matrix(3, 1, 10);
            Matrix b = new Matrix(3, 1, 10);

            Console.WriteLine("Матрица A:");
            Console.WriteLine(a);

            Console.WriteLine("Матрица B:");
            Console.WriteLine(b);

            Console.WriteLine("A + B:");
            Console.WriteLine(a + b);

            Console.WriteLine("A * B:");
            Console.WriteLine(a * b);

            Console.WriteLine("Детерминант A:");
            Console.WriteLine(a.Determinant());

            Console.WriteLine("Обратная матрица A:");
            Console.WriteLine(a.Inverse());
        }
        catch (MatrixException ex)
        {
            Console.WriteLine("Ошибка: " + ex.Message);
        }
    }
}
