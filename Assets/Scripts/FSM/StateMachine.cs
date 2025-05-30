namespace FSM
{
    public class StateMachine
    {
        private object _current;

        public void SetState(object newState)
        {
            if (_current == newState)
                return;

            if (newState is not ITransitionCondition condition || !condition.CanEnter())
                return;

            if (_current is IExitable exitable)
                exitable.Exit();

            _current = newState;

            if (_current is IEnterable enterable)
                enterable.Enter();
        }

        public void Tick()
        {
            if (_current is ITickableState tickable)
                tickable.Tick();
        }
    }
}