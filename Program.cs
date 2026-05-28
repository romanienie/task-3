using System; //подключаемая библиотека

class Program // класс
{
    static Matrix matrixA;
    static Matrix matrixB;

    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8; // поддержка русских символов

        bool isRunning = true; // флаг работы программы
// главный цыкл меню
        while (isRunning)
        {
            try
            {
                ShowMenu();

                Console.Write("выберите пункт: ");
                string choice = Console.ReadLine();
// обработка выбранного меню
                switch (choice)
                {
                        // создание случайной матрицы А
                    case "1":
                        matrixA = CreateRandomMatrix("A");
                        break;
// создание случайной матрицы B
                    case "2":
                        matrixB = CreateRandomMatrix("B");
                        break;
// вывод обоих
                    case "3":
                        ShowMatrices();
                        break;
// проверка
                    case "4":
                        CheckMatricesCreated();
                        Console.WriteLine("A + B:");
                        Console.WriteLine(matrixA + matrixB);
                        break;
// проверка матрицы
                    case "5":
                        CheckMatricesCreated();
                        Console.WriteLine("A * B:"); // перемножение
                        Console.WriteLine(matrixA * matrixB);
                        break;
// проверка
                    case "6":
                        CheckMatrixACreated();
                        Console.WriteLine("детерминант A:");
                        Console.WriteLine(matrixA.Determinant());
                        break;
// проверка
                    case "7":
                        CheckMatrixACreated();
                        Console.WriteLine("обратная матрица A:");
                        Console.WriteLine(matrixA.Inverse());
                        break;
// сравнение
                    case "8":
                        CheckMatricesCreated();
                        CompareMatrices();
                        break;
// создание копии
                    case "9":
                        CheckMatrixACreated();
                        Matrix copy = matrixA.CloneMatrix();

                        Console.WriteLine("копия матрицы A:");
                        Console.WriteLine(copy);
// проверка
                        Console.WriteLine("это разные объекты в памяти:");
                        Console.WriteLine(ReferenceEquals(matrixA, copy) ? "нет" : "да");
                        break;
// демонстрация приыедени типов
                    case "10":
                        DemonstrateTypeCasting();
                        break;
// завершение программы
                    case "0":
                        isRunning = false;
                        Console.WriteLine("программа завершена.");
                        break;
// если введен неизвестный пункт
                    default:
                        Console.WriteLine("такого пункта нет.");
                        break;
                }
            }
            catch (MatrixException ex)
            {
                Console.WriteLine("ошибка матрицы: " + ex.Message);
            }
            catch (FormatException)
            {
                Console.WriteLine("ошибка: нужно вводить число.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("неожиданная ошибка: " + ex.Message);
            }

            Console.WriteLine();
        }
    }
// отображение меню
    static void ShowMenu()
    {
        Console.WriteLine("===== МАТРИЧНЫЙ КАЛЬКУЛЯТОР =====");
        Console.WriteLine("1. создать случайную матрицу A");
        Console.WriteLine("2. создать случайную матрицу B");
        Console.WriteLine("3. показать матрицы");
        Console.WriteLine("4. A + B");
        Console.WriteLine("5. A * B");
        Console.WriteLine("6. детерминант A");
        Console.WriteLine("7. обратная матрица A");
        Console.WriteLine("8. сравнить A и B");
        Console.WriteLine("9. скопировать A");
        Console.WriteLine("10. показать приведение типов и true/false");
        Console.WriteLine("0. выход");
    }
// создание матрицы
    static Matrix CreateRandomMatrix(string name)
    {
        Console.Write($"введите размер матрицы {name}: ");
        int size = int.Parse(Console.ReadLine());

        Console.Write("введите минимальное случайное число: ");
        int min = int.Parse(Console.ReadLine());

        Console.Write("введите максимальное случайное число: ");
        int max = int.Parse(Console.ReadLine());

        Matrix matrix = new Matrix(size, min, max);

        Console.WriteLine($"матрица {name} создана:");
        Console.WriteLine(matrix);

        return matrix;
    }
//показывает матрицы 
    static void ShowMatrices()
    {
        Console.WriteLine("Матрица A:");
        Console.WriteLine(matrixA == null ? "матрица A еще не создана." : matrixA.ToString());

        Console.WriteLine("Матрица B:");
        Console.WriteLine(matrixB == null ? "матрица B еще не создана." : matrixB.ToString());
    }
// проверка
    static void CheckMatrixACreated()
    {
        if (matrixA == null)
            throw new MatrixOperationException("сначала создайте матрицу A.");
    }
// проверка 
    static void CheckMatricesCreated()
    {
        if (matrixA == null || matrixB == null)
            throw new MatrixOperationException("сначала создайте матрицы A и B.");
    }
// сравнивание матриц
    static void CompareMatrices()
    {
        Console.WriteLine("детерминант A: " + matrixA.Determinant());
        Console.WriteLine("детерминант B: " + matrixB.Determinant());

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
// приведение типов и использование лож и правда
    static void DemonstrateTypeCasting()
    {
        CheckMatrixACreated();

        double determinant = (double)matrixA;
        int size = (int)matrixA;

        Console.WriteLine("приведение Matrix к double дает детерминант:");
        Console.WriteLine(determinant);

        Console.WriteLine("приведение Matrix к int дает размер:");
        Console.WriteLine(size);

        if (matrixA)
            Console.WriteLine("Матрица A истинная: у нее есть обратная матрица.");
        else
            Console.WriteLine("Матрица A ложная: обратной матрицы нет.");
    }
}
