using System.ComponentModel.DataAnnotations;

namespace DataAccess.DataBaseLayer
{
    public class UserState : BaseDbModel
    {
        public long UserId { get; set; }
        public int PrevStateId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public int CurrencyId { get; set; }
        public int BranchId { get; set; }
        public int BankId { get; set; }
        public bool Buy { get; set; }
    }
}
