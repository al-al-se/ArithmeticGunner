using System;
namespace ArithmeticGunner.Models
{
    public enum OperationType
    {
        Add,
        Subtract,
        Multiply,
        Divide
    }

    public interface IOperationHandler
    {
        int Arg1 {get;}

        string CurrentOperation {get;}

        int Arg2 {get;}

        bool AcceptAnswer(int answer);

        int Level {get; set;}

        void PrepareValues();
    }

    public class OperationHandler : IOperationHandler
    {
        public int Arg1 {get; protected set;}

        protected OperationType _currentOperation {get; set;}

        public string CurrentOperation
        {
            get
            {
                switch(_currentOperation)
                {
                    case OperationType.Add: return "+";
                    case OperationType.Subtract: return "-";
                    case OperationType.Multiply: return "*";
                    case OperationType.Divide: return "/";
                    default: return " ";
                }
            }
        }

        public int Arg2 {get; protected set;}

        protected int _expectedResult {get; set;}

        public int Level {get; set;}


        public bool AcceptAnswer(int answer)
        {
            return _expectedResult == answer;
        }

        protected static Random _randomGenerator = new Random();

        protected int Generate()
        {
            return _randomGenerator.Next() % (Level * 4) + 1;
        }

        public void PrepareValues()
        {
            Arg2 = Generate();
            int iOperation = _randomGenerator.Next() % 4;
            _currentOperation = (OperationType)iOperation;
            switch(_currentOperation)
            {
                case OperationType.Add:
                    Arg1 = Generate(); 
                    _expectedResult = Arg1 + Arg2;
                    break;
                case OperationType.Subtract:
                    _expectedResult = Generate();
                    Arg1 = Arg2 + _expectedResult;
                    break;
                case OperationType.Multiply:
                    Arg1 = Generate(); 
                    _expectedResult = Arg1 * Arg2;
                    break;
                case OperationType.Divide:
                    _expectedResult = Generate();
                    Arg1 = Arg2 * _expectedResult;
                    break;
            }
        }

        public OperationHandler()
        {
            Level = 1;
            PrepareValues();
        }
    }
}