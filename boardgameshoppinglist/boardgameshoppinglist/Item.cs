using SQLite;
using System;

namespace boardgameshoppinglist
{
    [Table("Item")]
    public class Item
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string BggId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Priority { get; set; }
        public bool Purchased { get; set; }
        public DateTime? PurchasedDate { get; set; }

        [Ignore]
        public string ReadableTitle
        {
            get
            {
                return $"{Id}. {Name}; Priority: {Priority}";
            }
        }
    }
}