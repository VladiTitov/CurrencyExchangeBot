using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.DataBaseLayer
{
    public class Branch
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Adr { get; set; }

        public List<Quotation> Quotations { get; set; } = new List<Quotation>();
        public List<Phone> Phones { get; set; } = new List<Phone>();

        public Branch()
        {
            Quotations = new List<Quotation>();
            Phones = new List<Phone>();
        }

        public int BankId { get; set; }
        public Bank Bank { get; set; }

        public int CityId { get; set; }
        public City City { get; set; }
    }
}
