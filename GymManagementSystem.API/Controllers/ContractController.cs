using GymManagementSystem.API.Controllers.Base;
using GymManagementSystem.Core.DTO.Contract;
using GymManagementSystem.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContractController : BaseController
{
    private readonly IContractService _contractService;
    public ContractController(IContractService contractService) 
    {
        _contractService = contractService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ContractResponse>>> GetAll(CancellationToken cancellationToken)
        => HandleListedResult(await _contractService.GetAllAsync(cancellationToken));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ContractDetailsResponse>> GetById(Guid id, CancellationToken cancellationToken)
        => HandleResult(await _contractService.GetByIdAsync(id, cancellationToken));

    [HttpPost]
    public async Task<ActionResult<ContractResponse>> Create([FromBody] ContractAddRequest entity, CancellationToken cancellationToken)
        => HandleResult(await _contractService.CreateAsync(entity));

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ContractResponse>> Update(Guid id, ContractUpdateRequest entity)
        => HandleResult(await _contractService.UpdateAsync(id, entity));

}
