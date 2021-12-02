using Contexts.Solvers;
using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using EasyNetQ.Consumer;
using Solvers.Events;
using Solvers.Models;

namespace Solvers.Actions
{
    public class Logging : IConsumeAsync<CreatedSolver>
    {
        public Task ConsumeAsync(CreatedSolver message, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}
