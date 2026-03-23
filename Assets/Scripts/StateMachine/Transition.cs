public class Transition : ITransition
{
    public IStates To { get; }

    public IPredicate Condition { get; }
    public Transition(IStates to, IPredicate condition)
    {
        To = to;
        Condition = condition;
    }
}
