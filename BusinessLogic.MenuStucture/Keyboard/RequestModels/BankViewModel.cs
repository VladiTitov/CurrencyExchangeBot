using System.Collections.Generic;
using BusinessLogic.MenuStucture.Constants;

namespace BusinessLogic.MenuStucture.Keyboard.RequestModels
{
    public class BankViewModel
    {
        public string BankName { get; }
        public string BranchName { get; }
        public string BranchAdr { get; }
        public string BankBuy { get; }
        public string BankSale { get; }
        public List<string> BankPhones { get; }

        public BankViewModel(string bankName, string branchName, string branchAdr, string bankBuy, string bankSale, List<string> bankPhones)
        {
            BankName = bankName;
            BranchName = branchName;
            BranchAdr = branchAdr;
            BankBuy = bankBuy;
            BankSale = bankSale;
            BankPhones = bankPhones;
        }

        public override string ToString()
        {
            return $"{MenuEmojiConstants.Bank} {BankName}[.]({BanksImagesLinks.ImagesLinks[BankName]})\n" +
                   $"{MenuEmojiConstants.Location} {BranchName}\n" +
                   $"{MenuEmojiConstants.Location} {BranchAdr}\n" +
                   $"{MenuEmojiConstants.Shock} Покупка: {BankBuy}, Продажа: {BankSale}\n" +
                   $"{MenuEmojiConstants.Phone}\n{string.Join('\n', BankPhones)}";
        }
    }
}
