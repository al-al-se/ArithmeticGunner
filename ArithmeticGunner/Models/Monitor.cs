using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Avalonia.Threading;

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
        string Arg1 {get;}

        State CurrentState {get;}

        string Arg2 {get;}

        int Level {get;}

        int Lives {get;}

        void StartGame();

        void AcceptAnswer(string answer);
    }

    public class PlayingField : IPlayingField, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = null;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private State __internalState = State.NotStarted;
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

        public void TargetHit()
        {
            CurrentState = State.TargetHit;
            _operationHandler.Level = ++Level;
        }

        public string _acceptedAnswer {get; protected set;}

        public void AcceptAnswer(string answer)
        {
            if (CurrentState == State.TargetFound)
            {
                _acceptedAnswer = answer;
                CurrentState = State.Shot;
            }
        }

        public void GetShotResult()
        {
            if (_operationHandler.AcceptAnswer(_acceptedAnswer))
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
            set {__internalLevel = value; OnPropertyChanged("Level");}
        }

        private int __internalLives = 10;

        public int Lives
        {
            get {return __internalLives;}
            set {__internalLives = value; OnPropertyChanged("Lives");}
        }

        protected DispatcherTimer _timer = new DispatcherTimer();

        public PlayingField()
        {
            _timer.Tick += new System.EventHandler(OnTimer);
            _timer.Interval = new System.TimeSpan(0,0,1);
        }

        public int TimeoutSeconds {get; protected set;}

        protected Random _randomGenerator = new Random();

        void ResetTimeout()
        {
            switch(CurrentState)
            {
                case State.TargetNotFound:
                    TimeoutSeconds = _randomGenerator.Next() % 10 + 1;
                    break;
                case State.TargetFound:
                    TimeoutSeconds = _randomGenerator.Next() % 10 + 10;
                    break;
                default:
                    TimeoutSeconds = 1;
                    break;
            }
        }

        public void StartGame()
        {
            Lives = 10;
            Level = 1;
            _operationHandler.Level = Level;
            CurrentState = State.TargetNotFound;
            ResetTimeout();
            _timer.Start();
        }

        public void NewTargetFound()
        {
            _operationHandler.PrepareValues();
            CurrentState = State.TargetFound;
            ResetTimeout();
        }

        public void WeGotHit()
        {
            if (--Lives > 0)
            {
                CurrentState = State.WeGotHit;
                ResetTimeout();
            } else{
                CurrentState = State.GameOver;
                _timer.Stop();
            }
        }

        public void OnTimer(object? sender, System.EventArgs e)
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
                        WeGotHit(); 
                    break;
                case State.WeGotHit:
                    CurrentState = State.TargetFound;
                    break;
                case State.Shot: 
                    GetShotResult(); 
                    break;
                case State.TargetHit:
                    CurrentState = State.TargetNotFound;
                    ResetTimeout();
                    break;
                default:
                    ResetTimeout();
                    break;
            }
        }

    }
}