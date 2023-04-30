﻿using FinalProject.Data;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject.Models
{
    public class UserModel : IEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePicture { get; set; }
        public string PhoneNumber { get; set; }
    }
}
