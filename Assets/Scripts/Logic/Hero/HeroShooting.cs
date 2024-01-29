using Logic.Bullet;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Logic.Hero
{
    public class HeroShooting : MonoBehaviour
    {
        [SerializeField] private GameObject _projectilePrefab;
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private float _bulletForce = 20f;
        [SerializeField] private ShootVfx _shootVfx;

        private HeroInput _heroInput;
        private HeroAnimator _heroAnimator;
        private bool _isAvailable;

        private void Awake()
        {
            _heroInput = new HeroInput();
            _heroAnimator = GetComponent<HeroAnimator>();
        }

        private void OnEnable()
        {
            _heroInput.Hero.Aim.performed += EnableShoot;
            _heroInput.Hero.Aim.canceled += DisableShoot;
            _heroInput.Hero.Shoot.started += Shoot;
            _heroInput.Enable();
        }

        private void OnDisable()
        {
            _heroInput.Disable();
            _heroInput.Hero.Aim.performed -= EnableShoot;
            _heroInput.Hero.Aim.canceled -= DisableShoot;
            _heroInput.Hero.Shoot.started -= Shoot;
        }

        private void EnableShoot(InputAction.CallbackContext obj) =>
            _isAvailable = true;

        private void DisableShoot(InputAction.CallbackContext obj) =>
            _isAvailable = false;

        private void Shoot(InputAction.CallbackContext callbackContext)
        {
            var projectile = Instantiate(_projectilePrefab, _shootPoint.position, Quaternion.identity);
            projectile.transform.forward = _shootPoint.forward;
            projectile.GetComponentInChildren<Rigidbody>()
                .AddForce(_shootPoint.forward * _bulletForce, ForceMode.Impulse);
            _shootVfx.Show();
            _heroAnimator.PlayShoot();
        }
    }
}