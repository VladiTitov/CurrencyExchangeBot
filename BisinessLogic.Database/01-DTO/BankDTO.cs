using System.Collections.Generic;

namespace BusinessLogic.Database
{
    public class BankDTO
    {
        public int Id { get; set; }
        public string NameRus { get; set; }
        public List<BranchDTO> Branches { get; set; }
    }
}
