using Api.Dtos;
using AutoMapper;
using Domain.Models;
using Domain.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IMapper _mapper;

    public ProductController(IMapper mapper)
    {
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult> CreateProductAsync([FromBody] CreateProductDto createProductDto,
        [FromServices] UseCase<Task<Product>, Product> useCase)
    {
        var product = _mapper.Map<Product>(createProductDto);

        await useCase.Apply(product);
        // return CreatedAtAction()
        return Ok();
    }
}