using Common.CommonScripts.States;

namespace Player.Scripts.States
{
    public class PlayerDeathState: StateBase<PlayerController>, IState
    {
        public override void Enter()
        {
            Controller.playerData.state = State.Die;
            Controller.CurrentWeapon.SetActive(false);
            PlayerController.OnDied?.Invoke();
        }

        public override void Update()
        {
        }

        public PlayerDeathState(PlayerController controller) : base(controller) { }
    }
}