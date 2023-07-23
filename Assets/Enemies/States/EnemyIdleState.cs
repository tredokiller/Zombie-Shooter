using Common.CommonScripts.States;
using Player.Scripts.States;
using UnityEngine;

namespace Enemies.States
{
    public class EnemyIdleState : StateBase<EnemyBase.Scripts.EnemyBase>
    {
        public EnemyIdleState(EnemyBase.Scripts.EnemyBase controller) : base(controller) { }

        public override void Enter()
        {
            UpdateStateByEnemyPathFinished();
        }

        public override void Update()
        {
            UpdateStateByEnemyPathFinished();
        }

        private void UpdateStateByEnemyPathFinished()
        {
            Controller.SetState(Controller.NavAgent.velocity != Vector3.zero ? State.Chase : State.Idle);
        }
    }
}