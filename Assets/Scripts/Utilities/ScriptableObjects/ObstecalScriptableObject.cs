using UnityEngine;

namespace Assets.Scripts.Utilities
{
    [CreateAssetMenu(fileName = "ObstacleScriptableObject", menuName = "ScriptableObject/ObstacleScriptableObject")]
    public class ObstacleScriptableObject : ScriptableObject
    {
        public ObstacleType obstacleType;
        public GameObject prefab;
    }

    public enum ObstacleType
    {
        Train1,
        Train2,
        Train3,
        Blocker,
    }
}