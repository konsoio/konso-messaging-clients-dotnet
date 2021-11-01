using System;

namespace Konso.Clients.Messagings.Model
{
    public class ValidationErrorItem
    {
        public ValidationErrorItem() : this(string.Empty, null, string.Empty)
        {
        }

        public ValidationErrorItem(string name, object attemptedValue, string message)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");
            if (string.IsNullOrEmpty(message)) throw new ArgumentNullException("message");

            Name = name;
            AttemptedValue = attemptedValue;
            Message = message;
        }

        public ValidationErrorItem(string name, object attemptedValue)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");

            Name = name;
            AttemptedValue = attemptedValue;
        }

        public string Name { get; private set; }
        public object AttemptedValue { get; private set; }
        public string Message { get; private set; }
    }
}
