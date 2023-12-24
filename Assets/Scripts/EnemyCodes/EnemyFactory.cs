using System;
using Managers;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;


namespace EnemyCodes
{
    public class EnemyFactory : MonoBehaviour
    {
        public Transform[] portals;

        public Transform enemyParent;

        public float timer;

        public int[] spawnFrequencyTableSec;


        private void OnEnable()
        {
            GameManager.Instance.GameLevelUp.AddListener(RefreshSpawnTimer);
        }


        // Update is called once per frame
        void Update()
        {
            timer += Time.deltaTime;

            if (timer >= spawnFrequencyTableSec[GameManager.Instance.GameLevel])
            {
                timer = 0f;
                EnemySpawn();
            }
            // if (Keyboard.current.spaceKey.wasPressedThisFrame)
            // {
            //     GameManager.Instance.pool.Spawn(0);
            // }
        }

        private Vector2 RandomPosition()
        {
            var ran = Random.Range(0, portals.Length);
            return portals[ran].position;
        }

        private void EnemySpawn()
        {
            var enemy = GameManager.Instance.Pool.Spawn(GameManager.Instance.GameLevel, new Vector2(1000, 1000));
            enemy.SetActive(false);
            enemy.transform.SetParent(enemyParent);
            enemy.transform.position = RandomPosition();
            enemy.SetActive(true);
            enemy.GetComponent<EnemyController>().InitRecycle();
        }

        private void RefreshSpawnTimer()
        {
            timer = 0f;
        }
    }
}