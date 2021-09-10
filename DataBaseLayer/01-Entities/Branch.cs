using System.Collections.Generic;

namespace DataAccess.DataBaseLayer
{
    public class Branch : BaseDbModel
    {
        public string Name { get; set; }
        public string Adr { get; set; }

        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public ICollection<Quotation> Quotations { get; set; }
        public ICollection<Phone> Phones { get; set; }

        public int? BankId { get; set; }
        public virtual Bank Bank { get; set; }

        public int? CityId { get; set; }
        public virtual City City { get; set; }
    }
}
