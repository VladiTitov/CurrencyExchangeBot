namespace BusinessLogic.Database
{ 
    public class QuotationDTO
    {
        public int Id { get; set; }
        public string Sale { get; set; }
        public string Buy { get; set; }
        public int BranchDtoId { get; set; }
        public int CurrencyDtoId { get; set; }
    }
}
