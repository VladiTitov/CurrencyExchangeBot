using Telegram.Bot.Types.ReplyMarkups;

namespace BusinessLogic.MenuStucture.Models
{
    public class UserResponseModel
    {
        public string ResponseLabel { get; }
        public IReplyMarkup ResponseReplyMarkup { get; }

        public UserResponseModel(string label, IReplyMarkup replyMarkup) => 
            (ResponseLabel, ResponseReplyMarkup) = (label, replyMarkup);
    }
}
