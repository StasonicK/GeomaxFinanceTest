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

        private UnityEngine.Camera _camera;
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
            _camera = UnityEngine.Camera.main;
            _heroInput = new HeroInput();
            _currentRotation = transform.rotation;
        }

        private void Start() => 
            Cursor.visible = false;

        private void OnEnable() => 
            _heroInput.Enable();

        private void OnDisable() => 
            _heroInput.Disable();

        private void Update() =>
            Rotate();

        private void Rotate()
        {
            var (success, position) = GetMousePosition();

            if (success)
            {
                var direction = position - transform.position;
                direction.y = 0;
                transform.forward = direction;
            }
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