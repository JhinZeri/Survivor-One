using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "DATA/Player", fileName = "PlayerData")]
    public class PlayerDataSO : ScriptableObject
    {
        public int initialSpeed;
        public int maxHealth;
    }
}