using System.Threading.Tasks;

namespace BeltsAndLeaders.Server.Business.Queries
{
    public interface IQueryResponseOnly<TQueryResponse>
    {
        Task<TQueryResponse> ExecuteAsync();
    }
}
