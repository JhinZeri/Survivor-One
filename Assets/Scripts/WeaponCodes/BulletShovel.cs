using EnemyCodes;
using UnityEngine;

namespace WeaponCodes
{
    public class BulletShovel : Bullet
    {
        // Update is called once per frame
        void Update()
        {
            bodyRange.Pulse();
            if (bodyRange.GetNearestDetection())
            {
                foreach (var enemyObj in bodyRange.GetDetections())
                {
                    var enemy = enemyObj.GetComponent<EnemyController>();
                    if (!enemy.isOnHit)
                    {
                        enemy.InHit(damage);
                    }
                }
            }
        }
    }
}