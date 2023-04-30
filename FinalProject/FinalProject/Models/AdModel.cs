using FinalProject.Data;
using SQLite;
using System;
using Xamarin.Forms;

namespace FinalProject.Models
{
    public class AdModel : IEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        //public string Pictures { get; set; }
        public byte[] ImageData { get; set; }
        public decimal? Price { get; set; }
        public string Location { get; set; }
        public bool Available { get; set; }
        public DateTime Date { get; set; }
        public int? UserId { get; set; }
        [Ignore]
        public ImageSource ImageSource { get; set; }
    }
}