using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace WeaponCodes
{
    public class WeaponShovel : Weapon
    {
        [Header("Shovel Property")] public int rotateSpeedSec;

        private void Start()
        {
            ResetBulletShovel();
        }
        
        void Update()
        {
            // 此时本地和世界坐标系一样，所以第二个参数不影响结果
            transform.Rotate(Vector3.back * (rotateSpeedSec * Time.deltaTime));

            if (GameManager.Instance.Phase == GamePhase.Dev)
            {
                if (Keyboard.current.spaceKey.wasPressedThisFrame)
                {
                    LevelUp();
                }
            }
        }

        private void ResetBulletShovel()
        {
            if (transform.childCount > 0)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    GameManager.Instance.Pool.DeSpawn(transform.GetChild(i).gameObject);
                }
            }

            for (int index = 0; index < count; index++)
            {
                var bullet = GameManager.Instance.Pool.Spawn(prefabId).transform;
                bullet.transform.SetParent(transform);

                bullet.localPosition = Vector3.zero;
                bullet.localRotation = Quaternion.identity;

                var rot = 360 / count;
                bullet.Rotate(Vector3.back * index * rot);
                bullet.Translate(bullet.up * 1.5f, Space.World);
            }
        }

        protected override void LevelUp()
        {
            weaponLevel += 1;
            count += 1;
            weaponLevelUp?.Invoke();
        }
    }
}