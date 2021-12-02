using Contexts.Solvers;
using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using EasyNetQ.Consumer;
using ProtoBuf;
using Solvers.Events;
using Solvers.Models;

namespace Solvers.Actions
{
    public class CreateSolver
    {
        private AppDbContext _context;

        private IPubSub _bus;

        public CreateSolver(AppDbContext context, IPubSub bus)
        {
            _context = context;
            _bus = bus;
        }

        public async Task FromController(CreateSolverModel model)
        {
            var solver = new Solver
            {
                Name = model.Name,
                Image = model.Image,
            };

            await _context.AddAsync(solver);

            await _context.SaveChangesAsync();

            await _bus.PublishAsync(new CreatedSolver
            {
                Id = solver.Id,
                UserToken = Guid.NewGuid(),
            });
        }
    }

    public class CreateSolverModel
    {
        public CreateSolverModel()
        {
            Name = "";
            Image = null;
        }

        public string Name { get; set; }
        public string? Image { get; set; }
    }

}
