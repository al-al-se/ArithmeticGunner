using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ArithmeticGunner.Models
{
    public enum State
    {
        TargetNotFound,
        TargetFound,

        Shot,

        TargetHit,

        TargetAttacts,

        WeGotHit
    }

    public interface IMonitor
    {
        string Arg1 {get;}

        State CurrentState {get;}

        string Arg2 {get;}

        int Level {get;}

        int Lives {get;}

        void StartGame();

        void AcceptAnswer(string answer);
    }

    public class Monitor : IMonitor, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = null;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private State __internalState = State.TargetNotFound;
        public State CurrentState 
        {
            get
            {
                return __internalState;
            }
            protected set
            {
                __internalState = value;
                OnPropertyChanged("CurrentState ");
                if (__internalState == State.TargetFound)
                {
                    OnPropertyChanged("Arg1");
                    OnPropertyChanged("CurrentOperation");
                    OnPropertyChanged("Arg2");
                }
            }
        }

        public string StrState => __internalState.ToString("g");
        protected IOperationHandler _operationHandler = new OperationHandler();

        public string Arg1 => _operationHandler.Arg1;

        public string CurrentOperation => _operationHandler.CurrentOperation;

        public string Arg2 => _operationHandler.Arg2;

        public void AcceptAnswer(string answer)
        {
            if (CurrentState == State.TargetFound)
            {
                CurrentState = State.Shot;
                System.Threading.Thread.Sleep(1000);
                if (_operationHandler.AcceptAnswer(answer))
                {
                    CurrentState = State.TargetHit;
                } else
                {
                    CurrentState = State.TargetFound;
                }
            }
        }

        private int __internalLevel = 1;

        public int Level 
        {
            get {return __internalLevel;}
            set {__internalLevel = value; OnPropertyChanged("Level");}
        }

        private int __internalLives = 10;

        public int Lives
        {
            get {return __internalLives;}
            set {__internalLives = value; OnPropertyChanged("Lives");}
        }

        public void StartGame()
        {
            Lives = 10;
            Level = 1;
            CurrentState = State.TargetNotFound;
        }

    }
}