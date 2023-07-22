using Common.CommonScripts.States;
using Player.Scripts.States;

namespace Enemies.States
{
    public class EnemyChaseState : StateBase<EnemyBase.Scripts.EnemyBase>
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
        
        public EnemyChaseState(EnemyBase.Scripts.EnemyBase controller) : base(controller) { }
    }
}