using BusinessLogic.MenuStucture.Constants;

namespace BusinessLogic.MenuStucture.Keyboard.RequestModels
{
    public class BestOffersModel
    {
        public int BankId { get; }
        public string BankName { get; }
        public int BankAdrId { get; }
        public string BankAdr { get; }
        public string BankOffer { get; }

        public BestOffersModel(int bankNameId, string bankName, int bankAdrId, string bankAdr, string bankOffer)
        {
            BankId = bankNameId;
            BankName = bankName;
            BankAdrId = bankAdrId;
            BankAdr = bankAdr;
            BankOffer = bankOffer;
        }

        public override string ToString() => 
            $"{MenuEmojiConstants.Shock}  {BankOffer}, {MenuEmojiConstants.Location}  {BankAdr}";
    }
}
