using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.DataBaseLayer
{
    public class Branch
    {
        [Key]
        public int Id { get; set; }
        public string AdrRus { get; set; }

        public ICollection<Quotation> Quotations { get; set; }
        public ICollection<Phone> Phones { get; set; }

        public Branch()
        {
            Quotations = new List<Quotation>();
            Phones = new List<Phone>();
        }

        public int? BankId { get; set; }
        public Bank Bank { get; set; }

        public int? CityId { get; set; }
        public City City { get; set; }
    }
}
