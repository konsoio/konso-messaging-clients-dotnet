using Konso.Clients.Messagings.Model.Dtos;
using Konso.Clients.Messagings.Model;
using System.Threading.Tasks;
using Konso.Clients.Messaging.Model.Dtos;

namespace Konso.Clients.Messaging.Interfaces
{
    public interface ITemplateService
    {
        Task<PagedResponse<KonsoTemplateDto>> GetAsync(byte? targetId, int from = 0, int to = 10);
    }
}
