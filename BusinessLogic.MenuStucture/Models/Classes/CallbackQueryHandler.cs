﻿using BusinessLogic.MenuStucture.Models.Interfaces;
using BusinessLogic.MenuStucture.Services;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace BusinessLogic.MenuStucture.Models
{
    public class CallbackQueryHandler : IHandler
    {
        public Message Message { get; set; }
        public string Text { get; set; }

        public CallbackQueryHandler(CallbackQueryEventArgs arg)
        {
            Message = arg.CallbackQuery.Message;
            Text = arg.CallbackQuery.Data;
        }

        public void Process()
        {
            MenuEventHandler menuEventHandler = new MenuEventHandler();
            menuEventHandler.CallbackProcess(Text);
        }
    }
}
