using Common.CommonScripts.States;

namespace Player.Scripts.States
{
    public class IdleStateBase: StateBase<IStateAble>
    {
        public override void Enter()
        {
            Controller.SetState(State.Idle); 
        }

        public override void Update()
        {
            
        }

        public IdleStateBase(IStateAble stateAble) : base(stateAble) { }
    }
}