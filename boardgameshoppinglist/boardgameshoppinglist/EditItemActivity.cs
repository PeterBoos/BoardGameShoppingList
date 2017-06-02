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
    [Activity(Label = "EditItemActivity")]
    public class EditItemActivity : Activity
    {
        static ItemRepositoryNotAsync database;

        Item item;

        EditText txtName;
        EditText txtPrio;
        EditText txtBggId;
        Button btnSubmit;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.EditItem);

            database = new ItemRepositoryNotAsync();

            var itemId = Intent.GetIntExtra("ItemId", 0);
            item = database.GetItem(itemId);

            txtName = FindViewById<EditText>(Resource.Id.editTextEditName);
            txtPrio = FindViewById<EditText>(Resource.Id.editTextEditPrio);
            txtBggId = FindViewById<EditText>(Resource.Id.editTextEditBggId);
            btnSubmit = FindViewById<Button>(Resource.Id.btnEditSubmit);

            txtName.Text = item.Name;
            txtPrio.Text = item.Priority.ToString();
            txtBggId.Text = item.BggId;

            btnSubmit.Click += BtnSubmit_Click;
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            item.Name = txtName.Text;
            item.Priority = Convert.ToInt32(txtPrio.Text);
            item.BggId = txtBggId.Text;

            database.UpdateItem(item);

            Toast.MakeText(this, $"Item updated!", ToastLength.Long).Show();

            base.OnBackPressed();
        }
    }
}