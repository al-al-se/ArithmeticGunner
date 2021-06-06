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

    public class Monitor
    {
        protected State _state = State.TargetNotFound;

        public string StrState => _state.ToString("g");
        protected IOperationHandler _operationHandler = new OperationHandler();

        public string Arg1 => _operationHandler.Arg1;

        public string CurrentOperation => _operationHandler.CurrentOperation;

        public string Arg2 => _operationHandler.Arg2;

        public void AcceptAnswer(string answer)
        {

        }

        void StartGame()
        {
            
        }

    }
}