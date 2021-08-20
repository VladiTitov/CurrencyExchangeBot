using System.Linq;
using BusinessLogic.MenuStucture.Models.Interfaces;
using BusinessLogic.MenuStucture.Services;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace BusinessLogic.MenuStucture.Models
{
    public class MessageHandler : IHandler
    {
        public Message Message { get; set; }
        public string Text { get; set; }

        public MessageHandler(MessageEventArgs arg)
        {
            Message = arg.Message;
            Text = arg.Message.Text;
        }

        public void Process()
        {
            MenuEventHandler menuEventHandler = new MenuEventHandler();
            string text = Text.Split("  ").LastOrDefault();
            menuEventHandler.MessageProcess(text);
        }
    }
}
