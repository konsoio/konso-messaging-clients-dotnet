using Konso.Clients.Messagings.Model.Enums;
using System.Collections.Generic;

namespace Konso.Clients.Messagings.Model
{
    public class CreateMessageRequest
    {
        public string CorrelationId { get; set; }

        public string Env { get; set; }

        public List<string> Recipients { get; set; }

        public string Subject { get; set; }

        public int Delay { get; set; }

        public byte MessageType { get; set; }

        public List<string> Tags { get; set; }


        public string PlainText { get; set; }

        public string Html { get; set; }
    }
}
