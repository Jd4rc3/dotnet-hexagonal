using Api.Dtos;
using Bogus;
using Domain.Models;

namespace Test.Common;

public static class Builder
{
    public static List<Product> GenerateProductsData(int count)
    {
        var faker = new Faker<Product>()
            .RuleFor(c => c.Name, f => f.Music.Random.Word())
            .RuleFor(c => c.InInventory, f => f.Random.Number())
            .RuleFor(c => c.Max, f => f.Random.Number())
            .RuleFor(c => c.Min, f => f.Random.Number())
            .RuleFor(c => c.Enabled, f => f.Random.Bool())
            .RuleFor(c => c.Id, f => f.Random.Number());

        return faker.Generate(count);
    }

    public static List<ProductDto> GenerateProductsDtos(int count)
    {
        var faker = new Faker<ProductDto>()
            .RuleFor(c => c.Name, f => f.Music.Random.Word())
            .RuleFor(c => c.InInventory, f => f.Random.Number())
            .RuleFor(c => c.Max, f => f.Random.Number())
            .RuleFor(c => c.Min, f => f.Random.Number())
            .RuleFor(c => c.Enabled, f => f.Random.Bool())
            .RuleFor(c => c.Id, f => f.Random.Number());

        return faker.Generate(count);
    }
}