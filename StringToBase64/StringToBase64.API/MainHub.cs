using Microsoft.AspNetCore.SignalR;

namespace StringToBase64.API
{
    public class MainHub : Hub
    {
        //public async Task SendMessage(string user, string message)
        //{
        //    await Clients.All.SendAsync("ReceiveMessage", user, message);
        //}

        public async Task SendMessage(string message)
        {

            Random random = new Random();
            int randomInterval = random.Next(1, 6); //Get random number between 1 to 5

            await Task.Delay(randomInterval * 1000);

            await Clients.All.SendAsync("ReceiveMessage", message);

            //await Clients.All.SendAsync("ReceiveMessage", message, cancellationToken);
        }
    }
}
