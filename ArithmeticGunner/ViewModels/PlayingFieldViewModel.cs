
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

        public void OnShotButtonPressed()
        {
            Model.AcceptAnswer();
        }
    }
}