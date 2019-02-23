﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegrammAppMvcDotNetCore___Buisness_Logic;

namespace TelegrammAspMvcDotNetCoreBot.Models.Commands
{
    public class StartCommand : Command
    {
        private readonly UserOperation _user = new UserOperation();

        public override string Name => @"/start";

        public override bool Contains(Message message)
        {
            if (message.Type != Telegram.Bot.Types.Enums.MessageType.TextMessage)
                return false;

            return message.Text.Contains(Name);
        }

        public override async Task Execute(Message message, TelegramBotClient botClient)
		{
			Schedule.Unit();
			List<string> un = Schedule.GetUniversities();
			
			string[][] unn = new string[un.ToList().Count][];
			
			int count = 0;
			foreach (string item in un)
			{
				unn[count] = new[] { item };
				count++;
			}

			var chatId = message.Chat.Id;

			if (!_user.CheckUser(chatId))
				_user.CreateUser(chatId);
			else
				_user.RecreateUser(chatId);
			//await botClient.SendTextMessageAsync(chatId, "Hallo I'm ASP.NET Core Bot and I made by Mr.Robot", parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
			await botClient.SendTextMessageAsync(chatId, "Привет, выбери свой университет", parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown, replyMarkup: Keybord.GetKeyboard(unn, count));
		}
	}
}
