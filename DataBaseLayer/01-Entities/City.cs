using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.DataBaseLayer
{
    public class City : BaseDbModel
    {
        public string NameRus { get; set; }
        public string NameLat { get; set; }
        public string Url { get; set; }


        public City() =>
            Branches = new List<Branch>();

        public ICollection<Branch> Branches { get; set; }
    }
}
