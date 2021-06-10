﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.DataBaseLayer._01_Entities
{
    public class Bank
    {
        [Key]
        public int Id { get; set; }
        public string NameRus { get; set; }

        public Bank() =>
            Branches = new List<Branch>();
        public ICollection<Branch> Branches { get; set; }
    }
}