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

        private int _stateId = 0;
        public int StateId
        {
            get => _stateId;
            set
            {
                _stateId = value;
            }
        }

        public int CityId { get; set; }
        public int CurrencyId { get; set; }
        public int BankId { get; set; }
        public bool Buy { get; set; }

        public void StepDown()
        {
            this.StateId--;
            Modify?.Invoke();
        }

        public void UpdateState(int state)
        {
            this.StateId = state;
            Modify?.Invoke();
        }
        public void UpdateCity(int cityId)
        {
            this.CityId = cityId;
            this.StateId = 2;
            Modify?.Invoke();
        }

        public void UpdateCurrency(int currencyId)
        {
            this.CurrencyId = currencyId;
            this.StateId = 3;
            Modify?.Invoke();
        }

        public void UpdateBank(int bankId)
        {
            this.BankId = bankId;
            this.StateId = 5;
            Modify?.Invoke();
        }

        public void UpdateBool(bool state)
        {
            this.Buy = state;
            this.StateId = 4;
            Modify?.Invoke();
        }
    }
}
