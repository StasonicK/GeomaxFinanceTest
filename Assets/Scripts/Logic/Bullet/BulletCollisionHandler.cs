using UnityEngine;

namespace Logic.Bullet
{
    public class BulletCollisionHandler : MonoBehaviour
    {
        [SerializeField] private GameObject _parent;

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag(Constants.GroundTag))
                Destroy(_parent);
            else if (other.gameObject.CompareTag(Constants.ObstacleTag))
                Destroy(_parent);
        }
    }
}