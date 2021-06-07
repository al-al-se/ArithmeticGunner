using System;
using System.Collections.Generic;
using System.Text;

namespace ArithmeticGunner.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public PlayingFieldViewModel PlayingFieldInst {get; init;}

        public UserViewModel UserVM {get; init;}

        public MainWindowViewModel()
        {
            PlayingFieldInst = new PlayingFieldViewModel();
            UserVM = new UserViewModel();
            PlayingFieldInst.StartGame();
        }
    }
}
