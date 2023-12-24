using System.Collections.Generic;
using EnemyCodes;
using Micosmo.SensorToolkit;
using PlayerCodes;
using UnityEngine;
using UnityEngine.Events;
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
        public GamePhase Phase;

        [Header("GAME INFO")] public float GameTime;
        public float MaxGameTime = 2 * 10f;
        public int GameLevel;

        [Header("GAME OBJECT")] public PoolManager Pool;
        public EnemyFactory EnemyFactory;
      //  public RangeSensor2D RecycleSensor;

        [Header("GAME DATA")] public int[] LevelTimeTableSec ;
        public GameObject[] GameInitPrefabs;
        public List<GameObject> RuntimeGameObjects;
        public int[] PlayerLevelUpExpTable = { 5, 15, 30, 60 };

        [Header("PLAYER")] public PlayerController PlayerControl;
        public int CurLevel;
        public int KillCount;
        public float PlayerExp;

        [Header("EVENT")] public UnityEvent GameLevelUp;
        public UnityEvent PlayerLevelUp;

        protected override void Awake()
        {
            base.Awake();
            GameInit();
            DontDestroyOnLoad(this);
        }

        private void Update()
        {
            GameTime += Time.deltaTime;

            if (GameTime >= LevelTimeTableSec[Mathf.Min(GameLevel + 1, 1)])
            {
                if (GameLevel < 1) GameLevelUp?.Invoke();

                GameLevel = 1;
            }
            else
            {
                GameLevel = 0;
            }


            if (GameTime >= MaxGameTime) Debug.Log("Game Over");

            // gameLevel = Mathf.Min(Mathf.FloorToInt(gameTime / 10), 1);
        }

        protected override void OnDestroy()
        {
            foreach (var obj in RuntimeGameObjects) Destroy(obj);

            GameLevelUp.RemoveAllListeners();

            base.OnDestroy();
        }

        private void GameInit()
        {
            var enemyFactoryObj = Instantiate(GameInitPrefabs[0], null);
            EnemyFactory = enemyFactoryObj.GetComponent<EnemyFactory>();
            RuntimeGameObjects.Add(enemyFactoryObj);
        }

        public void GetExp(int exp)
        {
            PlayerExp += exp;
            if (PlayerExp < PlayerLevelUpExpTable[CurLevel - 1]) return;
            PlayerExp = 0;
            PlayerLevelUp?.Invoke();
        }
    }
}