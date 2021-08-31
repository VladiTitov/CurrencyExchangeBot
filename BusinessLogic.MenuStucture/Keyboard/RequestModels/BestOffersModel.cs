using BusinessLogic.MenuStucture.Constants;

namespace BusinessLogic.MenuStucture.Keyboard.RequestModels
{
    public class BestOffersModel
    {
        public int BankId { get; set; }
        public string BankName { get; set; }
        public int BankAdrId { get; set; }
        public string BankAdr { get; set; }
        public string BankOffer { get; set; }

        public BestOffersModel(int bankNameId, string bankName, int bankAdrId, string bankAdr, string bankOffer)
        {
            BankId = bankNameId;
            BankName = bankName;
            BankAdrId = bankAdrId;
            BankAdr = bankAdr;
            BankOffer = bankOffer;
        }

        public override string ToString() => 
            $"{MenuEmojiConstants.Shock}  {BankOffer}, {MenuEmojiConstants.Location}  {BankAdr}, {MenuEmojiConstants.Bank} {BankName}";
    }
}
