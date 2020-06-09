using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MihaZupan;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace StateMachineBot
{
    class Program
    {
        private static TelegramBotClient _bot;
        private static OrderService _orderService = new OrderService();

        public Program(OrderService orderService)
        {
            _orderService = orderService;
        }

        static async Task Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            var socks = new HttpToSocks5Proxy("94.103.81.38", 1088);
            
            _bot = new TelegramBotClient("1212762438:AAFUFtjX85bpnIGY6Cvc1LzpuUp03gDLJ6Q", socks);
            StartBot();
            
            Thread.Sleep(Timeout.Infinite);
        }

        static void StartBot()
        {
            _bot.OnUpdate += OnUpdate;
            _bot.StartReceiving();
        }

        public static async void OnUpdate(object sender, UpdateEventArgs e)
        {
            var chat = e.Update.Message.Chat;
            var message = e.Update.Message;
            
            if(chat == null || message == null || string.IsNullOrEmpty(message.Text)) return;

            var state = (OrderStage)Int16.Parse(message.Text);
            var newMessage = "";
            
            switch (state)
            {
                case OrderStage.Processing:
                    _orderService.ProcessRequest(chat.Id);
                    newMessage = "Формирование заказа";
                    break;
                case OrderStage.Paying:
                    _orderService.PayRequest(chat.Id);
                    newMessage = "Заказ в оплате";
                    break;
                case OrderStage.Delivering:
                    _orderService.DeliveryRequest(chat.Id);
                    newMessage = "Заказа отправлен";
                    break;
                case OrderStage.Received:
                    _orderService.RecieveRequest(chat.Id);
                    newMessage = "Заказ получен";  
                    break;
                default:
                    newMessage = "Не удалось распознать статус";        
                    break;
            }

            await _bot.SendTextMessageAsync(chat.Id, newMessage);
        }
    }
}