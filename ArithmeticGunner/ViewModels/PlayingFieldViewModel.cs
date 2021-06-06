
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
                ShotButtonPressed ? "button_pressed.jpg" : "button.jpg";

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
                        return "monitor_with_target.jpg";
                    case ArithmeticGunner.Models.State.TargetAttacts:
                        return "monitor_attack.jpg"; 
                    case ArithmeticGunner.Models.State.WeGotHit:
                        return "monitor_we_got_hit.jpg"; 
                    case ArithmeticGunner.Models.State.Shot:
                        return "monitor_shot.jpg";
                    case ArithmeticGunner.Models.State.TargetHit:
                        return "monitor_target_hit.jpg";
                    case ArithmeticGunner.Models.State.GameOver:
                        return "game_over.jpg";
                    default:
                        return "monitor.jpg";
                }
            }
        }
    }
}