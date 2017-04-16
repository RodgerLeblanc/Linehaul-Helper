using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Diagnostics;
using System.ComponentModel;

namespace Linehaul_Helper.CustomControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterPageItemCustomView : StackLayout
    {
        public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(nameof(ImageSource), typeof(ImageSource), typeof(MasterPageItemCustomView), null);
        public ImageSource ImageSource
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        public static readonly BindableProperty ImageSizeProperty = BindableProperty.Create(nameof(ImageSize), typeof(int), typeof(MasterPageItemCustomView), 30);
        public int ImageSize
        {
            get { return (int)GetValue(ImageSizeProperty); }
            set { SetValue(ImageSizeProperty, value); }
        }

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(MasterPageItemCustomView), null);
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(MasterPageItemCustomView), null);
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(MasterPageItemCustomView), default(object));
        public object CommandParameter
        {
            get { return this.GetValue(CommandParameterProperty); }
            set { this.SetValue(CommandParameterProperty, value); }
        }

        public MasterPageItemCustomView()
        {
            InitializeComponent();

            BindingContext = this;

            //GestureRecognizers.Add(new TapGestureRecognizer()
            //{
            //    Command = new Command(() =>
            //    {
            //        if (this.Command != null)
            //        {
            //            this.Command.Execute(this.CommandParameter ?? null);
            //        }
            //    })
            //});
        }
    }
}
