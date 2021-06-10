using System.ComponentModel.DataAnnotations;

namespace DataAccess.DataBaseLayer._01_Entities
{
    public class Quotation
    {
        [Key]
        public int Id { get; set; }
        public string Sale { get; set; }
        public string Buy { get; set; }

        public int? BranchId { get; set; }
        public Branch Branch { get; set; }

        public int? CurrencyId { get; set; }
        public Currency Currency { get; set; }
    }
}
