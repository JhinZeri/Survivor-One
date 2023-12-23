using Micosmo.SensorToolkit;
using UnityEngine;

namespace WeaponCodes
{
    public abstract class Bullet : MonoBehaviour
    {
        public int damage;
        
        public float force;

        public float attackFrequency;

        public int penetration;

        public RangeSensor2D bodyRange;
    }
}