using UnityEngine;

namespace Logic.Bullet
{
    public class BulletLifeTime : MonoBehaviour
    {
        [SerializeField] private float _lifeTime;

        private float _currentTime;

        private void Update()
        {
            if (_currentTime >= _lifeTime)
                Destroy(gameObject);

            _currentTime += Time.deltaTime;
        }
    }
}