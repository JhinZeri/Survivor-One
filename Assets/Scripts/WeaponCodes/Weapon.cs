using UnityEngine;
using UnityEngine.Events;

namespace WeaponCodes
{
    public abstract class Weapon : MonoBehaviour
    {
        [Header("Events")] public UnityEvent weaponLevelUp;

        public int id;

        public int prefabId;

        public int count;

        public int weaponLevel;

        public abstract void LevelUp();
    }
}