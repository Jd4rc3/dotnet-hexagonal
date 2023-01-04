using Api.Dtos;
using AutoMapper;
using Domain.Models;
using Domain.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class BuyController : ControllerBase
{
    private readonly IMapper _mapper;

    public BuyController(IMapper mapper)
    {
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult> PurchaseAsync([FromBody] PurchaseDto purchaseDto,
        [FromServices] UseCase<Task<Buy>, Buy> useCase)
    {
        var purchase = _mapper.Map<Buy>(purchaseDto);
        var result = await useCase.Apply(purchase);

        return Ok(_mapper.Map<PurchaseDto>(result));
    }
}