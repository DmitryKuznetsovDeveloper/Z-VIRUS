namespace FSM
{
    public interface IEnterable
    {
        void Enter();
    }

    public interface IExitable
    {
        void Exit();
    }

    public interface ITickableState
    {
        void Tick();
    }

    public interface ITransitionCondition
    {
        bool CanEnter();
    }
}