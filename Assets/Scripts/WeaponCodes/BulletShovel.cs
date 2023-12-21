using EnemyCodes;
using Micosmo.SensorToolkit;
using UnityEngine;

namespace WeaponCodes
{
    public class BulletShovel : Bullet
    {

        public void HitEnemy(GameObject enemyObj, Sensor sensor)
        {
            var enemy = enemyObj.GetComponent<EnemyController>();
            enemy.UnderHit(damage);
        }
    }
}