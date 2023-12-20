using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "DATA/EnemyData", fileName = "EnemyTemplateSO")]
    public class EnemyDataSO : ScriptableObject
    {
        public int speed;
        public int maxHealth;
    }
}