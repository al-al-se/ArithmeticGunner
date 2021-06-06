using System;
using System.Collections.Generic;
using System.Text;

namespace ArithmeticGunner.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public PlayingField PlayingFieldInst {get;} = new PlayingField();
    }
}
