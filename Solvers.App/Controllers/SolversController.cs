using Contexts.Solvers;
using EasyNetQ;
using Microsoft.AspNetCore.Mvc;
using Solvers.App.Actions;
using Solvers.App.Models;

namespace Solvers.App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SolversController : Controller
    {
        private readonly ILogger<SolversController> _logger;
        private readonly IPubSub _bus;
        private readonly AppDbContext _context;

        public SolversController(ILogger<SolversController> logger, AppDbContext context, IPubSub bus)
        {
            _logger = logger;
            _context = context;
            _bus = bus;
        }
        
        [HttpGet]
        public IList<Solver> Index()
        {
            return _context.Solvers.ToList();
        }

        [HttpPost]
        public async Task Create([FromServices] CreateSolver action, CreateSolverModel model)
        {
            await action.FromController(model);
        }

        [HttpDelete]
        public async Task Delete()
        {
            _context.Solvers.RemoveRange(_context.Solvers.ToList());
            await _context.SaveChangesAsync();
        }

    }
}