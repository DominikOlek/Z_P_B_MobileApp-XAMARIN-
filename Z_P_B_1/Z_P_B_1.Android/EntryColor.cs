using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Z_P_B_1;

[assembly: ExportRenderer(typeof(Xamarin.Forms.Entry), typeof(MyApp.MyRenderers.MyEntryRenderer))]
namespace MyApp.MyRenderers
{
    public class MyEntryRenderer : EntryRenderer 
    {
        public MyEntryRenderer(Context context) : base(context)
        {
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Xamarin.Forms.Color a = (Xamarin.Forms.Color)App.Current.Resources["textColor"];
                Control.BackgroundTintList = ColorStateList.ValueOf(a.B == 0 ? Android.Graphics.Color.Black:Android.Graphics.Color.White) ;
            }
        }
    }
}