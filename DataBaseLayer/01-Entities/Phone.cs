using System.ComponentModel.DataAnnotations;

namespace DataAccess.DataBaseLayer
{
    public class Phone
    {
        [Key]
        public int Id { get; set; }
        public string PhoneNum { get; set; }

        public int? BranchId { get; set; }
        public Branch Branch { get; set; }
    }
}
