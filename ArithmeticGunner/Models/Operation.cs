using System;
using ReactiveUI;
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

        string CurrentOperationStr {get;}

        int Arg2 {get;}

        bool AcceptAnswer(int answer);

        int Level {get; set;}

        void PrepareValues();
    }

    public class OperationHandler : ReactiveObject, IOperationHandler
    {
        protected int _arg1 = 0;
        public int Arg1
        {
            get => _arg1;
            set => this.RaiseAndSetIfChanged(ref _arg1,value);
        }

        protected OperationType _currentOperation {get; set;}

        public OperationType CurrentOperation 
        {
            get => _currentOperation; 
            set
            {
                _currentOperation = value;
                switch(_currentOperation)
                {
                    case OperationType.Add: CurrentOperationStr = "+"; break;
                    case OperationType.Subtract: CurrentOperationStr = "-"; break;
                    case OperationType.Multiply: CurrentOperationStr = "*"; break;
                    case OperationType.Divide: CurrentOperationStr = "/"; break;
                    default: CurrentOperationStr = " "; break;
                }
            }
        }

        protected string _currentOperationStr = "";

        public string CurrentOperationStr
        {
            get =>  _currentOperationStr;
            set => this.RaiseAndSetIfChanged(ref  _currentOperationStr,value);
        }

        protected int _arg2 = 0;
        public int Arg2
        {
            get => _arg2;
            set => this.RaiseAndSetIfChanged(ref _arg2,value);
        }

        protected int _expectedResult {get; set;}

        public int Level {get; set;}

        public bool AcceptAnswer(int answer)
        {
            return _expectedResult == answer;
        }

        protected static Random _randomGenerator = new Random();

        protected int Generate()
        {
            return _randomGenerator.Next() % (Level * 2) + 1;
        }

        public void PrepareValues()
        {
            Arg2 = Generate() % 20;
            int iOperation = _randomGenerator.Next() % 4;
            CurrentOperation = (OperationType)iOperation;
            switch(CurrentOperation)
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
                    Arg1 = Generate() % 20; 
                    _expectedResult = Arg1 * Arg2;
                    break;
                case OperationType.Divide:
                    _expectedResult = Generate() % 20;
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