using System.Threading.Tasks;

namespace BeltsAndLeaders.Server.Business.Commands
{
    public interface ICommand<TCommandRequest, TCommandResponse>
    {
        Task<TCommandResponse> ExecuteAsync(TCommandRequest commandRequest);
    }
}
