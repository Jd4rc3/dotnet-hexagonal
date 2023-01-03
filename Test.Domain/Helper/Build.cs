using Domain.Models;

namespace Test.Domain.Helper;

public static class Build
{
    public static Product Product()
    {
        return new Product()
        {
            Id = 1,
            Enabled = true,
            Max = 200,
            Min = 8,
            Name = "Apple",
            InInventory = 800
        };
    }
}