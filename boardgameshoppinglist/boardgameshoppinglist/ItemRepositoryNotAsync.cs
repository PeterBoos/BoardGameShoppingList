﻿using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;

namespace boardgameshoppinglist
{
    public class ItemRepositoryNotAsync
    {
        private SQLiteConnection _connection;

        public ItemRepositoryNotAsync()
        {
            _connection = new SQLiteConnection(DatabaseFilePathRetriever.GetPath());
            _connection.CreateTable<Item>();
        }

        public IEnumerable<Item> GetItems()
        {
            return (from t in _connection.Table<Item>()
                    where t.Purchased == false
                    select t).ToList();
        }

        public Item GetItem(int id)
        {
            return _connection.Table<Item>().FirstOrDefault(t => t.Id == id);
        }

        public void DeleteItem(int id)
        {
            _connection.Delete<Item>(id);
        }

        public void AddItem(Item item)
        {
            _connection.Insert(item);
        }

        public void UpdateItem(Item item)
        {
            _connection.Update(item);
        }

        public void SetPurchased(int id)
        {
            var item = GetItem(id);
            item.Purchased = true;
            item.PurchasedDate = DateTime.Now;
            UpdateItem(item);
        }
    }
}