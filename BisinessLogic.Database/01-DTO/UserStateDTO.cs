namespace BusinessLogic.Database
{
    public class UserStateDTO
    {
        public int Id { get; set; }
        public long UserId { get; set; }
        public int State { get; set; } = 0;
        public string City { get; set; }
        public string Currency { get; set; }
        public bool Buy { get; set; }
    }
}
