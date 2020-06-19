using System.IO;
using System.Threading;
using Microsoft.Extensions.Configuration;
using StateMachineBot.States;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace StateMachineBot
{
    class Program
    {
        static IStateMachine _machine;
        private static TelegramBotClient _bot;

        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            //var socks = new HttpToSocks5Proxy("94.103.81.38", 1088);
            _machine = new StateMachine(CreateInitState);

            _bot = new TelegramBotClient("1212762438:AAFUFtjX85bpnIGY6Cvc1LzpuUp03gDLJ6Q");//, socks);
            StartBot();
            
            Thread.Sleep(Timeout.Infinite);
        }
        
        public static async void OnUpdate(object sender, UpdateEventArgs e)
        {
            var chat = e.Update.Message.Chat;
            var message = e.Update.Message;
            
            if(chat == null || message == null || string.IsNullOrEmpty(message.Text)) return;
            
            var data = new MessageEvent
            {
                Id = chat.Id.ToString(),
                Message = message.Text
            };
            var result = await _machine.FireEvent(data);
            await _bot.SendTextMessageAsync(chat.Id, result.AnswerMessage);
        }
        
        static void StartBot()
        {
            _bot.OnUpdate += OnUpdate;
            _bot.StartReceiving();
        }

        static IState CreateInitState()
        {
            return new InitState(_machine);
        }
    }
}