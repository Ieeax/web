using System.Threading;
using System.Threading.Tasks;

namespace Leeax.Web.Components
{
    public interface IBatchProvider<TItem>
    {
        Task<Batch<TItem>> FetchAsync(int batchIndex, int batchSize, CancellationToken token);
    }
}