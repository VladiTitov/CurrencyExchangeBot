using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.DataBaseLayer
{
    public class Bank
    {
        [Key]
        public int Id { get; set; }
        public string NameRus { get; set; }

        //public Bank() =>
        //    Branches = new List<Branch>();
        public List<Branch> Branches { get; set; } = new List<Branch>();
    }
}
