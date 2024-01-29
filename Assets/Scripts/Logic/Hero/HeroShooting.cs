using UnityEngine;

namespace Logic.Hero
{
    public class HeroShooting : MonoBehaviour
    {
        [SerializeField] private GameObject _projectilePrefab;
        [SerializeField] private Transform _prefabSpawn;
        [SerializeField] private Transform _aimedTransform;


        private void Shoot()
        {
            var projectile = Instantiate(_projectilePrefab, _prefabSpawn.position, Quaternion.identity);
            projectile.transform.forward = _aimedTransform.forward;
        }
    }
}