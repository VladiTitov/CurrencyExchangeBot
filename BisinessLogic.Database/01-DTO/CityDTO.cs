using System.Collections.Generic;

namespace BusinessLogic.Database
{
    public class CityDTO
    {
        public int Id { get; set; }
        public string NameRus { get; set; }
        public string NameLat { get; set; }
        public string Url { get; set; }
        public List<BranchDTO> Branches { get; set; }
    }
}
