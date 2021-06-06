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

        string CurrentOperation {get;}

        string Arg2 {get;}

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

        }

        public void StartGame()
        {

        }

    }
}