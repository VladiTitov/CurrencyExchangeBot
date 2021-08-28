using System.Linq;
using BusinessLogic.MenuStucture.Models.Interfaces;
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

        public void Process(IEventHandler menuEventHandler) => 
            menuEventHandler.MessageProcess(Text.Split("  ").LastOrDefault());
    }
}
