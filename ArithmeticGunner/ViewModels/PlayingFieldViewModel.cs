using System;
using ReactiveUI;
using Avalonia.Threading;
namespace ArithmeticGunner.ViewModels
{

    public class PlayingFieldViewModel : ViewModelBase
    {
        public PlayingFieldViewModel()
        {
            Model = new ArithmeticGunner.Models.PlayingFieldModel();

            this.WhenAnyValue(x => x.Model.OperationData.Arg1)
                            .Subscribe(x => Arg1 = x.ToString());

            this.WhenAnyValue(x => x.Model.OperationData.CurrentOperationStr)
                            .Subscribe(x => Operation = x);

            this.WhenAnyValue(x => x.Model.OperationData.Arg2)
                            .Subscribe(x => Arg2 = x.ToString());

            this.WhenAnyValue(x => x.Model.Level)
                            .Subscribe(x => Level = $"Level:{x.ToString()}");

            this.WhenAnyValue(x => x.Model.Lives)
                            .Subscribe(x => Lives = x);

            this.WhenAnyValue(x => x.Model.CurrentState)
                            .Subscribe(x => MonitorImage = StateToMonitorImage(x));

            _timer.Tick += new System.EventHandler(OnTimer);
            _timer.Interval = new System.TimeSpan(0,0,1);
        }

        public ArithmeticGunner.Models.IPlayingField Model { get; protected init;}

        protected DispatcherTimer _timer = new DispatcherTimer();

        public void StartGame()
        {
            Model.StartGame();
             _timer.Start();
        }

        public void OnTimer(object? sender, System.EventArgs e)
        {
            Model.OnTimer();
        }

        protected string _arg1 = "1";
        public string Arg1
        {
            get => _arg1;
            set => this.RaiseAndSetIfChanged(ref _arg1,value);
        }

        protected string _arg2 = "1";
        public string Arg2
        {
            get => _arg2;
            set => this.RaiseAndSetIfChanged(ref _arg2,value);
        }

        protected string _operation = "1";
        public string Operation
        {
            get => _operation;
            set => this.RaiseAndSetIfChanged(ref _operation,value);
        }

        protected string _level = "1";
        public string Level
        {
            get => _level;
            set => this.RaiseAndSetIfChanged(ref _level,value);
        }

        protected double _lives = 10;
        public double Lives
        {
            get => _lives;
            set => this.RaiseAndSetIfChanged(ref _lives,value);
        }



        protected bool _shotButtonPressed = false;
        protected bool ShotButtonPressed
        {
            get => _shotButtonPressed;
            set
            {
                ShotButtonImage = value ? "button_pressed.bmp" : "button.bmp";
                _shotButtonPressed = value;
            }
        }

        protected string _shotButtonImage = "button.bmp";

        public string ShotButtonImage
        {
            get => _shotButtonImage;
            set => this.RaiseAndSetIfChanged(ref _shotButtonImage,value);
        }

        public string Answer {get; set;} = "0";

        public void OnShotButtonPressed()
        {
            ShotButtonPressed = true;
            System.Threading.Thread.Sleep(1000);
            int ivalue;
            if (Int32.TryParse(Answer,out ivalue))
            {
                Model.AcceptAnswer(ivalue);
            }
            ShotButtonPressed = false;
        }

        protected string _monitorImage = "button.bmp";

        public string MonitorImage
        {
            get => _monitorImage;
            set => this.RaiseAndSetIfChanged(ref _monitorImage,value);
        }

        public string StateToMonitorImage(ArithmeticGunner.Models.State s)
        {
            switch(s)
            {
                case ArithmeticGunner.Models.State.TargetFound:
                    return "monitor_with_target.bmp";
                case ArithmeticGunner.Models.State.TargetAttacts:
                    return "monitor_attack.bmp"; 
                case ArithmeticGunner.Models.State.WeGotHit:
                    return "monitor_we_got_hit.bmp"; 
                case ArithmeticGunner.Models.State.Shot:
                    return "monitor_shot.bmp";
                case ArithmeticGunner.Models.State.TargetHit:
                    return "monitor_target_hit.bmp";
                case ArithmeticGunner.Models.State.GameOver:
                    return "game_over.bmp";
                default:
                    return "monitor.bmp";
            }
        }
    }
}