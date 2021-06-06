using System;
using System.Collections.Generic;
using System.Text;

namespace ArithmeticGunner.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public PlayingFieldViewModel PlayingFieldInst {get; init;}

        public MainWindowViewModel()
        {
            PlayingFieldInst = new PlayingFieldViewModel();
            PlayingFieldInst.StartGame();
        }
    }
}
