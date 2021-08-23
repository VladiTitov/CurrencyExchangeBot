using Telegram.Bot.Types.ReplyMarkups;

namespace BusinessLogic.MenuStucture.Models
{
    public class UserResponseModel
    {
        public string ResponseLabel { get; set; }
        public IReplyMarkup ResponseReplyMarkup { get; set; }

        public UserResponseModel(string label, IReplyMarkup replyMarkup) => 
            (ResponseLabel, ResponseReplyMarkup) = (label, replyMarkup);
    }
}
