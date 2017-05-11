using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Linehaul_Helper.ViewModels;
using Linehaul_Helper.Helpers;
using System.ComponentModel;
using Linehaul_Helper.Models;

namespace Linehaul_Helper.CustomControls
{
    public partial class AxleWeight : StackLayout
    {
        public static readonly BindableProperty MaxLegalWeightProperty = BindableProperty.Create(nameof(MaxLegalWeight), typeof(int), typeof(AxleWeight), default(int));
        public int MaxLegalWeight
        {
            get { return (int)GetValue(MaxLegalWeightProperty); }
            private set
            {
                SetValue(MaxLegalWeightProperty, value);
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

        public static readonly BindableProperty PsiTableProperty = BindableProperty.Create(nameof(PsiTable), typeof(List<PsiKgPair>), typeof(AxleWeight), default(List<PsiKgPair>));
        public List<PsiKgPair> PsiTable
        {
            get { return (List<PsiKgPair>)GetValue(PsiTableProperty); }
            set
            {
                SetValue(PsiTableProperty, value);
            }
        }

        public static readonly BindableProperty SelectedIndexProperty = BindableProperty.Create(nameof(SelectedIndex), typeof(int), typeof(AxleWeight), default(int), BindingMode.TwoWay);
        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set
            {
                SetValue(SelectedIndexProperty, value);
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
