using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.API.Controllers.Base;

public class BaseCrudController<Response, Request, UpdateRequest,Entity> : BaseController
{
    private readonly IService<Response, Request, UpdateRequest, Entity> _service;
    public BaseCrudController(IService<Response, Request, UpdateRequest,Entity> service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Response>>> GetAll(CancellationToken cancellationToken) 
        => HandleListedResult(await _service.GetAllAsync(cancellationToken));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Response>> GetById(Guid id, CancellationToken cancellationToken) 
        => HandleResult(await _service.GetByIdAsync(id, cancellationToken));

    [HttpPost]
    public async Task<ActionResult<Response>> Create([FromBody ]Request entity, CancellationToken cancellationToken) 
        => HandleResult(await _service.CreateAsync(entity,cancellationToken));

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<Response>> Update(Guid id, UpdateRequest entity,CancellationToken cancellationToken)
        => HandleResult(await _service.UpdateAsync(id, entity,cancellationToken));

}
