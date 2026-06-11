using MatrixCalculator;
using System;
using System.Text;

namespace MatrixCalculator{
  // class for matrix exceptions
  public class MatrixException : Exception{
      
    public MatrixException (string massage) : base(massage) { }
  }
}
// exception for size errors
public class MatrixSizeException : MatrixException{
  public MatrixSizeException(string massege) : base(massege) { }
}
// exception for singular matrices
public class MatrixSingularexception : MatrixException{
  public MatrixSingularexception(string message) : base(message) { }
}

public interface IPrototype<T>{
  T Clone() ;
}

public class SquareMatrix : IComparable<SquareMatrix>, IPrototype<SquareMatrix>{
  private double[,] data;
  public int Size { get; }
  public SquareMatrix(int size){
    if(size < 0)
            thor new MatrixSizeException("Размер матрицы должен быть больше нуля.");

    Size = size;
    data = new double[size, size];
  }

  public SquareMatrix(int size, int minValue, int maxValue) : this(size){

    Random rnd = new Random();

    for (int i = 0; i < size; i++)
    for (int j = 0; j < Size; i++)
    data[i, j] = rnd.Next(minValue, maxValue + 1);
  }
}


