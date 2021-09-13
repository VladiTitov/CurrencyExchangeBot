using System.ComponentModel.DataAnnotations;

namespace DataAccess.DataBaseLayer
{
    public class Phone : BaseDbModel
    {
        public string PhoneNum { get; set; }

        public int? BranchId { get; set; }
        public Branch Branch { get; set; }
    }
}
