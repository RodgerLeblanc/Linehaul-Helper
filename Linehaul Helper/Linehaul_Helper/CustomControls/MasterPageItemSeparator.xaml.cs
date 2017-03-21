using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Linehaul_Helper.CustomControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterPageItemSeparator : BoxView
    {
        public static readonly BindableProperty SeparatorColorProperty = BindableProperty.Create(nameof(SeparatorColor), typeof(Color), typeof(MasterPageItemSeparator), (Color)Application.Current.Resources["DicomOrange"]);
        public Color SeparatorColor
        {
            get { return (Color)GetValue(SeparatorColorProperty); }
            set { SetValue(SeparatorColorProperty, value); }
        }

        public MasterPageItemSeparator()
        {
            InitializeComponent();

            BindingContext = this;
        }
    }
}
