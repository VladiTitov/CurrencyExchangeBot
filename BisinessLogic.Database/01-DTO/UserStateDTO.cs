namespace BusinessLogic.Database
{
    public class UserStateDTO
    {
        public delegate void UserStateModify();

        public event UserStateModify Modify;

        public int Id { get; set; }
        private long _userId;
        public long UserId
        {
            get => _userId;
            set
            {
                _userId = value;
                Modify?.Invoke();
            }
        }

        public int PrevStateId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public int CurrencyId { get; set; }
        public int BranchId { get; set; }
        public int BankId { get; set; }
        public bool Buy { get; set; }

        public void StepDown()
        {
            this.PrevStateId = this.StateId;
            this.StateId--;
            Modify?.Invoke();
        }
        public void UpdateState(int state)
        {
            this.PrevStateId = this.StateId;
            this.StateId = state;
            Modify?.Invoke();
        }
        public void UpdateCity(int cityId)
        {
            this.PrevStateId = this.StateId;
            this.CityId = cityId;
            this.StateId = 2;
            Modify?.Invoke();
        }
        public void UpdateCurrency(int currencyId)
        {
            this.PrevStateId = this.StateId;
            this.CurrencyId = currencyId;
            this.StateId = 3;
            Modify?.Invoke();
        }
        public void UpdateBank(int bankId)
        {
            this.PrevStateId = this.StateId;
            this.BankId = bankId;
            this.StateId = 5;
            Modify?.Invoke();
        }
        public void UpdateBranch(int branchId)
        {
            this.PrevStateId = this.StateId;
            this.BranchId = branchId;
            this.StateId = 6;
            Modify?.Invoke();
        }
        public void UpdateBool(bool state)
        {
            this.PrevStateId = this.StateId;
            this.Buy = state;
            this.StateId = 4;
            Modify?.Invoke();
        }
    }
}
