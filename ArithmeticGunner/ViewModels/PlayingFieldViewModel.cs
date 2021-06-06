
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
                ShotButtonPressed ? "button_pressed.bmp" : "button.bmp";

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
}