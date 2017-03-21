using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Linehaul_Helper.CustomControls
{
    public partial class CustomViewCell : ViewCell
    {
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(CustomViewCell), default(string));
        public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(nameof(ImageSource), typeof(ImageSource), typeof(CustomViewCell), default(ImageSource));
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(Command), typeof(CustomViewCell), default(Command));
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(CustomViewCell), default(object));

        public CustomViewCell()
        {
            InitializeComponent();
        }

        public string Title
        {
            get
            {
                return (string)this.GetValue(TitleProperty);
            }
            set
            {
                this.SetValue(TitleProperty, value);
            }
        }

        public ImageSource ImageSource
        {
            get
            {
                return (ImageSource)this.GetValue(ImageSourceProperty);
            }
            set
            {
                this.SetValue(ImageSourceProperty, value);
            }
        }

        public Command Command
        {
            get
            {
                return (Command)this.GetValue(CommandProperty);
            }
            set
            {
                this.SetValue(CommandProperty, value);
            }
        }

        public object CommandParameter
        {
            get
            {
                return this.GetValue(CommandParameterProperty);
            }
            set
            {
                this.SetValue(CommandParameterProperty, value);
            }
        }

        protected override void OnTapped()
        {
            base.OnTapped();

            if (this.Command != null)
            {
                this.Command.Execute(this.CommandParameter ?? this);
            }
        }
    }
}
