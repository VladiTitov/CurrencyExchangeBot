using System.ComponentModel.DataAnnotations;

namespace DataAccess.DataBaseLayer
{
    public class BaseDbModel
    {
        [Key]
        public int Id { get; set; }
    }
}
