using System;
using System.Linq;
using BusinessLogic.Logger;
using BusinessLogic.MenuStucture.Models.Interfaces;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace BusinessLogic.MenuStucture.Models
{
    public class MessageHandler : IHandler
    {
        public Message Message { get; set; }
        public string Text { get; set; }

        [Obsolete ("This property is obsolete")]
        public MessageHandler(MessageEventArgs arg)
        {
            Message = arg.Message;
            Text = arg.Message.Text;
        }

        public void Process(IEventHandler menuEventHandler) 
        {
            try
            {
                menuEventHandler.MessageProcess(Text?.Split("  ").LastOrDefault());
            }
            catch 
            {
                LoggingService.AddEventToLog("Cannot send message to user. Text is null");
            }
            
        }
    }
}
