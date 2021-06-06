using ReactiveUI;
using Avalonia.Threading;
namespace ArithmeticGunner.ViewModels
{

    public class PlayingFieldViewModel : ViewModelBase
    {
        public PlayingFieldViewModel()
        {
            Model = new ArithmeticGunner.Models.PlayingFieldModel();
            
            _timer.Tick += new System.EventHandler(OnTimer);
            _timer.Interval = new System.TimeSpan(0,0,1);
        }

        public ArithmeticGunner.Models.IPlayingField Model { get; protected init;}

        protected DispatcherTimer _timer = new DispatcherTimer();

        public void StartGame()
        {
            Model.StartGame();
            MonitorImage = StateToMonitorImage(Model.CurrentState);
             _timer.Start();
        }

        public void OnTimer(object? sender, System.EventArgs e)
        {
            Model.OnTimer();
            MonitorImage = StateToMonitorImage(Model.CurrentState);
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

        public void OnShotButtonPressed()
        {
            ShotButtonPressed = true;
            System.Threading.Thread.Sleep(1000);
            Model.AcceptAnswer();
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