using UnityEngine;

namespace Logic.Hero
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(HeroAnimator))]
    public class HeroMovement : MonoBehaviour
    {
        [SerializeField] private float _walkForwardSpeed = 3f;
        [SerializeField] private float _walkBackSpeed = 2f;
        [SerializeField] private float _runForwardSpeed = 7f;
        [SerializeField] private float _runBackSpeed = 5f;

        private HeroInput _heroInput;
        private CharacterController _characterController;
        private HeroAnimator _heroAnimator;
        private bool _isGrounded;
        private Vector3 _velocity;
        private Vector3 _direction;

        private void Awake()
        {
            _heroInput = new HeroInput();
            _characterController = GetComponent<CharacterController>();
            _heroAnimator = GetComponent<HeroAnimator>();
        }

        private void OnEnable() =>
            _heroInput.Enable();

        private void OnDisable() =>
            _heroInput.Disable();

        private void Update() =>
            Move();

        private void Move()
        {
            Vector2 movementInput = _heroInput.Hero.Move.ReadValue<Vector2>();
            Vector3 airDirection = Vector3.zero;

            if (_characterController.isGrounded)
                airDirection = transform.forward * movementInput.y + transform.right * movementInput.x;
            else
                _direction = transform.forward * movementInput.y + transform.right * movementInput.x;

            if (movementInput.magnitude > Constants.MinimumMagnitude)
            {
                // if (_inputService.IsRunButtonUp())
                //     SetRun(airDirection, movementInput);
                // else
                SetWalk(airDirection, movementInput);
            }
            else
            {
                SetIdle(movementInput);
            }
        }

        private void SetRun(Vector3 airDirection, Vector2 movementInput)
        {
            _characterController.Move((_direction.normalized * GetMovementSpeed(_direction.normalized, true) +
                                       airDirection.normalized * _walkBackSpeed) * Time.deltaTime);
            _heroAnimator.SetHorizontalInput(movementInput.y);
            _heroAnimator.PlayRun();
        }

        private void SetWalk(Vector3 airDirection, Vector2 movementInput)
        {
            _characterController.Move((_direction.normalized * GetMovementSpeed(_direction.normalized, false) +
                                       airDirection.normalized * _walkBackSpeed) * Time.deltaTime);
            _heroAnimator.SetHorizontalInput(movementInput.y);
            _heroAnimator.PlayWalk();
        }

        private void SetIdle(Vector2 movementInput)
        {
            _heroAnimator.SetHorizontalInput(movementInput.y);
            _heroAnimator.PlayIdle();
        }

        private float GetMovementSpeed(Vector3 direction, bool isRun)
        {
            if (isRun)
            {
                if (direction.x > 0f)
                    return _runForwardSpeed;
                else
                    return _runBackSpeed;
            }
            else
            {
                if (direction.x > 0f)
                    return _walkForwardSpeed;
                else
                    return _walkBackSpeed;
            }
        }
    }
}