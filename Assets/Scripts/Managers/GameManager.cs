using System;
using System.Collections.Generic;
using EnemyCodes;
using PlayerCodes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Utility;

public enum GamePhase
{
    Dev,
    Release
}

namespace Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public GamePhase phase;
        
        public float gameTime;
        public float maxGameTime = 2 * 10f;
        public int gameLevel = 0;
        
        public PlayerController playerControl;
        public PoolManager pool;
        public EnemyFactory enemyFactory;
        
        public int[] levelTimeTableSec;
        public GameObject[] gameInitPrefabs;
        public List<GameObject> runtimeGameObjects;

        public UnityEvent gameLevelUp;

        protected override void Awake()
        {
            base.Awake();
            GameInit();
            DontDestroyOnLoad(this);
        }

        private void Update()
        {
            gameTime += Time.deltaTime;

            if (gameTime >= levelTimeTableSec[Mathf.Min(gameLevel + 1, 1)])
            {
                if (gameLevel < 1)
                {
                    gameLevelUp?.Invoke();
                }

                gameLevel = 1;
            }
            else
            {
                gameLevel = 0;
            }


            if (gameTime >= maxGameTime)
            {
                Debug.Log("Game Over");
            }

            // gameLevel = Mathf.Min(Mathf.FloorToInt(gameTime / 10), 1);
        }

        private void GameInit()
        {
            GameObject enemyFactoryObj = Instantiate(gameInitPrefabs[0], null);
            enemyFactory = enemyFactoryObj.GetComponent<EnemyFactory>();
            runtimeGameObjects.Add(enemyFactoryObj);
        }

        protected override void OnDestroy()
        {
            foreach (var obj in runtimeGameObjects)
            {
                Destroy(obj);
            }

            gameLevelUp.RemoveAllListeners();

            base.OnDestroy();
        }
    }
}