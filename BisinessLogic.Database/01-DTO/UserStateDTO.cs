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

        private int _state = 0;
        public int State
        {
            get => _state;
            set
            {
                _state = value;
                Modify?.Invoke();
            }
        }
        public string City { get; set; }
        public string Currency { get; set; }
        public bool Buy { get; set; }

        public void StepUp() => this.State++;
        public void StepDown() => this.State--;

        public void UpdateCity(string city)
        {
            this.City = city;
            StepUp();
            Modify?.Invoke();
        }

        public void UpdateCurrency(string currency)
        {
            this.Currency = currency;
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
