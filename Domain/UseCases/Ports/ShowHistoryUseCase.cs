using Domain.Models;

namespace Domain.UseCases;

public class ShowHistoryUseCase : UseCase<Task<List<Buy>>, int>
{
    private readonly IBuyRepository _repository;

    public ShowHistoryUseCase(IBuyRepository repository)
    {
        _repository = repository;
    }

    public override async Task<List<Buy>> Apply(int clientId)
    {
        return await _repository.HistoryAsync(clientId);
    }
}