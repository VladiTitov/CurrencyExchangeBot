using Telegram.Bot.Types;

namespace BusinessLogic.MenuStucture.Models.Interfaces
{
    public interface IHandler
    {
        public Message Message { get; set; }
        public string Text { get; set; }

        public void Process();
    }
}
