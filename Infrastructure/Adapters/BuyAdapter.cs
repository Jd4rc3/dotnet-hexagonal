using Domain.Models;
using Domain.UseCases;

namespace Infrastructure.Adapters;

public class BuyAdapter : IBuyRepository
{
    private readonly Context _context;

    public BuyAdapter(Context context)
    {
        _context = context;
    }

    public async Task<Buy> PurchaseAsync(Buy buy)
    {
        var newBuy = await _context.AddAsync(buy);

        return newBuy.Entity;
    }
}