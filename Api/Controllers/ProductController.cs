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
    private readonly UseCase<Task<Product>, Product> _useCase;

    public ProductController(UseCase<Task<Product>, Product> useCase, IMapper mapper)
    {
        _useCase = useCase;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult> CreateProductAsync(CreateProductDto createProductDto)
    {
        var product = _mapper.Map<Product>(createProductDto);

        await _useCase.Apply(product);
        // return CreatedAtAction()
        return Ok();
    }
}