using Managers;
using UnityEngine;
using UnityEngine.InputSystem;


namespace EnemyCodes
{
    public class EnemyFactory : MonoBehaviour
    {
        public Transform[] portals;

        public Transform enemyParent;

        public float timer;


        // Update is called once per frame
        void Update()
        {
            timer += Time.deltaTime;

            if (timer >= 1f)
            {
                timer = 0f;
                RandomEnemySpawn();
            }
            // if (Keyboard.current.spaceKey.wasPressedThisFrame)
            // {
            //     GameManager.Instance.pool.Spawn(0);
            // }
        }

        public Vector2 RandomPosition()
        {
            var ran = Random.Range(0, portals.Length);
            return portals[ran].position;
        }

        private void RandomEnemySpawn()
        {
            var enemy = GameManager.Instance.pool.Spawn(Random.Range(0, 2));
            enemy.transform.SetParent(enemyParent);
            enemy.transform.position = RandomPosition();
            enemy.GetComponent<EnemyController>().InitRecycle();
        }
    }
}