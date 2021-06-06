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
        string Arg1 {get;}

        string CurrentOperation {get;}

        string Arg2 {get;}

        bool AcceptAnswer(string answer);

        int Level {get; set;}

        void PrepareValues();
    }

    public class OperationHandler : IOperationHandler
    {
        protected int _arg1 {get; set;}

        public string Arg1 => _arg1.ToString();

        protected OperationType _currentOperation {get; set;}

        public string CurrentOperation => _currentOperation.ToString("g");

        protected int _arg2 {get; set;}

        public string Arg2 => _arg2.ToString();

        protected int _expectedResult {get; set;}

        public int Level {get; set;}

        public bool AcceptAnswer(string answer)
        {
            int iValue;
            if(!Int32.TryParse(answer, out iValue)) return false;
            return AcceptAnswer(iValue);
        }

        bool AcceptAnswer(int answer)
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
            _arg2 = Generate();
            int iOperation = _randomGenerator.Next() % 4;
            _currentOperation = (OperationType)iOperation;
            switch(_currentOperation)
            {
                case OperationType.Add:
                    _arg1 = Generate(); 
                    _expectedResult = _arg1 + _arg2;
                    break;
                case OperationType.Subtract:
                    _expectedResult = Generate();
                    _arg1 = _arg2 + _expectedResult;
                    break;
                case OperationType.Multiply:
                    _arg1 = Generate(); 
                    _expectedResult = _arg1 * _arg2;
                    break;
                case OperationType.Divide:
                    _expectedResult = Generate();
                    _arg1 = _arg2 * _expectedResult;
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