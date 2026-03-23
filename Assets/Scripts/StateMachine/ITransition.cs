public interface ITransition
{
    IStates To {  get; }
    IPredicate Condition { get; }
}
