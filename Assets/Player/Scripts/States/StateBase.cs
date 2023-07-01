using Common.CommonScripts.States;

namespace Player.Scripts.States
{
    public abstract class StateBase<T> : IState
    {
        protected readonly T Controller;
        protected bool IsCanChangeState = true;

        protected StateBase(T controller)
        {
            Controller = controller;
        }

        public abstract void Enter();

        public abstract void Update();

        public bool CanChangeState()
        {
            return IsCanChangeState;
        }
    }
}