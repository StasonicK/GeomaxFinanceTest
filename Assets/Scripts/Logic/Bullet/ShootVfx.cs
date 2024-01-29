using UnityEngine;

namespace Logic.Bullet
{
    public class ShootVfx : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _vfx;

        private void Awake() =>
            _vfx.Stop();

        public void Show() =>
            _vfx.Play();
    }
}