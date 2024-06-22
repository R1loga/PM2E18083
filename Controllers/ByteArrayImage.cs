using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace PM2E18083.Controllers
{
    public class ByteArrayImage : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            ImageSource imageSource = null;

            if (value != null)
            {
                byte[] imageByte = (byte[])value;
                var stream = new MemoryStream(imageByte);
                imageSource = ImageSource.FromStream(() => stream);
            }

            return imageSource;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
