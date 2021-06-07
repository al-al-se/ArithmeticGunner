using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ArithmeticGunner.Views
{
    public partial class UserView : UserControl
    {
        public UserView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}