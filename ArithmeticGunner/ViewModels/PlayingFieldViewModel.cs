using System.Collections.ObjectModel;
using Avalonia.Interactivity;

namespace ArithmeticGunner.ViewModels
{
    public class PlayingFieldViewModel : ViewModelBase
    {
        public PlayingFieldViewModel()
        {
            Model = new ArithmeticGunner.Models.PlayingFieldModel();
        }

        public ArithmeticGunner.Models.IPlayingField Model { get; protected init;}

        public void OnShotButtonPressed(object sender, RoutedEventArgs e)
        {
            
        }
    }
}