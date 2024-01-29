using UnityEngine;

namespace Logic.Hero
{
    public class LaserDrawer : MonoBehaviour
    {
        [SerializeField] private LineRenderer _laserRenderer;
        [SerializeField] private LayerMask _laserMask;
        [SerializeField] private float _laserLength;
        [SerializeField] private Transform _aimedTransform;

        private void Start()
        {
            if (_laserRenderer != null)
            {
                _laserRenderer.SetPositions(new Vector3[]
                {
                    Vector3.zero,
                    Vector3.zero
                });
            }
        }

        private void Update() =>
            RefreshLaser();

        private void RefreshLaser()
        {
            if (_laserRenderer == null)
                return;

            Vector3 lineEnd;

            if (Physics.Raycast(_aimedTransform.position, _aimedTransform.forward, out RaycastHit hitinfo, _laserLength,
                    _laserMask))
                lineEnd = hitinfo.point;
            else
                lineEnd = _aimedTransform.position + _aimedTransform.forward * _laserLength;

            _laserRenderer.SetPosition(1, _aimedTransform.InverseTransformPoint(lineEnd));
        }
    }
}