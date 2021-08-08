namespace BisinessLogic.Database
{
    public class BranchDTO
    {
        public int Id { get; set; }
        public string AdrRus { get; set; }
        public int BankDtoId { get; set; }
        public BankDTO Bank { get; set; }
        public int CityDtoId { get; set; }
        public CityDTO City { get; set; }
    }
}
