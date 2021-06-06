using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ArithmeticGunner.Views
{
    public partial class PlayingFieldView : UserControl
    {
        public PlayingFieldView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}