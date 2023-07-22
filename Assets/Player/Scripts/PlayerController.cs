using System;
using Common.CommonScripts;
using Common.CommonScripts.States;
using Inputs;
using Managers;
using Player.Scripts.States;
using UnityEngine;
using Weapons.Scripts;
using Zenject;
using Random = UnityEngine.Random;

namespace Player.Scripts
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(WeaponSwitcher))]
    [RequireComponent(typeof(StateMachine))]
    public class PlayerController : MonoBehaviour , ITarget, IStateAble , IDamageable
    {
        [SerializeField] private Transform weaponPositions;
        [SerializeField] private Transform rifleWeaponPosition;
        [SerializeField] private Transform pistolWeaponPosition;

        [Header("Sounds")] 
        [SerializeField] private AudioClip[] damageAudios;
        
        
        [Header("Mesh")]
        [SerializeField] private Transform playerMesh;
        public float JogSpeed { private set; get; }
        public float RunSpeed { private set; get; }
        
        [Header("Rotation")] 
        [SerializeField, Range(0, 20)] private float smoothRotationTime;
        
        private float _currentSpeed;

        private IMouseInteraction _mouseInteraction;
        private CharacterController _characterController;
        private WeaponSwitcher _weaponSwitcher;
        private StateMachine _stateMachine;

        public PlayerData playerData;

        public float Health { private set; get; }
        public float MaxHealth { private set; get; }
        private bool _isDied;
        
        private InputManager _inputManager;
        private GameInput.PlayerActions _playerActions;
        private ISfxManager _sfxManager;

        private Vector3 _horizontalDirection;
        private Vector2 _horizontalVelocity;
        private float _verticalVelocity;

        public Vector3 RotationForAnimationHorizontal { private set; get; }
        
        private Vector2 _inputPlayer;
        
        public IWeapon CurrentWeapon { private set; get; }
        public WeaponType weaponType;

        [Header("States")] 
        private IState _runState;
        private IState _jogState;
        private IState _idleState;
        private IState _deathState;

        private Camera _camera;
        
        
        [Header("States")]
        public static Action OnDamaged;
        public static Action OnDied;

        [Inject]
        private void Constructor(InputManager inputManager ,IMouseInteraction mouseInteraction, ISfxManager sfxManager)
        {
            _inputManager = inputManager;
            _mouseInteraction = mouseInteraction;
            _sfxManager = sfxManager;
        }
        
        private void SetPlayerData()
        {
            JogSpeed = playerData.jogSpeed;
            RunSpeed = playerData.runSpeed;
            Health = playerData.health;
            MaxHealth = playerData.maxHealth;
        }
        
        private void Awake()
        {
            SetPlayerData();
            InstantiateStates();
            
            _currentSpeed = JogSpeed;

            _camera = Camera.main;
            _stateMachine = GetComponent<StateMachine>();
            _weaponSwitcher = GetComponent<WeaponSwitcher>();
            _characterController = GetComponent<CharacterController>();
            
            _playerActions = _inputManager.GetPlayerActions();
        }

        private void InstantiateStates()
        {
            _jogState = new PlayerJogState(this);
            _idleState = new IdleStateBase(this);
            _runState = new PlayerRunState(this);
            _deathState = new PlayerDeathState(this);
        }

        private void OnEnable()
        {
            _playerActions.Reload.started += context => TryToReload(); 
            WeaponSwitcher.OnWeaponSwitched += UpdateWeapon;
        }

        private void Start()
        {
            UpdateWeapon();
        }

        private void Update()
        {
            UpdateInputPlayer();
            UpdateState();
            ApplyGravity();

            if (_isDied) return;
            Rotate();
            Move();
            UpdateWeaponInteraction();
        }

        private void ApplyGravity()
        {
            if (!_characterController.isGrounded)
            {
                _verticalVelocity += Physics.gravity.y  * Time.deltaTime;
            }
            else
            {
                _verticalVelocity = 0;
            }
        }

        private void UpdateWeaponPosition()
        {
            if (CurrentWeapon.GetWeaponType() == WeaponType.Primary)
            {
                CurrentWeapon.SetPosition(rifleWeaponPosition.position);
                return;
            }
            CurrentWeapon.SetPosition(pistolWeaponPosition.position);
        }

        private void Rotate()
        {
            var targetRotation = _mouseInteraction.GetRotationToMousePosition(out _horizontalDirection , transform.position);
            weaponPositions.rotation = Quaternion.Lerp(weaponPositions.rotation, targetRotation, smoothRotationTime * Time.deltaTime);
            
            if (_horizontalVelocity == Vector2.zero)
            {
                if (Mathf.Abs(weaponPositions.localRotation.eulerAngles.y) > 75f)
                {
                    playerMesh.rotation = Quaternion.Lerp(playerMesh.rotation , targetRotation, smoothRotationTime * Time.deltaTime); 
                }
            }
            else
            {
                playerMesh.rotation = Quaternion.Lerp(playerMesh.rotation , targetRotation, smoothRotationTime * Time.deltaTime); 
            }
            
            CurrentWeapon.SetRotation(targetRotation);
        }

        private void UpdateInputPlayer()
        {
            _inputPlayer = _playerActions.Move.ReadValue<Vector2>();
        }
        
        private void Move()
        {
            _horizontalVelocity = _inputPlayer * _currentSpeed;
            RotationForAnimationHorizontal =
                playerMesh.transform.InverseTransformDirection(new Vector3(_horizontalVelocity.x + _camera.transform.up.x , 0f , _horizontalVelocity.y));

            _characterController.Move(new Vector3(_horizontalVelocity.x , _verticalVelocity, _horizontalVelocity.y) * Time.deltaTime);
        }

        private void UpdateState()
        {
            if (_isDied)
            {
                _stateMachine.SetState(_deathState);
                return;
            }
            if (_horizontalVelocity.magnitude <= 0.1f)
            {
                _stateMachine.SetState(_idleState);
                return;
            }

            if (_currentSpeed == playerData.jogSpeed)
            {
                _stateMachine.SetState(_jogState);
            }
            else
            {
                _stateMachine.SetState(_runState);
            }
            
        }

        public void ChangeSpeed(float speed)
        {
            _currentSpeed = speed;
        }
        
        private void UpdateWeapon()
        {
            CurrentWeapon = _weaponSwitcher.GetCurrentWeapon();
            weaponType = CurrentWeapon.GetWeaponType();
            
            CurrentWeapon.SetPositionImmediately(rifleWeaponPosition.position);
        }

        private void TryToShoot()
        {
            if (CurrentWeapon != null)
            {
                CurrentWeapon.Shoot();
            }
        }

        private void TryToReload()
        {
            if (CurrentWeapon != null)
            {
                CurrentWeapon.Reload();
            }
        }


        private void UpdateWeaponInteraction()
        {
            UpdateWeaponPosition();
            if (_playerActions.Fire.IsPressed())
            {
                TryToShoot();
            }
        }
        
        private void OnDisable()
        {
            _playerActions.Reload.started -= context => TryToReload(); 
            WeaponSwitcher.OnWeaponSwitched -= UpdateWeapon;
        }

        public Transform GetTargetTransform()
        {
            return transform;
        }

        public State GetState()
        {
            return playerData.state;
        }

        public void SetState(State newState)
        {
            playerData.state = newState;
        }

        public void TookDamage(float damage)
        {
            Health -= damage;
            OnDamaged?.Invoke();
            if (Health <= 0 && !_isDied)
            {
                _isDied = true;
                return;
            }
            _sfxManager.MakeSound(damageAudios[Random.Range(0 , damageAudios.Length)]);
        }
    }
}
