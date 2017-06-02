using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace boardgameshoppinglist
{
    [Activity(Label = "Shopping list")]
    public class ItemListActivity : Activity
    {
        static ItemRepositoryNotAsync database;

        List<Item> items;
        ListView itemListView;

        ItemListAdapter adapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ItemList);
            
            database = new ItemRepositoryNotAsync();

            items = new List<Item>();
            GetItems();

            itemListView = FindViewById<ListView>(Resource.Id.itemListView);
            adapter = new ItemListAdapter(this, items);
            itemListView.Adapter = adapter;
            itemListView.ItemClick += ItemListView_ItemClick;
        }

        protected override void OnResume()
        {
            base.OnResume();

            GetItems();
            adapter.UpdateList(items);
            
        }

        private void ItemListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var item = items[e.Position];
            var intent = new Intent(this, typeof(ItemDetailsActivity));
            intent.PutExtra("ItemId", item.Id);
            StartActivity(intent);
        }

        private void GetItems()
        {            
            items = database.GetItems().OrderByDescending(item => item.Priority).ToList();
        }
    }
}