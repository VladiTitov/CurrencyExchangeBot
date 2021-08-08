using System.ComponentModel.DataAnnotations;

namespace DataAccess.DataBaseLayer
{
    public class UserState
    {
        [Key]
        public int Id { get; set; }
        public long UserId { get; set; }
        public int State { get; set; }
        public string City { get; set; }
        public string Currency { get; set; }
        public bool Buy { get; set; }
    }
}
