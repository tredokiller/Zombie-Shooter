using Common.CommonScripts.States;

namespace Player.Scripts.States
{
    public class PlayerRunState: StateBase<PlayerController>, IState
    {
        public override void Enter()
        {
            Controller.ChangeSpeed(Controller.RunSpeed);
            Controller.playerData.state = State.Run;
        }

        public override void Update()
        {
         
        }

        public PlayerRunState(PlayerController controller) : base(controller) { }
    }
}