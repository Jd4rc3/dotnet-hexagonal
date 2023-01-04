using Domain.Models;

namespace Domain.UseCases;

public interface IBuyRepository
{
    Task<Buy> PurchaseAsync(Buy buy);

    Task<List<Buy>> HistoryAsync(int clientId);
}