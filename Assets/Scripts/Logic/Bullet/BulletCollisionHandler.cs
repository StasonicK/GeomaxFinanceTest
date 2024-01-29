using UnityEngine;

namespace Logic.Bullet
{
    public class BulletCollisionHandler : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag(Constants.GroundTag))
                Destroy(gameObject);
            else if (other.gameObject.CompareTag(Constants.ObstacleTag))
                Destroy(gameObject);
        }
    }
}