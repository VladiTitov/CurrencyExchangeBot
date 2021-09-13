using System.Collections.Generic;

namespace BusinessLogic.Database
{
    public class BranchDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Adr { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public List<QuotationDTO> Quotations { get; set; }
        public List<PhoneDTO> Phones { get; set; }

        public int BankId { get; set; }
        public BankDTO Bank { get; set; }
        public int CityId { get; set; }
        public CityDTO City { get; set; }
    }
}
