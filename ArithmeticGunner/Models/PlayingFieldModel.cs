using System;
namespace ArithmeticGunner.Models
{
    public enum State
    {
        NotStarted,
        TargetNotFound,
        TargetFound,

        Shot,

        TargetHit,

        TargetAttacts,

        WeGotHit,

        GameOver
    }

    public interface IPlayingField
    {
        int Arg1 {get;}

        State CurrentState {get;}

        string CurrentOperation {get;}

        int Arg2 {get;}

        int Level {get;}

        int Lives {get;}

        void StartGame();

        void AcceptAnswer(int answer);

        void OnTimer();
    }

    public class PlayingFieldModel :  IPlayingField
    {
        public State CurrentState  {get;  protected set; }

        protected IOperationHandler _operationHandler = new OperationHandler();

        public int Arg1 => _operationHandler.Arg1;

        public string CurrentOperation => _operationHandler.CurrentOperation;

        public int Arg2 => _operationHandler.Arg2;

        public void TargetHit()
        {
            CurrentState = State.TargetHit;
            _operationHandler.Level = ++Level;
        }

        public int Answer {get; set;} = 0;

        public void AcceptAnswer(int answer)
        {
            if (CurrentState == State.TargetFound ||
                CurrentState == State.TargetAttacts ||
                CurrentState == State.WeGotHit)
            {
                Answer = answer;
                CurrentState = State.Shot;
            }
        }

        public void GetShotResult()
        {
            if (_operationHandler.AcceptAnswer(Answer))
            {
                TargetHit();
            } else
            {
                CurrentState = State.TargetFound;
            }
        }

        private int __internalLevel = 1;

        public int Level 
        {
            get {return __internalLevel;}
            set {__internalLevel = value;}
        }

        private int __internalLives = 10;

        public int Lives
        {
            get {return __internalLives;}
            set {__internalLives = value; }
        }

        public int TimeoutSeconds {get; protected set;}

        protected Random _randomGenerator = new Random();

        public void StartGame()
        {
            Lives = 10;
            Level = 1;
            _operationHandler.Level = Level;
            CurrentState = State.TargetNotFound;
            TimeoutSeconds = _randomGenerator.Next() % 10 + 1;
        }

        public void NewTargetFound()
        {
            _operationHandler.PrepareValues();
            CurrentState = State.TargetFound;
            TimeoutSeconds = _randomGenerator.Next() % 20 + 20;           
        }

        public void WeGotHit()
        {
            if (--Lives > 0)
            {
                CurrentState = State.WeGotHit;
            } else{
                CurrentState = State.GameOver;
            }
        }

        public void OnTimer()
        {
            --TimeoutSeconds;
            switch(CurrentState)
            {
                case State.TargetNotFound:
                    if (TimeoutSeconds == 0)
                        NewTargetFound(); 
                    break;
                case State.TargetFound: 
                    if (TimeoutSeconds == 0)
                        CurrentState = State.TargetAttacts;
                    break;
                case State.TargetAttacts:
                    WeGotHit(); 
                    break;
                case State.WeGotHit:
                    CurrentState = State.TargetFound;
                    TimeoutSeconds = _randomGenerator.Next() % 10 + 10;
                    break;
                case State.Shot: 
                    GetShotResult(); 
                    break;
                case State.TargetHit:
                    CurrentState = State.TargetNotFound;
                    TimeoutSeconds = _randomGenerator.Next() % 10 + 1;
                    break;
                default:
                    break;
            }
        }

    }
}