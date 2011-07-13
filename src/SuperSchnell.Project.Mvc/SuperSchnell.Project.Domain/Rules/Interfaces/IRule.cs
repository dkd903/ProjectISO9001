namespace SuperSchnell.Project.Domain.Rules
{
    public interface IRule<T>
    {
        string BrokenMessage { get; }
        bool IsBroken(T entity);
    }
}