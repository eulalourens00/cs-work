using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace company_messenger
{
    public class MessageCore
    {
        public DateTime Time {  get; set; }
        public string Sender { get; set; }
        public string Text { get; set; }

        public MessageCore(string sender, string text)
        {
            Time = DateTime.Now;
            Sender = sender;
            Text = text;
        }
    }
}
