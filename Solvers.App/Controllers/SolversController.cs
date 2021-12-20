using Contexts.Solvers;
using EasyNetQ;
using Microsoft.AspNetCore.Mvc;
using Solvers.App.Actions;
using Solvers.App.Actions.Queries;
using Solvers.App.Models;

namespace Solvers.App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SolversController : Controller
    {
        [HttpGet]
        public IList<Solver> Index([FromServices] ListSolvers action)
        {
            return action.FromController();
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<Solver> Get([FromServices] GetSolver action, long id)
        {
            return action.FromController(id);
        }

        [HttpPost]
        public async Task<ActionResult<Solver>> Create([FromServices] CreateSolver action, CreateSolverModel model)
        {
            return await action.FromController(Request, model);
        }

        [HttpPut]
        public async Task<ActionResult<Solver>> Update([FromServices] UpdateSolver action, UpdateSolverModel model)
        {
            return await action.FromController(Request, model);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete([FromServices] DeleteSolver action, long id)
        {
            return await action.FromController(Request, id);
        }
    }
}