using Api.Dtos;
using AutoMapper;
using Domain.Models;
using Domain.UseCases;
using Domain.UseCases.CreateProductUseCase;
using Domain.UseCases.DeleteProductUseCase;
using Domain.UseCases.GetProductUseCase;
using Domain.UseCases.UpdateProductUseCase;
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
        [FromServices] IEnumerable<UseCase<Task<Product>, Product>> createProductUseCase)
    {
        var useCase = createProductUseCase.Single(_ => _.GetType() == typeof(CreateProductUseCase));
        var product = _mapper.Map<Product>(createProductDto);

        await useCase.Apply(product);
        return CreatedAtAction(nameof(GetProductAsync), new { id = product.Id }, product);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetProductAsync([FromRoute] int id,
        [FromServices] IEnumerable<UseCase<Task<Product>, Product>> getProductUseCase)
    {
        var useCase = getProductUseCase.Single(_ => _.GetType() == typeof(GetProductUseCase));
        var result = await useCase.Apply(new Product { Id = id });

        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult> GetAllProductsAsync(
        [FromServices] UseCase<Task<List<Product>>, Product> useCase)
    {
        var result = await useCase.Apply(new Product());

        return Ok(result.Select(product => _mapper.Map<ProductDto>(product)));
    }

    //TODO: Update
    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateProductAsync([FromRoute] int id, [FromBody] CreateProductDto createProductDto,
        [FromServices] IEnumerable<UseCase<Task<Product>, Product>> updateProductUseCase)
    {
        var useCase = updateProductUseCase.Single(_ => _.GetType() == typeof(UpdateProductUseCase));
        var product = _mapper.Map<Product>(createProductDto);
        var result = await useCase.Apply(product with { Id = id });

        return Ok(_mapper.Map<ProductDto>(result));
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteProduct(int id,
        [FromServices] IEnumerable<UseCase<Task<Product>, Product>> deleteProductUseCase)
    {
        var useCase = deleteProductUseCase.Single(_ => _.GetType() == typeof(DeleteProductUseCase));

        var deletedProduct = await useCase.Apply(new Product { Id = id });
        return Ok(deletedProduct);
    }
}