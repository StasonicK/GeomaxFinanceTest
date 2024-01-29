using UnityEngine;
using UnityEngine.InputSystem;

namespace Logic.Hero
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(HeroAnimator))]
    public class HeroMovement : MonoBehaviour
    {
        [SerializeField] private float _walkForwardSpeed = 3f;

        private HeroInput _heroInput;
        private CharacterController _characterController;

        private HeroAnimator _heroAnimator;

        private bool _isGrounded;
        private Vector3 _velocity;
        private Vector3 _direction;
        private bool _isAim;

        private void Awake()
        {
            _heroInput = new HeroInput();
            _characterController = GetComponent<CharacterController>();
            _heroAnimator = GetComponent<HeroAnimator>();
        }

        private void OnEnable()
        {
            _heroInput.Hero.Aim.performed += DoAim;
            _heroInput.Hero.Aim.canceled += StopAim;
            _heroInput.Enable();
        }

        private void OnDisable()
        {
            _heroInput.Disable();
            _heroInput.Hero.Aim.performed -= DoAim;
            _heroInput.Hero.Aim.canceled -= StopAim;
        }

        private void Update() =>
            Move();

        private void DoAim(InputAction.CallbackContext obj) =>
            _isAim = true;

        private void StopAim(InputAction.CallbackContext obj) =>
            _isAim = false;

        private void Move()
        {
            if (_isAim)
            {
                _heroAnimator.PlayAim();
                return;
            }

            Vector2 movementInput = _heroInput.Hero.Move.ReadValue<Vector2>();
            _direction = transform.forward * movementInput.y + transform.right * movementInput.x;

            if (movementInput.magnitude > Constants.MinimumMagnitude)
                SetWalk();
            else
                SetIdle();
        }

        private void SetWalk()
        {
            _characterController.Move((_direction.normalized * _walkForwardSpeed) * Time.deltaTime);
            _heroAnimator.PlayWalk();
        }

        private void SetIdle() =>
            _heroAnimator.PlayIdle();
    }
}