namespace Konso.Clients.Messaging.Model.Dtos
{
    public class KonsoTemplateDto
    {
        public string? Body { get; set; }

        public string? Subject { get; set; }
        public string Parameters { get; set; }

        public string TemplateType { get; set; }

        public byte Target { get; set; }

        public string? RecipientGroupId { get; set; }

        public string? Name { get; set; }

        public byte Format { get; set; }

        public bool IsPriority { get; set; }

    }
}
