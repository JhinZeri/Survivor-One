using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "DATA/Player", fileName = "PlayerData")]
    public class PlayerDataSO : ScriptableObject
    {
        public int currentHealth;
        public int speed;
        public int maxHealth;
    }
}