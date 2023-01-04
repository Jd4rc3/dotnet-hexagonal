namespace Domain.UseCases;

public abstract class UseCase<T, TP>
{
    public abstract T Apply(TP type);
}