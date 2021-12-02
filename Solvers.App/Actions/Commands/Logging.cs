using Contexts.Solvers;
using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using EasyNetQ.Consumer;
using Solvers.App.Events;
using Solvers.App.Models;

namespace Solvers.App.Actions
{
    public class Logging : IConsumeAsync<CreatedSolver>
    {
        public Task ConsumeAsync(CreatedSolver message, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}
