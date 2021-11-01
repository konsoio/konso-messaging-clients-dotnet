namespace Konso.Clients.Messagings.Model
{
    public class ErrorItem
    {
        public ErrorItem() : this(string.Empty, string.Empty)
        {

        }
        public ErrorItem(string message) : this(message, string.Empty)
        {

        }

        public ErrorItem(string message, string stack)
        {
            Message = message;
            Stack = stack;
        }

        public string Message { get; set; }

        public string Stack { get; set; }
    }
}
