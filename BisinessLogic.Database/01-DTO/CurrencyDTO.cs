using System.Collections.Generic;

namespace BusinessLogic.Database
{
    public class CurrencyDTO
    {
        public int Id { get; set; }
        public string NameRus { get; set; }
        public string NameLat { get; set; }
        public string Logo { get; set; }
        public string Url { get; set; }

        public List<QuotationDTO> Quotation { get; set; }
    }
}
