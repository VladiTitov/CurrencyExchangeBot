using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.DataBaseLayer
{
    public class Currency
    {
        [Key]
        public int Id { get; set; }
        public string NameRus { get; set; }
        public string NameLat { get; set; }
        public string Url { get; set; }


        public Currency() => 
            Quotations = new List<Quotation>();

        public ICollection<Quotation> Quotations { get; set; }
    }
}
