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
        //private const string link = @"https://hooks.slack.com/services/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";
        private const string link = @"https://hooks.slack.com/services/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";



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
            //if (message.Length>300)
            //{
            //    message = message.Remove(300);
            //}

            this.Message = message;

            var slackMessage = new SlackMessage
            {
                Channel = this.Channel,
                Text = this.Message,
                IconEmoji = Emoji.Computer,
                Username = this.Username
            };

            var slackClient = new SlackClient(link);

            slackClient.Post(slackMessage);
        }

        //public void CreateAttachment(string message)
        //{
        //    this.Message = message;

        //    var slackMessage = new SlackMessage
        //    {
        //        Channel = this.Channel,
        //        Text = this.Message,
        //        IconEmoji = Emoji.Computer,
        //        Username = this.Username
        //    };

        //    var slackClient = new SlackClient(link);

        //    var slackAttachment = new SlackAttachment
        //    {
        //        Fallback = "New open task [Urgent]: <http://url_to_task|Test out Slack message attachments>",
        //        Text = "New open task *[Urgent]*: <http://url_to_task|Test out Slack message attachments>",
        //        Color = "#D00000",
        //        Fields =
        //    new List<SlackField>
        //        {
        //            new SlackField
        //                {
        //                    Title = "Notes",
        //                    Value = "This is much *easier* than I thought it would be."
        //                }
        //        }
        //    };

        //    slackMessage.Attachments = new List<SlackAttachment> { slackAttachment };

        //    slackClient.Post(slackMessage);
        //}

        public string Message { get; set; }

        public string Channel { get; set; }

        public string Username { get; set; }

    }
}
