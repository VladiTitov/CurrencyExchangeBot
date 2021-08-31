namespace BusinessLogic.Database
{ 
    public class QuotationDTO
    {
        public int Id { get; set; }
        public string Sale { get; set; }
        public string Buy { get; set; }

        public int BranchId { get; set; }
        public BranchDTO Branch { get; set; }

        public int CurrencyId { get; set; }
        public CurrencyDTO Currency { get; set; }
    }
}
