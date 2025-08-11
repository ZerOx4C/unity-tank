using System;
using UnityEngine;

namespace Runtime
{
    public class TankBehaviour : MonoBehaviour
    {
        public float forceFactor = 1;

        private Rigidbody _rigidbody;
        private TankModel _tankModel;
        private float _leftTrackForce;
        private float _rightTrackForce;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (_tankModel)
            {
                AddTrackForce(_tankModel.LeftTrack, _leftTrackForce * forceFactor);
                AddTrackForce(_tankModel.RightTrack, _rightTrackForce * forceFactor);
            }
        }

        public void SetTankModel(TankModel model)
        {
            if (_tankModel)
            {
                Destroy(_tankModel.gameObject);
            }

            if (!model)
            {
                _tankModel = null;
                return;
            }

            model.transform.SetParent(transform, false);
            model.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            _tankModel = model;
        }

        public void SetLeftTrackForce(float force)
        {
            _leftTrackForce = force;
        }

        public void SetRightTrackForce(float force)
        {
            _rightTrackForce = force;
        }

        private void AddTrackForce(Transform track, float force)
        {
            if (track)
            {
                _rigidbody.AddForceAtPosition(track.forward * force, track.position, ForceMode.Acceleration);
            }
        }
    }
}
