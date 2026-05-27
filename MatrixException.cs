using System; // подключаемая библиотека
// класс всех ошибок матрицы
public class MatrixException : Exception
{
    public MatrixException(string message) : base(message) // принимает текст ошибок
    {
    }
}
