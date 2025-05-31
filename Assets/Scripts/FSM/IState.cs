namespace FSM
{
    public interface IEnterable: IState
    {
        void Enter();
    }

    public interface IExitable: IState
    {
        void Exit();
    }

    public interface ITickableState: IState
    {
        void Tick();
    }

    public interface ITransitionCondition : IState
    {
        bool CanEnter();
    }
    
    public interface IState {}
}