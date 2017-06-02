using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;
using System.Threading.Tasks;

/*
 *      ATTENTION!
 * 
 *  This class is replaced by 
 *  ItemRepositoryNotAsync.cs
 *  since async doesn't seem to 
 *  work very well...
 */

namespace boardgameshoppinglist
{
    public class ItemDatabase
    {
        readonly SQLiteAsyncConnection database;
        private static object collisionLock = new object();

        public ItemDatabase()
        {
            database = new SQLiteAsyncConnection(DatabaseFilePathRetriever.GetPath());
            database.CreateTableAsync<Item>().Wait();
        }

        //Get all ToDoItems
        public async Task<List<Item>> GetItems()
        {
            return await database.Table<Item>().ToListAsync(); //Careful with ToList calls on databases!
        }

        //Get specific ToDoItem
        public async Task<Item> GetItem(int id)
        {
            return await database.Table<Item>().Where(tdi => tdi.Id == id).FirstOrDefaultAsync();
        }

        //Delete specific ToDoItem
        public async Task DeleteItem(Item item)
        {
            await database.DeleteAsync(item);
        }

        //Add new ToDoItem to DB
        public async Task AddItem(Item item)
        {
            await database.InsertAsync(item);
        }
    }
}