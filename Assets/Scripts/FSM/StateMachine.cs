namespace FSM
{
    public class StateMachine
    {
        private object _current;

        public object CurrentState => _current;

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

        public void TrySetIfNotCurrent(object newState)
        {
            if (_current == newState)
                return;

            SetState(newState);
        }
        
        public void TrySetMany(params object[] states)
        {
            foreach (var state in states)
            {
                if (state is ITransitionCondition condition && condition.CanEnter())
                {
                    TrySetIfNotCurrent(state);
                    return;
                }
            }
        }
        
        public void TrySet(IState state)
        {
            if (state is ITransitionCondition condition && !condition.CanEnter())
                return;

            if (_current == state)
                return;

            if (_current is IExitable exitable)
                exitable.Exit();

            _current = state;

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