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
    public class BggInfoItem
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Thumbnail { get; set; }
        public string MinPlayers { get; set; }
        public string MaxPlayers { get; set; }
        public string PlayingTime { get; set; }
        public string YearPublished { get; set; }
        public string[] Mechanics { get; set; }
        public string[] Designers { get; set; }
        public string[] Publishers { get; set; }
        public string[] Artists { get; set; }
        public string BggRating { get; set; }
        public string AverageRating { get; set; }
        public string Rank { get; set; }
        public BggExpansion[] Expansions { get; set; }

        //public int Id { get; set; }
        //public int ItemId { get; set; }

        //public string GameId { get; set; }
        //public string Name { get; set; }
        //public string Description { get; set; }
        //public string ImageUrl { get; set; }
        //public string ThumbNailUrl { get; set; }
        //public int MinPlayers { get; set; }
        //public int MaxPlayers { get; set; }
        //public int PlayingTime { get; set; }
        //public string Mechanics { get; set; }
        //public bool IsExplansion { get; set; }
        //public string YearPublished { get; set; }
        //public double BggRating { get; set; }
        //public double AvarageRating { get; set; }
        //public int Rank { get; set; }
        //public string Designers { get; set; }
        //public string Publishers { get; set; }
        //public string Expansions { get; set; }
    }
}