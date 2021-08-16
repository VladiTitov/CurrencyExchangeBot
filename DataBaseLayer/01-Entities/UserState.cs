using System.ComponentModel.DataAnnotations;

namespace DataAccess.DataBaseLayer
{
    public class UserState
    {
        [Key]
        public int Id { get; set; }
        public long UserId { get; set; }
        public int StateId { get; set; }
        public string CityId { get; set; }
        public string CurrencyId { get; set; }
        public string BankId { get; set; }
        public bool Buy { get; set; }
    }
}
