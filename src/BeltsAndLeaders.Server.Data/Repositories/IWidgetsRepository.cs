using System.Threading.Tasks;
using BeltsAndLeaders.Server.Data.Models;

namespace BeltsAndLeaders.Server.Data.Repositories
{
    public interface IWidgetsRepository
    {
        Task<ulong> CreateAsync(WidgetRecord widget);

        Task<WidgetRecord> GetAsync(ulong id);

        Task UpdateAsync(WidgetRecord widget);

        Task DeleteAsync(ulong id);
    }
}
