using System.Collections.Generic;

namespace DataAccess.DataBaseLayer
{
    public class Branch : BaseDbModel
    {
        public string Name { get; set; }
        public string Adr { get; set; }

        public List<Quotation> Quotations { get; set; }
        public List<Phone> Phones { get; set; }

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
