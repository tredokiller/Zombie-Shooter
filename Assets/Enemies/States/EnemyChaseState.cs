using Common.CommonScripts.States;
using Player.Scripts.States;

namespace Enemies.States
{
    public class EnemyChaseState : StateBase<EnemyBase>
    {
        public override void Enter()
        {
            Controller.SetState(State.Chase);
            Controller.NavAgent.speed = Controller.ChasingSpeed;
            MoveToTarget();
        }
        
        public override void Update()
        {
            MoveToTarget();
        }

        private void MoveToTarget()
        {
            Controller.NavAgent.destination = Controller.TargetTransform.position;
        }
        
        public EnemyChaseState(EnemyBase controller) : base(controller) { }
    }
}