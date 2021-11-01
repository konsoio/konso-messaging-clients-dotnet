using Konso.Clients.Messagings.Model;
using Konso.Clients.Messagings.Model.Dtos;
using Konso.Clients.Messagings.Model.Requests;
using System.Threading.Tasks;

namespace Konso.Clients.Messagings.Interfaces
{
    public interface IMessagingService
    {
        Task<bool> SendAsync(CreateMessageRequest request);

        Task<PagedResponse<MessageHistoryDto>> GetAsync(MessageHistoryGetRequest messageHistoryGetRequest);
    }
}
