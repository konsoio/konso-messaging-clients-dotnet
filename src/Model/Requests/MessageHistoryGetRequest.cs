using Konso.Clients.Messagings.Model.Requests.Enums;
using System.Collections.Generic;

namespace Konso.Clients.Messagings.Model.Requests
{
    public class MessageHistoryGetRequest
    {

        public long? DateFrom { get; set; }
        public long? DateTo { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public int From { get; set; } = 0;
        public int To { get; set; } = 10;

        public SortingTypes Sort { get; set; } = SortingTypes.CreationDateDesc;

        public List<string> Tags { get; set; }
    }
}
