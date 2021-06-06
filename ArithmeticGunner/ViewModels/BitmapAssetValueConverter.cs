using System;
using System.Globalization;
using Avalonia.Data.Converters;
using System.Reflection;
using Avalonia.Platform;
using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Imaging;

namespace ArithmeticGunner.ViewModels
{
    public class BitmapAssetValueConverter: IValueConverter
    {
        public static BitmapAssetValueConverter Instance = new BitmapAssetValueConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            if (value is string rawUri && 
             (targetType.IsAssignableFrom(typeof(Bitmap)) || 
                targetType.IsAssignableFrom(typeof(IImage))))
            {
                Uri uri;

                // Allow for assembly overrides
                if (rawUri.StartsWith("avares://"))
                {
                    uri = new Uri(rawUri);
                }
                else
                {
                    string assemblyName = Assembly.GetEntryAssembly().GetName().Name;
                    uri = new Uri($"avares://{assemblyName}/Assets/{rawUri}");
                }

                var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();
                var asset = assets.Open(uri);

                return new Bitmap(asset);
            }

            throw new NotSupportedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}