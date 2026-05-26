using System;

class Program
{
    static Matrix matrixA;
    static Matrix matrixB;

    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        bool isRunning = true;

        while (isRunning)
        {
            try
            {
                ShowMenu();

                Console.Write("Выберите пункт: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        matrixA = CreateRandomMatrix("A");
                        break;

                    case "2":
                        matrixB = CreateRandomMatrix("B");
                        break;

                    case "3":
                        ShowMatrices();
                        break;

                    case "4":
                        CheckMatricesCreated();
                        Console.WriteLine("A + B:");
                        Console.WriteLine(matrixA + matrixB);
                        break;

                    case "5":
                        CheckMatricesCreated();
                        Console.WriteLine("A * B:");
                        Console.WriteLine(matrixA * matrixB);
                        break;

                    case "6":
                        CheckMatrixACreated();
                        Console.WriteLine("Детерминант A:");
                        Console.WriteLine(matrixA.Determinant());
                        break;

                    case "7":
                        CheckMatrixACreated();
                        Console.WriteLine("Обратная матрица A:");
                        Console.WriteLine(matrixA.Inverse());
                        break;

                    case "8":
                        CheckMatricesCreated();
                        CompareMatrices();
                        break;

                    case "9":
                        CheckMatrixACreated();
                        Matrix copy = matrixA.CloneMatrix();

                        Console.WriteLine("Копия матрицы A:");
                        Console.WriteLine(copy);

                        Console.WriteLine("Это разные объекты в памяти:");
                        Console.WriteLine(ReferenceEquals(matrixA, copy) ? "Нет" : "Да");
                        break;

                    case "10":
                        DemonstrateTypeCasting();
                        break;

                    case "0":
                        isRunning = false;
                        Console.WriteLine("Программа завершена.");
                        break;

                    default:
                        Console.WriteLine("Такого пункта нет.");
                        break;
                }
            }
            catch (MatrixException ex)
            {
                Console.WriteLine("Ошибка матрицы: " + ex.Message);
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка: нужно вводить число.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Неожиданная ошибка: " + ex.Message);
            }

            Console.WriteLine();
        }
    }

    static void ShowMenu()
    {
        Console.WriteLine("===== МАТРИЧНЫЙ КАЛЬКУЛЯТОР =====");
        Console.WriteLine("1. Создать случайную матрицу A");
        Console.WriteLine("2. Создать случайную матрицу B");
        Console.WriteLine("3. Показать матрицы");
        Console.WriteLine("4. A + B");
        Console.WriteLine("5. A * B");
        Console.WriteLine("6. Детерминант A");
        Console.WriteLine("7. Обратная матрица A");
        Console.WriteLine("8. Сравнить A и B");
        Console.WriteLine("9. Скопировать A");
        Console.WriteLine("10. Показать приведение типов и true/false");
        Console.WriteLine("0. Выход");
    }

    static Matrix CreateRandomMatrix(string name)
    {
        Console.Write($"Введите размер матрицы {name}: ");
        int size = int.Parse(Console.ReadLine());

        Console.Write("Введите минимальное случайное число: ");
        int min = int.Parse(Console.ReadLine());

        Console.Write("Введите максимальное случайное число: ");
        int max = int.Parse(Console.ReadLine());

        Matrix matrix = new Matrix(size, min, max);

        Console.WriteLine($"Матрица {name} создана:");
        Console.WriteLine(matrix);

        return matrix;
    }

    static void ShowMatrices()
    {
        Console.WriteLine("Матрица A:");
        Console.WriteLine(matrixA == null ? "Матрица A еще не создана." : matrixA.ToString());

        Console.WriteLine("Матрица B:");
        Console.WriteLine(matrixB == null ? "Матрица B еще не создана." : matrixB.ToString());
    }

    static void CheckMatrixACreated()
    {
        if (matrixA == null)
            throw new MatrixOperationException("Сначала создайте матрицу A.");
    }

    static void CheckMatricesCreated()
    {
        if (matrixA == null || matrixB == null)
            throw new MatrixOperationException("Сначала создайте матрицы A и B.");
    }

    static void CompareMatrices()
    {
        Console.WriteLine("Детерминант A: " + matrixA.Determinant());
        Console.WriteLine("Детерминант B: " + matrixB.Determinant());

        if (matrixA > matrixB)
            Console.WriteLine("A больше B по детерминанту.");
        else if (matrixA < matrixB)
            Console.WriteLine("A меньше B по детерминанту.");
        else
            Console.WriteLine("A и B равны по детерминанту.");

        Console.WriteLine("A == B: " + (matrixA == matrixB));
        Console.WriteLine("A != B: " + (matrixA != matrixB));
        Console.WriteLine("A.CompareTo(B): " + matrixA.CompareTo(matrixB));
        Console.WriteLine("A.Equals(B): " + matrixA.Equals(matrixB));
    }

    static void DemonstrateTypeCasting()
    {
        CheckMatrixACreated();

        double determinant = (double)matrixA;
        int size = (int)matrixA;

        Console.WriteLine("Приведение Matrix к double дает детерминант:");
        Console.WriteLine(determinant);

        Console.WriteLine("Приведение Matrix к int дает размер:");
        Console.WriteLine(size);

        if (matrixA)
            Console.WriteLine("Матрица A истинная: у нее есть обратная матрица.");
        else
            Console.WriteLine("Матрица A ложная: обратной матрицы нет.");
    }
}
