using System.Collections.Generic;

namespace DataAccess.DataBaseLayer
{
    public class Bank : BaseDbModel
    {
        public string NameRus { get; set; }

        public ICollection<Branch> Branches { get; set; }
    }
}
