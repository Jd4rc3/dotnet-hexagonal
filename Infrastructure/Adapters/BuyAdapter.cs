using Domain.Models;
using Domain.UseCases;
using Microsoft.EntityFrameworkCore;

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

    public async Task<List<Buy>> HistoryAsync(int clientId)
    {
        return await _context.Buy.Include(b => b.Buys).Select(b => b.IdNumber == clientId ? b : null).ToListAsync();
    }
}