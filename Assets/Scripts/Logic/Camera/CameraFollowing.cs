using UnityEngine;

namespace Logic.Camera
{
    public class CameraFollowing : MonoBehaviour
    {
        [SerializeField] private Transform _following;
        [SerializeField] private float _distance;

        private void LateUpdate()
        {
            if (_following == null)
                return;

            transform.position = _following.position + new Vector3(0, _distance, 0);
        }
    }
}