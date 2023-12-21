using UnityEngine;

namespace WeaponCodes
{
    public abstract class Weapon : MonoBehaviour
    {
        public int id;

        public int prefabId;

        public int count;
        
        public int weaponLevel;
    }
}