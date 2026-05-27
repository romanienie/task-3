// класс ошибки дл неправильного размера матрицы
public class MatrixSizeException : MatrixException
{
    public MatrixSizeException(string message) : base(message) // конструктор принимает сообщение об ошибке
    {
    }
}
