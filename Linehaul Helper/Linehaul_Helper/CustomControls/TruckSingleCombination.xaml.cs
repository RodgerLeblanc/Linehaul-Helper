using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Linehaul_Helper.CustomControls
{
    public partial class TruckSingleCombination : Frame
    {
        public static readonly BindableProperty NumberOfAxlesProperty = BindableProperty.Create(nameof(NumberOfAxles), typeof(List<int>), typeof(TruckSingleCombination), default(List<int>));
        public List<int> NumberOfAxles
        {
            get { return (List<int>)GetValue(NumberOfAxlesProperty); }
            set { SetValue(NumberOfAxlesProperty, value); }
        }

        public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(nameof(ImageSource), typeof(ImageSource), typeof(TruckSingleCombination), default(ImageSource));
        public ImageSource ImageSource
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        public TruckSingleCombination()
        {
            InitializeComponent();
            BindingContext = this;
        }
    }
}
