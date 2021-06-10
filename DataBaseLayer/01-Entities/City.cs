using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.DataBaseLayer._01_Entities
{
    public class City
    {
        [Key]
        public int Id { get; set; }
        public string NameRus { get; set; }
        public string Url { get; set; }


        public City() =>
            Branches = new List<Branch>();

        public ICollection<Branch> Branches { get; set; }
    }
}
