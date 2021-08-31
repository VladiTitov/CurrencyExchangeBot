namespace BusinessLogic.Database
{
    public class PhoneDTO
    {
        public int Id { get; set; }
        public string PhoneNum { get; set; }

        public int? BranchId { get; set; }
        public BranchDTO Branch { get; set; }
    }
}
