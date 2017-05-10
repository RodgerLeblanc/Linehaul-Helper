using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Linehaul_Helper.ViewModels;
using Linehaul_Helper.Helpers;

namespace Linehaul_Helper.CustomControls
{
    public partial class AxleWeight : StackLayout
    {
        //public static readonly BindableProperty NumberOfAxleProperty = BindableProperty.Create(nameof(NumberOfAxle), typeof(int), typeof(AxleWeight), default(int));
        //public int NumberOfAxle
        //{
        //    get { return (int)GetValue(NumberOfAxleProperty); }
        //    set
        //    {
        //        SetValue(NumberOfAxleProperty, value);
        //    }
        //}

        public static readonly BindableProperty PsiProperty = BindableProperty.Create(nameof(Psi), typeof(int), typeof(AxleWeight), default(int), BindingMode.TwoWay);
        public int Psi
        {
            get { return (int)GetValue(PsiProperty); }
            set
            {
                SetValue(PsiProperty, value);
            }
        }

        public static readonly BindableProperty PositionProperty = BindableProperty.Create(nameof(Position), typeof(string), typeof(AxleWeight), default(string));
        public string Position
        {
            get { return (string)GetValue(PositionProperty); }
            set
            {
                SetValue(PositionProperty, value);
            }
        }

        public static readonly BindableProperty WeightProperty = BindableProperty.Create(nameof(Weight), typeof(int), typeof(AxleWeight), default(int));
        public int Weight
        {
            get { return (int)GetValue(WeightProperty); }
            private set
            {
                SetValue(WeightProperty, value);
            }
        }

        public AxleWeight()
        {
            InitializeComponent();
        }
    }
}
