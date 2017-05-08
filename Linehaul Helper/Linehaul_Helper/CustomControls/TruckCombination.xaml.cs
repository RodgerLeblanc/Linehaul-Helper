using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Linehaul_Helper.CustomControls
{
    public partial class TruckCombination : Image
    {
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(TruckCombination), default(ICommand));
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(string), typeof(TruckCombination), default(string));
        public string CommandParameter
        {
            get { return (string)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public TruckCombination()
        {
            InitializeComponent();
            BindingContext = this;
        }
    }
}
