using UnityEngine;
using UnityEngine.Serialization;

namespace Runtime
{
    [CreateAssetMenu(fileName = "RuntimeSettings", menuName = "ScriptableObjects/RuntimeSettings")]
    public class RuntimeSettings : ScriptableObject
    {
        [SerializeField] private TankBehaviour tankBehaviourPrefab;

        public TankBehaviour TankBehaviourPrefab => tankBehaviourPrefab;
    }
}
