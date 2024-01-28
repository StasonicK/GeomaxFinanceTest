using UnityEngine;

namespace Logic.Hero
{
    public class HeroRotation : MonoBehaviour
    {
        [SerializeField] private float _rotationHorizontalSpeed = 0.5f;

        private HeroInput _heroInput;
        private float _xAxis;
        private Vector2 _delta;
        private Vector3 _newFollowPos;
        private Vector3 _eulerAngles;

        private void Awake()
        {
            _heroInput = new HeroInput();
            _eulerAngles = transform.eulerAngles;
        }

        private void OnEnable() =>
            _heroInput.Enable();

        private void OnDisable() =>
            _heroInput.Disable();

        private void Update() =>
            Rotate();

        private void Rotate()
        {
            _delta = _heroInput.Hero.Aim.ReadValue<Vector2>();
            _xAxis += _delta.x * _rotationHorizontalSpeed;
            _eulerAngles = new Vector3(_eulerAngles.x, _xAxis, _eulerAngles.z);
            transform.eulerAngles = _eulerAngles;
        }
    }
}