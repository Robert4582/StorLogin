using System;
using Common;
using Common.Extensions;
using RabbitMQ.Client.Events;

namespace Login
{
    public class Program
    {
        static MessageQueue queue = new MessageQueue();
        static void Main(string[] args)
        {
            queue.CreateExchange(RabbitMQExchangeTypes.Direct, "stor");
            queue.BindServices("stor", Services.Login, Services.Create);
            queue.AssignOnRecieve(OnReceive);

            Console.Read();
        }
        static void OnReceive(BasicDeliverEventArgs args)
        {
            var file = Json.DeserializeFromMemory<NetworkFile<string[]>>(args.Body);
            Console.WriteLine($"Request recieved for service {file.Service}.");
            switch (file.Service)
            {
                case Services.Login:
                    handleLoginUser(file);
                    break;
                case Services.Create:
                    handleCreateUser(file);
                    break;

                default:
                    throw new Exception("Unrecognized Service!");
            }
        }
        public static void handleLoginUser(NetworkFile<string[]> file)
        {
            if (UserDatabase.TryGetUser(file.Info[0], file.Info[1], out User user))
            {
                foreach (var item in user.GetAsStrings())
                {
                    Console.WriteLine(item);
                }
                queue.RespondToRpc(file, new NetworkFile<string[]>(user.GetAsStrings()) { Service = Services.Response}, true);
            }
        }
        public static void handleCreateUser(NetworkFile<string[]> file)
        {
            if (UserDatabase.TryCreateUser(file.Info[0], file.Info[1], file.Info[2], out User user))
            {
                queue.RespondToRpc(file, new NetworkFile<string[]>(user.GetAsStrings()) { Service = Services.Response }, true);
            }
        }

    }
}
