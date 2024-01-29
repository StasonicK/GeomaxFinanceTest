using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Logic.Hero
{
    public class HeroRotation : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed = 0.5f;
        [SerializeField] private float _thresholdMagnitude = 100f;
        [SerializeField] private LayerMask groundMask;

        private Camera _camera;
        private HeroInput _heroInput;
        private Vector2 _mousePosition;
        private Vector3 _mousePositionToLookAt;
        private Vector3 _mouseViewportPosition;
        private Quaternion _currentRotation;
        private Quaternion _targetRotation;
        private Vector3 _mouseWorldPosition;
        private Vector3 _targetDirection;
        private float _angle;
        private Vector2 _currentMousePosition;
        private Vector2 _mouseLook;
        private Vector3 _rotationTarget;
        private Vector2 _tempMouseLook;
        private Quaternion _rotation;
        private Vector3 _aimDirection;
        private RaycastHit _hit;
        private Ray _ray;
        private Vector3 _lookPosition;
        private Coroutine _rotateCoroutine;

        private void Awake()
        {
            _camera = Camera.main;
            _heroInput = new HeroInput();
            _currentRotation = transform.rotation;
        }

        private void Start()
        {
            Cursor.visible = false;
        }

        private void OnEnable()
        {
            // _heroInput.Hero.MouseLook.performed += OnMouseLook;
            // _heroInput.Hero.MouseLook.canceled += OnMouseLookStop;
            _heroInput.Enable();
        }

        private void OnDisable()
        {
            _heroInput.Disable();
            // _heroInput.Hero.MouseLook.performed -= OnMouseLook;
            // _heroInput.Hero.MouseLook.canceled -= OnMouseLookStop;
        }

        private void Update() =>
            Rotate();

        private void Rotate()
        {
            // _mousePosition = _heroInput.Hero.MousePositon.ReadValue<Vector2>();
            // Debug.Log($"mousePosition {_mousePosition}");
            // _mouseWorldPosition = _camera.ScreenToWorldPoint(_mousePosition);
            // // _mouseWorldPosition = _camera.ScreenToWorldPoint(new Vector3(_mousePosition.x, 0f, _mousePosition.y));
            // _targetDirection = _mouseWorldPosition - transform.position;
            // Debug.Log($"targetDirection {_targetDirection}");
            // _angle = Mathf.Atan2(_targetDirection.y, _targetDirection.x) * Mathf.Rad2Deg;
            // Debug.Log($"angle {_angle}");
            // transform.rotation = Quaternion.Euler(new Vector3(0f, _angle, 0f));

            // _currentMousePosition = Mouse.current.position.ReadValue();
            // var worldPos = _camera.ScreenToWorldPoint(_currentMousePosition);
            // var newAim = (worldPos - transform.position).normalized;
            // Debug.Log($"newAim {newAim}");
            // var rotationZ = Mathf.Atan2(newAim.x, newAim.z) * Mathf.Rad2Deg;
            // transform.rotation = Quaternion.Euler(0, rotationZ, 0);

            // _mousePosition = _heroInput.Hero.MouseLook.ReadValue<Vector2>();


            // if (_mouseLook.magnitude <= _thresholdMagnitude)
            //     return;
            //
            // _ray = _camera.ScreenPointToRay(_mouseLook);
            //
            // if (Physics.Raycast(_ray, out _hit))
            //     _rotationTarget = _hit.point;
            //
            // _lookPosition = _rotationTarget - transform.position;
            // _lookPosition.y = 0;
            // _rotation = Quaternion.LookRotation(_lookPosition);
            // _aimDirection = new Vector3(_rotationTarget.x, 0f, _rotationTarget.z);
            //
            // if (_aimDirection != Vector3.zero && _mouseLook.magnitude > _thresholdMagnitude)
            // {
            //     Debug.Log($"rotation {_rotation}");
            //     transform.rotation =
            //         Quaternion.Slerp(transform.rotation, _rotation, _rotationSpeed * Time.deltaTime);
            // }
            // else
            // {
            //     transform.rotation = transform.rotation;
            // }

            var (success, position) = GetMousePosition();

            if (success)
            {
                var direction = position - transform.position;
                direction.y = 0;
                transform.forward = direction;
            }
        }

        private IEnumerator RotateCoroutine()
        {
            _ray = _camera.ScreenPointToRay(_mouseLook);

            if (Physics.Raycast(_ray, out _hit))
                _rotationTarget = _hit.point;

            _lookPosition = _rotationTarget - transform.position;
            _lookPosition.y = 0;
            _rotation = Quaternion.LookRotation(_lookPosition);
            // Debug.Log($"rotation {_rotation}");
            _aimDirection = new Vector3(_rotationTarget.x, 0f, _rotationTarget.z);

            while (_aimDirection != Vector3.zero)
            {
                transform.rotation =
                    Quaternion.Slerp(transform.rotation, _rotation, _rotationSpeed * Time.deltaTime);

                yield return null;
            }
        }

        private void OnMouseLook(InputAction.CallbackContext context)
        {
            _tempMouseLook = context.ReadValue<Vector2>();
            // Debug.Log($"tempMouseLook {_tempMouseLook}");
            // Debug.Log($"tempMouseLook magnitude {_tempMouseLook.magnitude}");

            if (_tempMouseLook.magnitude > _thresholdMagnitude)
            {
                _mouseLook = _tempMouseLook;

                // if (_rotateCoroutine != null)
                //     StopCoroutine(_rotateCoroutine);
                //
                // _rotateCoroutine = StartCoroutine(RotateCoroutine());
            }
            else
            {
                _mouseLook = Vector2.zero;
                // StopCoroutine(_rotateCoroutine);
            }

            // _mouseLook = context.ReadValue<Vector2>();
            Debug.Log($"OnMouseLook");
            Debug.Log($"mouseLook.magnitude {_mouseLook.magnitude}");
            Debug.Log($"tempMouseLook.magnitude {_tempMouseLook.magnitude}");
        }

        private void OnMouseLookStop(InputAction.CallbackContext context)
        {
            _mouseLook = Vector2.zero;
            Debug.Log($"OnMouseLookStop");
            Debug.Log($"mouseLook.magnitude {_mouseLook.magnitude}");
            // StopCoroutine(_rotateCoroutine);
        }

        private (bool success, Vector3 position) GetMousePosition()
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, groundMask))
                return (success: true, position: hitInfo.point);
            else
                return (success: false, position: Vector3.zero);
        }
    }
}