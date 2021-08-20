using System.Collections.Generic;

namespace DataAccess.DataBaseLayer
{
    public class BaseEntity
    {
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string BranchAdr { get; set; }
        public string[] Phone { get; set; }
        public string Buy { get; set; }
        public string Sale { get; set; }
    }
}
