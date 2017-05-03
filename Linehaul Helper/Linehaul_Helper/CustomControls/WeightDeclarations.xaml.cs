using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Linehaul_Helper.CustomControls
{
    public partial class WeightDeclarations : StackLayout
    {
        public static readonly BindableProperty LightPsiProperty = BindableProperty.Create(nameof(LightPsi), typeof(int), typeof(WeightDeclarations), 0);
        public int LightPsi
        {
            get { return (int)GetValue(LightPsiProperty); }
            set { SetValue(LightPsiProperty, value); }
        }

        public static readonly BindableProperty LightWeightProperty = BindableProperty.Create(nameof(LightWeight), typeof(int), typeof(WeightDeclarations), 0);
        public int LightWeight
        {
            get { return (int)GetValue(LightWeightProperty); }
            set { SetValue(LightWeightProperty, value); }
        }

        public static readonly BindableProperty HeavyPsiProperty = BindableProperty.Create(nameof(HeavyPsi), typeof(int), typeof(WeightDeclarations), 0);
        public int HeavyPsi
        {
            get { return (int)GetValue(HeavyPsiProperty); }
            set { SetValue(HeavyPsiProperty, value); }
        }

        public static readonly BindableProperty HeavyWeightProperty = BindableProperty.Create(nameof(HeavyWeight), typeof(int), typeof(WeightDeclarations), 0);
        public int HeavyWeight
        {
            get { return (int)GetValue(HeavyWeightProperty); }
            set { SetValue(HeavyWeightProperty, value); }
        }

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(MainPageBox), null);
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(MainPageBox), default(object));
        public object CommandParameter
        {
            get { return this.GetValue(CommandParameterProperty); }
            set { this.SetValue(CommandParameterProperty, value); }
        }

        public WeightDeclarations()
        {
            InitializeComponent();
            BindingContext = this;
        }
    }
}
