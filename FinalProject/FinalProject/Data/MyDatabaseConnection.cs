using FinalProject.Models;
using FinalProject.Views;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FinalProject.Data
{
    public interface IEntity
    {
        int Id { get; set; }
    }

    public class MyDatabaseConnection
    {
        private SQLiteConnection _database;
        public MyDatabaseConnection(string dbPath)
        {
            _database = new SQLiteConnection(dbPath);
            _database.CreateTable<UserModel>();
            _database.CreateTable<AdModel>();
        }

        public List<T> GetItems<T>() where T : IEntity, new()
        {
            return _database.Table<T>().ToList();
        }

        public int SaveItem<T>(T item) where T : IEntity
        {
            if (item.Id != 0)
            {
                return _database.Update(item);
            }
            else
            {
                return _database.Insert(item);
            }
        }

        public int DeleteItem<T>(T item) where T : IEntity
        {
            return _database.Delete(item);
        }
    }

}
