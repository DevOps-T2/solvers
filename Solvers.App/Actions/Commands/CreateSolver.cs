using Contexts.Solvers;
using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using EasyNetQ.Consumer;
using ProtoBuf;
using Solvers.App.Contracts;
using Solvers.App.Events;
using Solvers.App.Models;

namespace Solvers.App.Actions
{
    public class CreateSolver : IAction
    {
        private WriteDbContext _context;

        public CreateSolver(WriteDbContext context)
        {
            _context = context;
        }

        public async Task<Solver> FromController(CreateSolverModel model)
        {
            var solver = new Solver
            {
                Name = model.Name,
                Image = model.Image,
            };

            await _context.AddAsync(solver);

            await _context.SaveChangesAsync();

            return solver;
        }
    }

    public class CreateSolverModel
    {
        public CreateSolverModel()
        {
            Name = "";
            Image = "";
        }

        public string Name { get; set; }
        public string Image { get; set; }
    }

}
