using System;
using System.Collections.Generic;
using Managers;
using Micosmo.SensorToolkit;
using UnityEngine;

namespace WeaponCodes
{
    public class WeaponGun : Weapon
    {
        [Header("Gun Property")] public float bulletSpeed;

        public float scanRadius;

        // public Transform nearestEnemy;

        public List<GameObject> enemyList;

        public RangeSensor2D scanRange;

        public float attackFrequency;
        public float time;

        private void Awake()
        {
            scanRange.Circle.Radius = scanRadius;
        }
        

        // Update is called once per frame
        private void Update()
        {
            if (weaponLevel > 0)
            {
                time += Time.deltaTime;
                scanRange.Pulse();
                enemyList = scanRange.GetDetections();

                if (scanRange.GetNearestDetection() && time >= attackFrequency)
                {
                    Fire();
                }
            }
        }

        private void Fire()
        {
            time = 0f;
            var targetPos = scanRange.GetNearestDetection().transform.position;
            var dir = (targetPos - transform.position).normalized;
            var bullet = GameManager.Instance.Pool.Spawn(prefabId, transform, transform.position)
                .GetComponent<BulletGun>();
            bullet.transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
            bullet.Init(bulletSpeed, dir);
        }

        protected override void LevelUp()
        {
            throw new NotImplementedException();
        }
    }
}