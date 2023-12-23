using System;
using EnemyCodes;
using Managers;
using Micosmo.SensorToolkit;
using UnityEngine;

namespace WeaponCodes
{
    public class BulletGun : Bullet
    {
        public float speed;
        public int curPenetration;
        private Rigidbody2D m_rigid;

        private void Awake()
        {
            m_rigid = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            OutMapRecycleCheck();
        }

        public void Init(float bulletSpeed, Vector2 dir)
        {
            curPenetration = penetration;
            speed = bulletSpeed;
            m_rigid.velocity = dir * speed;
        }

        public void ContactEnemy(GameObject enemyObj, Sensor sensor)
        {
            var enemy = enemyObj.GetComponent<EnemyController>();
            enemy.UnderHit(damage,force);
            curPenetration--;
            if (curPenetration < 0)
            {
                GameManager.Instance.pool.DeSpawn(gameObject);
                curPenetration = penetration;
            }
        }

        private void OutMapRecycleCheck()
        {
            if (Vector3.Distance(transform.position, Vector3.zero) >= 50)
            {
                GameManager.Instance.pool.DeSpawn(gameObject);
                curPenetration = penetration;
            }
        }
    }
}