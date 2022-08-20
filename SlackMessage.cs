using Slack.Webhooks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorKoorespondencii
{
    public class SlackMessageLog
    {        
        private const string hidenWebSlackHook = @"ht@@@tps://hooks@@@.sl@@@ack@@@.co@@@m/services@@@/T03TV@@@7SHBCY@@@/B03V2QS7XUG@@@/LyOGHw9U1@@@PR4duWvzGX@@@pR5Z5";



        public SlackMessageLog(string channel)
        {
            this.Channel = channel;
            this.Message = "";
            this.Username = Environment.MachineName + "-" + Environment.UserName;
        }
        public SlackMessageLog()
        {
            Channel = "#gstore";
            this.Message = "";
            this.Username = Environment.MachineName + "-" + Environment.UserName;

        }

        public void CreateMessage(string message)
        {
            if (message.Length>400)
            {
                message = message.Remove(400);
            }
            this.Message = message;

            var slackMessage = new SlackMessage
            {
                Channel = this.Channel,
                Text = this.Message,
                IconEmoji = Emoji.Computer,
                Username = this.Username
            };

            var slackClient = new SlackClient(UnhideSlackWebURL.Unhide(hidenWebSlackHook));

            slackClient.Post(slackMessage);
        }

        public string Message { get; set; }

        public string Channel { get; set; }

        public string Username { get; set; }

    }
}
