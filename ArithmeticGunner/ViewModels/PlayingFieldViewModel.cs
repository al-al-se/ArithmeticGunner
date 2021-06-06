
namespace ArithmeticGunner.ViewModels
{
    public class PlayingFieldViewModel : ViewModelBase
    {
        public PlayingFieldViewModel()
        {
            Model = new ArithmeticGunner.Models.PlayingFieldModel();
        }

        public ArithmeticGunner.Models.IPlayingField Model { get; protected init;}

        public void StartGame()
        {
            Model.StartGame();
        }

        protected bool ShotButtonPressed = false;

        public string ShotButtonImage => 
                ShotButtonPressed ? "/Assets/button_pressed.jpg" : "/Assets/button.jpg";

        public void OnShotButtonPressed()
        {
            ShotButtonPressed = true;
            System.Threading.Thread.Sleep(120);
            Model.AcceptAnswer();
            ShotButtonPressed = false;
        }

        public string MonitorImage
        {
            get{
                switch(Model.CurrentState)
                {
                    case ArithmeticGunner.Models.State.TargetFound:
                        return "/Assets/monitor_with_target.jpg";
                    case ArithmeticGunner.Models.State.TargetAttacts:
                        return "/Assets/monitor_attack.jpg"; 
                    case ArithmeticGunner.Models.State.WeGotHit:
                        return "/Assets/monitor_we_got_hit.jpg"; 
                    case ArithmeticGunner.Models.State.Shot:
                        return "/Assets/monitor_shot.jpg";
                    case ArithmeticGunner.Models.State.TargetHit:
                        return "/Assets/monitor_target_hit.jpg";
                    case ArithmeticGunner.Models.State.GameOver:
                        return "/Assets/game_over.jpg";
                    default:
                        return "/Assets/monitor.jpg";
                }
            }
        }
    }
}