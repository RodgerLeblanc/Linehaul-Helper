using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Linehaul_Helper.Helpers
{
    static class ImageSourceHelper
    {
        static public ImageSource GetFromResource(string fileName)
        {
            return ImageSource.FromResource($"Linehaul_Helper.Images.{fileName}");
        }
    }
}
