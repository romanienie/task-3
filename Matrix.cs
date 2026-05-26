using System;
using System.Text;

public class Matrix : IComparable<Matrix>, ICloneable
{
    private double[,] data;

    public int Size { get; }

    public Matrix(int size)
    {
        if (size <= 0)
            throw new MatrixSizeException("Размер матрицы должен быть больше нуля.");

        Size = size;
        data = new double[size, size];
    }

    public Matrix(int size, int min, int max) : this(size)
    {
        if (min >= max)
            throw new MatrixOperationException("Минимальное значение должно быть меньше максимального.");

        Random random = new Random();

        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                data[i, j] = random.Next(min, max);
            }
        }
    }

    public Matrix(double[,] array)
    {
        if (array == null)
            throw new MatrixOperationException("Массив не может быть null.");

        if (array.GetLength(0) != array.GetLength(1))
            throw new MatrixSizeException("Матрица должна быть квадратной.");

        Size = array.GetLength(0);
        data = new double[Size, Size];

        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                data[i, j] = array[i, j];
            }
        }
    }

    public double this[int row, int column]
    {
        get
        {
            CheckIndex(row, column);
            return data[row, column];
        }
        set
        {
            CheckIndex(row, column);
            data[row, column] = value;
        }
    }

    private void CheckIndex(int row, int column)
    {
        if (row < 0 || row >= Size || column < 0 || column >= Size)
            throw new MatrixOperationException("Индекс выходит за границы матрицы.");
    }

    private static void CheckSameSize(Matrix a, Matrix b)
    {
        if (a == null || b == null)
            throw new MatrixOperationException("Матрица не может быть null.");

        if (a.Size != b.Size)
            throw new MatrixSizeException("Размеры матриц должны совпадать.");
    }

    public static Matrix operator +(Matrix a, Matrix b)
    {
        CheckSameSize(a, b);

        Matrix result = new Matrix(a.Size);

        for (int i = 0; i < a.Size; i++)
        {
            for (int j = 0; j < a.Size; j++)
            {
                result[i, j] = a[i, j] + b[i, j];
            }
        }

        return result;
    }

    public static Matrix operator *(Matrix a, Matrix b)
    {
        CheckSameSize(a, b);

        Matrix result = new Matrix(a.Size);

        for (int i = 0; i < a.Size; i++)
        {
            for (int j = 0; j < a.Size; j++)
            {
                for (int k = 0; k < a.Size; k++)
                {
                    result[i, j] += a[i, k] * b[k, j];
                }
            }
        }

        return result;
    }

    public double Determinant()
    {
        double[,] temp = CopyArray(data);
        double det = 1;

        for (int i = 0; i < Size; i++)
        {
            int pivot = i;

            for (int row = i + 1; row < Size; row++)
            {
                if (Math.Abs(temp[row, i]) > Math.Abs(temp[pivot, i]))
                    pivot = row;
            }

            if (Math.Abs(temp[pivot, i]) < 0.000001)
                return 0;

            if (pivot != i)
            {
                SwapRows(temp, i, pivot);
                det *= -1;
            }

            det *= temp[i, i];

            for (int row = i + 1; row < Size; row++)
            {
                double factor = temp[row, i] / temp[i, i];

                for (int column = i; column < Size; column++)
                {
                    temp[row, column] -= factor * temp[i, column];
                }
            }
        }

        return det;
    }

    public Matrix Inverse()
    {
        double det = Determinant();

        if (Math.Abs(det) < 0.000001)
            throw new MatrixNotInvertibleException("Обратная матрица не существует, потому что детерминант равен нулю.");

        double[,] left = CopyArray(data);
        double[,] right = new double[Size, Size];

        for (int i = 0; i < Size; i++)
        {
            right[i, i] = 1;
        }

        for (int i = 0; i < Size; i++)
        {
            int pivot = i;

            for (int row = i + 1; row < Size; row++)
            {
                if (Math.Abs(left[row, i]) > Math.Abs(left[pivot, i]))
                    pivot = row;
            }

            if (Math.Abs(left[pivot, i]) < 0.000001)
                throw new MatrixNotInvertibleException("Обратная матрица не существует.");

            if (pivot != i)
            {
                SwapRows(left, i, pivot);
                SwapRows(right, i, pivot);
            }

            double divisor = left[i, i];

            for (int column = 0; column < Size; column++)
            {
                left[i, column] /= divisor;
                right[i, column] /= divisor;
            }

            for (int row = 0; row < Size; row++)
            {
                if (row == i)
                    continue;

                double factor = left[row, i];

                for (int column = 0; column < Size; column++)
                {
                    left[row, column] -= factor * left[i, column];
                    right[row, column] -= factor * right[i, column];
                }
            }
        }

        return new Matrix(right);
    }

    private static double[,] CopyArray(double[,] source)
    {
        int size = source.GetLength(0);
        double[,] copy = new double[size, size];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                copy[i, j] = source[i, j];
            }
        }

        return copy;
    }

    private static void SwapRows(double[,] array, int row1, int row2)
    {
        int size = array.GetLength(0);

        for (int column = 0; column < size; column++)
        {
            double temp = array[row1, column];
            array[row1, column] = array[row2, column];
            array[row2, column] = temp;
        }
    }

    public int CompareTo(Matrix other)
    {
        if (other == null)
            return 1;

        return Determinant().CompareTo(other.Determinant());
    }

    public override bool Equals(object obj)
    {
        if (obj is not Matrix other)
            return false;

        if (Size != other.Size)
            return false;

        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                if (Math.Abs(data[i, j] - other.data[i, j]) > 0.000001)
                    return false;
            }
        }

        return true;
    }

    public override int GetHashCode()
    {
        int hash = Size;

        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                hash = hash * 31 + data[i, j].GetHashCode();
            }
        }

        return hash;
    }

    public Matrix CloneMatrix()
    {
        return new Matrix(data);
    }

    public object Clone()
    {
        return CloneMatrix();
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                sb.Append($"{data[i, j],8:F2}");
            }

            sb.AppendLine();
        }

        return sb.ToString();
    }

    public static bool operator >(Matrix a, Matrix b)
    {
        CheckSameSize(a, b);
        return a.Determinant() > b.Determinant();
    }

    public static bool operator <(Matrix a, Matrix b)
    {
        CheckSameSize(a, b);
        return a.Determinant() < b.Determinant();
    }

    public static bool operator >=(Matrix a, Matrix b)
    {
        CheckSameSize(a, b);
        return a.Determinant() >= b.Determinant();
    }

    public static bool operator <=(Matrix a, Matrix b)
    {
        CheckSameSize(a, b);
        return a.Determinant() <= b.Determinant();
    }

    public static bool operator ==(Matrix a, Matrix b)
    {
        if (ReferenceEquals(a, b))
            return true;

        if (a is null || b is null)
            return false;

        return a.Equals(b);
    }

    public static bool operator !=(Matrix a, Matrix b)
    {
        return !(a == b);
    }

    public static explicit operator double(Matrix matrix)
    {
        if (matrix == null)
            throw new MatrixOperationException("Матрица не может быть null.");

        return matrix.Determinant();
    }

    public static explicit operator int(Matrix matrix)
    {
        if (matrix == null)
            throw new MatrixOperationException("Матрица не может быть null.");

        return matrix.Size;
    }

    public static bool operator true(Matrix matrix)
    {
        return matrix != null && Math.Abs(matrix.Determinant()) > 0.000001;
    }

    public static bool operator false(Matrix matrix)
    {
        return matrix == null || Math.Abs(matrix.Determinant()) < 0.000001;
    }
}
