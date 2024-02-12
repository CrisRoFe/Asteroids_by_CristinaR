namespace Asteroids.GameStateMachine
{
    class Context
	{
        private State _state = null;

        public Context(State state)
        {
            TransitionTo(state);
        }

        public void ExecuteState()
        {
            _state.ExecuteState();
        }

        public void TransitionTo(State state)
        {
            _state = state;
            _state.SetContext(this);
        }
    }
}