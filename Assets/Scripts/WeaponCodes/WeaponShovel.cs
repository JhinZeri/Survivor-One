using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace WeaponCodes
{
    public class WeaponShovel : Weapon
    {
        private void Start()
        {
            ResetBulletShovel();
        }

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(Vector3.back * (150 * Time.deltaTime));
        }

        private void ResetBulletShovel()
        {
            for (int index = 0; index < count; index++)
            {
                var bullet = GameManager.Instance.pool.Spawn(2).transform;
                bullet.transform.SetParent(transform);
                var rot = 360 / count;
                bullet.Rotate(Vector3.forward * index * rot);
                bullet.Translate(bullet.up * 1.5f, Space.World);
            }
        }
    }
}