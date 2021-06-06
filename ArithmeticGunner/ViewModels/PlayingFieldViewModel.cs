using System.Collections.ObjectModel;

namespace ArithmeticGunner.ViewModels
{
    public class PlayingField : ViewModelBase
    {
        public PlayingField()
        {
            Model = new ArithmeticGunner.Models.PlayingField();
        }

        public ArithmeticGunner.Models.IPlayingField Model { get; protected set;}
    }
}