// класс для ошибки когда обратная матрица не существует
public class MatrixOperationException : MatrixException
{
    public MatrixOperationException(string message) : base(message) //принимает сообщение об ошибке
    {
    }
}
