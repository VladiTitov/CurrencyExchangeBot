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
                Modify?.Invoke();
            }
        }

        public int CityId { get; set; }
        public int CurrencyId { get; set; }
        public int BankId { get; set; }
        public bool Buy { get; set; }

        public void StepUp() => this.StateId++;
        public void StepDown() => this.StateId--;

        public void UpdateState(int state) => this.StateId = state;
        public void UpdateCity(int cityId)
        {
            this.CityId = cityId;
            StepUp();
            Modify?.Invoke();
        }

        public void UpdateCurrency(int currencyId)
        {
            this.CurrencyId = currencyId;
            StepUp();
            Modify?.Invoke();
        }

        public void UpdateBank(int bankId)
        {
            this.BankId = bankId;
            StepUp();
            Modify?.Invoke();
        }

        public void UpdateBool(bool state)
        {
            this.Buy = state;
            StepUp();
            Modify?.Invoke();
        }
    }
}
