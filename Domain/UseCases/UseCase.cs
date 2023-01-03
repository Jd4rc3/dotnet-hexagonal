namespace Domain.UseCases;

public abstract class UseCase<T, P>
{
    public abstract T Apply(P type);
}