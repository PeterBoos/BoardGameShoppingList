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
    [Activity(Label = "Add a new Item")]
    public class AddItemActivity : Activity
    {
        static ItemRepositoryNotAsync database;

        EditText txtName;
        EditText txtPrio;
        EditText txtBggId;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.AddItem);
            
            database = new ItemRepositoryNotAsync();

            txtName = FindViewById<EditText>(Resource.Id.editTextName);
            txtPrio = FindViewById<EditText>(Resource.Id.editTextPrio);
            txtBggId = FindViewById<EditText>(Resource.Id.edittextBggId);
            var btnSubmit = FindViewById<Button>(Resource.Id.btnSubmit);

            btnSubmit.Click += BtnSubmit_Click;
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            var item = new Item
            {
                Name = txtName.Text,
                Priority = Convert.ToInt32(txtPrio.Text),
                CreatedDate = DateTime.Now,
                BggId = txtBggId.Text
            };

            try
            {
                database.AddItem(item);
            }
            catch (Exception ex)
            {
                var p = ex;
            }
            
            var items = database.GetItems().ToList();

            txtName.Text = "";
            txtPrio.Text = "";
            txtBggId.Text = "";

            Toast.MakeText(this, $"Item created. Items in DB: {items.Count}", ToastLength.Long).Show();
        }
    }
}