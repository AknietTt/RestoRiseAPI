using PRTelegramBot.Attributes;
using PRTelegramBot.InlineButtons;
using PRTelegramBot.Interface;
using PRTelegramBot.Models;
using PRTelegramBot.Models.CallbackCommands;
using PRTelegramBot.Models.InlineButtons;
using PRTelegramBot.Utils;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace RestoRise.TelegramBot.TelegramCommands;

public class Commands
{
    
    [ReplyMenuHandler("/start" )]
    public static async Task Example(ITelegramBotClient botClient, Update update)
    {
        string msg = "Добро пожаловать на FeedMe!\nЧтобы заказать еду, нажмите на кнопку «Сайт» слева.";
        var option = new OptionMessage();
      //  botClient.AnswerWebAppQueryAsync();
        await PRTelegramBot.Helpers.Message.Send(botClient, update, msg, option);
    }
    
}