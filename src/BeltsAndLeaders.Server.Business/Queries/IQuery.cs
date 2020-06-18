using System.Threading.Tasks;

namespace BeltsAndLeaders.Server.Business.Queries
{
    public interface IQuery<TQueryRequest, TQueryResponse>
    {
        Task<TQueryResponse> ExecuteAsync(TQueryRequest queryRequest);
    }
}
