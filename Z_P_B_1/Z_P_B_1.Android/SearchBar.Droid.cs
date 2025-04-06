using Android.Content;
using Android.Content.Res;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Z_P_B_1;

[assembly: ExportRenderer(typeof(Xamarin.Forms.SearchBar), typeof(MyApp.Renderers.SearchBarIconColorCustomRenderer))]
namespace MyApp.Renderers
{
    public class SearchBarIconColorCustomRenderer : SearchBarRenderer
    {
        public SearchBarIconColorCustomRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);
            var icon = Control?.FindViewById(Context.Resources.GetIdentifier("android:id/search_mag_icon", null, null));
            Color a = (Color)App.Current.Resources["textColor"];   
            (icon as ImageView)?.SetColorFilter(a.B == 1  ? Color.White.ToAndroid() : Color.Black.ToAndroid());

            if (Control != null)
            {
                Control.BackgroundTintList = ColorStateList.ValueOf(a.B == 0 ? Android.Graphics.Color.Black : Android.Graphics.Color.White);
            }
        }
    }
}
