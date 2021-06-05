using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ArithmeticGunner.Views
{
    public partial class PlayingField : UserControl
    {
        public PlayingField()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}