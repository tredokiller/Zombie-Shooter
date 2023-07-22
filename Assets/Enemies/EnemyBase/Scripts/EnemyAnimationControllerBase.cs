using System;
using UnityEngine;

namespace Enemies.DefaultZombie.Scripts
{
    public class EnemyAnimationControllerBase : MonoBehaviour
    {
        private Animator _animator;
        private EnemyBase.Scripts.EnemyBase _enemyBase;
        private static readonly int State = Animator.StringToHash("State");
        private static readonly int IsDamaged = Animator.StringToHash("IsDamaged");

        [SerializeField, Range(1, 10)] private int dieAnimationsCount;
        private static readonly int IsDied = Animator.StringToHash("IsDied");

        private void Awake()
        {
            _enemyBase = GetComponent<EnemyBase.Scripts.EnemyBase>();
            _animator = _enemyBase.Mesh.GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _enemyBase.OnDamaged += ActiveDamagedTrigger;
            _enemyBase.OnDied += ActivateDiedTrigger;
        }

        private void OnDisable()
        {
            _enemyBase.OnDamaged -= ActiveDamagedTrigger;
            _enemyBase.OnDied -= ActivateDiedTrigger;
        }

        private void ActiveDamagedTrigger()
        {
           _animator.SetTrigger(IsDamaged); 
        }

        private void ActivateDiedTrigger()
        {
            _animator.SetTrigger(IsDied);
        }

        private void Update()
        {
            UpdateState();
        }

        private void UpdateState()
        {
            _animator.SetInteger(State , (int)_enemyBase.GetState());
        }
    }
}