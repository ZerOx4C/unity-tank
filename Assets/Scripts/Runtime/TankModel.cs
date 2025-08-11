using UnityEngine;
using UnityEngine.Serialization;

namespace Runtime
{
    public class TankModel : MonoBehaviour
    {
        [SerializeField] private Transform leftTrack;
        [SerializeField] private Transform rightTrack;
        [SerializeField] private Transform turret;
        [SerializeField] private Transform canonBarrel;

        public Transform LeftTrack => leftTrack;
        public Transform RightTrack => rightTrack;
        public Transform Turret => turret;
        public Transform CanonBarrel => canonBarrel;
    }
}
