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
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Android.Graphics;

namespace boardgameshoppinglist
{
    [Activity(Label = "Details")]
    public class ItemDetailsActivity : Activity
    {
        static ItemRepositoryNotAsync database;
        static HttpClient client = new HttpClient();

        Item item;

        ImageView imgMain;
        TextView txtName;
        TextView txtDescription;
        TextView txtMechanics;
        TextView txtPlayers;
        TextView txtPlayingTime;
        TextView txtReleaseYear;
        TextView txtBggRating;
        TextView txtAvarageRating;
        TextView txtBggRank;
        Button btnAddBggId;
        Button btnPurchased;
        Button btnDelete;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ItemDetails);

            database = new ItemRepositoryNotAsync();

            var itemId = Intent.GetIntExtra("ItemId", 0);
            if (itemId == 0)
            {
                // Handle error
            }

            item = database.GetItem(itemId);

            imgMain = FindViewById<ImageView>(Resource.Id.imageViewDetails);
            txtName = FindViewById<TextView>(Resource.Id.txtName);
            txtDescription = FindViewById<TextView>(Resource.Id.txtDescription);
            txtMechanics = FindViewById<TextView>(Resource.Id.txtMechanics);
            txtPlayers = FindViewById<TextView>(Resource.Id.txtPlayers);
            txtPlayingTime = FindViewById<TextView>(Resource.Id.txtPlayingtime);
            txtReleaseYear = FindViewById<TextView>(Resource.Id.txtRealeaseYear);
            txtBggRating = FindViewById<TextView>(Resource.Id.txtBggRating);
            txtAvarageRating = FindViewById<TextView>(Resource.Id.txtAvarageRating);
            txtBggRank = FindViewById<TextView>(Resource.Id.txtBggRank);

            btnAddBggId = FindViewById<Button>(Resource.Id.btnAddBggId);
            btnPurchased = FindViewById<Button>(Resource.Id.btnSetPurchased);
            btnDelete = FindViewById<Button>(Resource.Id.btnDelete);

            if (string.IsNullOrWhiteSpace(item.BggId))
            {
                imgMain.SetImageResource(Resource.Drawable.die);
                txtName.Text = item.Name;
                btnAddBggId.Visibility = ViewStates.Visible;
            }
            else
            {
                LoadDataFromBgg(item.BggId);
                btnAddBggId.Visibility = ViewStates.Gone;
            }

            btnAddBggId.Click += BtnAddBggId_Click;
            btnPurchased.Click += BtnPurchased_Click;
            btnDelete.Click += BtnDelete_Click;
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            var alert = new AlertDialog.Builder(this);
            alert.SetMessage($"Are you sure you want to delete {item.Name}?");
            alert.SetPositiveButton("Yes", (s, a) => {
                database.DeleteItem(item.Id);
                base.OnBackPressed();
            });
            alert.SetNegativeButton("No", (s, a) => { });
            alert.Create().Show();
        }

        private void BtnPurchased_Click(object sender, EventArgs e)
        {
            var alert = new AlertDialog.Builder(this);
            alert.SetMessage($"Have you purchased the game {item.Name}?");
            alert.SetPositiveButton("Yes", (s, a) => {
                database.SetPurchased(item.Id);
                base.OnBackPressed();
            });
            alert.SetNegativeButton("No", (s, a) => { });
            alert.Create().Show();
        }

        private void BtnAddBggId_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(EditItemActivity));
            intent.PutExtra("ItemId", item.Id);
            StartActivity(intent);
        }

        static BggInfoItem GetFromBggApi(string bggId)
        {
            var client = new WebClient { Encoding = Encoding.UTF8 };
            var response = client.DownloadString("http://bgg-json.azurewebsites.net/thing/" + bggId);
            var boardGameData = JsonConvert.DeserializeObject<BggInfoItem>(response);

            if (!boardGameData.Image.Contains("https:"))
            {
                boardGameData.Image = $"https:{boardGameData.Image}";
            }
            
            return boardGameData;
        }

        private void LoadDataFromBgg(string bggId)
        {
            var bggInfo = GetFromBggApi(bggId);            

            imgMain.SetImageBitmap(GetImage(bggInfo.Image));
            txtName.Text = bggInfo.Name;
            txtDescription.Text = bggInfo.Description;
            txtMechanics.Text = SortMechanics(bggInfo.Mechanics);
            txtPlayers.Text = $"Players: {bggInfo.MinPlayers} - {bggInfo.MaxPlayers}";
            txtPlayingTime.Text = $" | Play time: {bggInfo.PlayingTime} min";
            txtReleaseYear.Text = $"Published {bggInfo.YearPublished}";
            txtBggRating.Text = $"BGG Rating: {bggInfo.BggRating}";
            txtAvarageRating.Text = $" | Avg Rating: {bggInfo.AverageRating}";
            txtBggRank.Text = $"Rank: {bggInfo.Rank}";
        }

        private Bitmap GetImage(string url)
        {
            Bitmap imageBitmap = null;
            using (var wc = new WebClient())
            {
                var imageBytes = wc.DownloadData(url);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
                else
                {
                    imageBitmap = BitmapFactory.DecodeResource(Resources, Resource.Drawable.die);
                }
            }

            return imageBitmap;
        }

        private string SortMechanics(string[] mechanics)
        {
            var sb = new StringBuilder();

            if (mechanics != null && mechanics.Length > 0)
            {
                for (var i = 0; i < mechanics.Length; i++)
                {
                    if (i == 0) { sb.Append($"{mechanics[i]}"); }

                    sb.Append($" | {mechanics[i]}");
                }
            }
            else
            {
                return "Mechanics unknown";
            }

            return sb.ToString();
        }
    }
}