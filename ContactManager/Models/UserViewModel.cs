﻿using System.ComponentModel.DataAnnotations;

namespace ContactManager.Models
{
    public class UserViewModel
    {
        public string? Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Married { get; set; }
        public string? Phone { get; set; }
        public decimal Salary { get; set; }
    }
}
