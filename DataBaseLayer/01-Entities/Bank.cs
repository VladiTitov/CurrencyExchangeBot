using System.Collections.Generic;

namespace DataAccess.DataBaseLayer
{
    public class Bank : BaseDbModel
    {
        public string NameRus { get; set; }

        public Bank() =>
            Branches = new List<Branch>();
        public List<Branch> Branches { get; set; }
    }
}
