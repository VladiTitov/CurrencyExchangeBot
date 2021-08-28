using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.DataBaseLayer
{
    public class Currency : BaseDbModel
    {
        public string NameRus { get; set; }
        public string NameLat { get; set; }
        public string Logo { get; set; }
        public string Url { get; set; }


        public Currency() => 
            Quotations = new List<Quotation>();

        public ICollection<Quotation> Quotations { get; set; }
    }
}
