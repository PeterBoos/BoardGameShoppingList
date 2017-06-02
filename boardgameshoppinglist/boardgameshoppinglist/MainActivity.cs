using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using System.Text;

namespace boardgameshoppinglist
{
    [Activity(Label = "Boardgames Shoppinglist", MainLauncher = true, Icon = "@drawable/Icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
            SetContentView (Resource.Layout.Main);

            var btnList = FindViewById<Button>(Resource.Id.btnList);
            var btnAddItem = FindViewById<Button>(Resource.Id.btnAddItem);
            var btnAbout = FindViewById<Button>(Resource.Id.btnAbout);

            btnList.Click += BtnList_Click;
            btnAddItem.Click += BtnAddItem_Click;
            btnAbout.Click += BtnAbout_Click;
        }

        private void BtnAbout_Click(object sender, System.EventArgs e)
        {
            var intent = new Intent(this, typeof(AboutActivity));
            StartActivity(intent);
        }

        private void BtnList_Click(object sender, System.EventArgs e)
        {
            var intent = new Intent(this, typeof(ItemListActivity));
            StartActivity(intent);
        }

        private void BtnAddItem_Click(object sender, System.EventArgs e)
        {
            var intent = new Intent(this, typeof(AddItemActivity));            
            StartActivity(intent);
        }
    }
}

