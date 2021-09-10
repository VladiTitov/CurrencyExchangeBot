using System.Collections.Generic;

namespace BusinessLogic.Database
{
    public class BaseClassDTO
    {
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string BranchAdr { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public List<string> Phone { get; set; }
        public string Buy { get; set; }
        public string Sale { get; set; }
    }
}
