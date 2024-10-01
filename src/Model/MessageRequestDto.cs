using Konso.Clients.Messagings.Model.Enums;
using System.Collections.Generic;

namespace Konso.Clients.Messagings.Model
{
    public class MessageRequestDto 
    {
        public string BucketId { get; set; }

        public string CorrelationId { get; set; }

        public string PlainBase64Body { get; set; }

        public string HtmlBase64Body { get; set; }

        public string Env { get; set; }

        public List<string> Recipients { get; set; }

        public string Subject { get; set; }

        public int Delay { get; set; }

        public byte MessageType { get; set; }

        public List<string> Tags { get; set; }
    }
}
