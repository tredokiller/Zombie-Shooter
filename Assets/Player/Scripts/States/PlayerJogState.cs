using Common.CommonScripts.States;

namespace Player.Scripts.States
{
    public class PlayerJogState: StateBase<PlayerController>
    {
        public override void Enter()
        {
            Controller.ChangeSpeed(Controller.JogSpeed);
            Controller.playerData.state = State.Jog;
        }

        public override void Update()
        {
        }

        public PlayerJogState(PlayerController controller) : base(controller) { }
    }
}