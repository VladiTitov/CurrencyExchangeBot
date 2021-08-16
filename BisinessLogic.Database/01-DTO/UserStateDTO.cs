﻿namespace BusinessLogic.Database
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

        public string CityId { get; set; }
        public string CurrencyId { get; set; }
        public string BankId { get; set; }
        public bool Buy { get; set; }

        public void StepUp() => this.StateId++;
        public void StepDown() => this.StateId--;

        public void UpdateCity(string cityId)
        {
            this.CityId = cityId;
            StepUp();
            Modify?.Invoke();
        }

        public void UpdateCurrency(string currencyId)
        {
            this.CurrencyId = currencyId;
            StepUp();
            Modify?.Invoke();
        }

        public void UpdateBank(string bankId)
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