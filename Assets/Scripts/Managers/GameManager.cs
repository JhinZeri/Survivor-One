using System;
using EnemyCodes;
using PlayerCodes;
using UnityEngine.InputSystem;
using Utility;

public enum GamePhase
{
    Dev,Release
}
namespace Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public GamePhase phase;
        public PlayerController playerControl;

        public PoolManager pool;

        public EnemyFactory enemyFactory;

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this);
        }

        private void Update()
        {
            
        }

        private void Start()
        {
            // playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }
    }
}