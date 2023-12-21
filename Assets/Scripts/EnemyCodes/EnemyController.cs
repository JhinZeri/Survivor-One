using System;
using Managers;
using Micosmo.SensorToolkit;
using ScriptableObjects;
using UnityEngine;

namespace EnemyCodes
{
    [Serializable]
    public class EnemyRuntimeData
    {
        public int currentHealth;
        public int currentSpeed;
        public float currentHitCooldown;
    }

    public class EnemyController : MonoBehaviour
    {
        public EnemyDataSO enemyDataSoTemplate;
        public EnemyRuntimeData runtimeData;
        public GamePhase phase;
        public Rigidbody2D targetPlayer;
        public RangeSensor2D bodyRangeSensor;

        private Rigidbody2D m_rigid;

        private SpriteRenderer m_sprite;

        private bool m_isLive = true;
        private bool m_isContactPlayer;


        private void Awake()
        {
            m_rigid = GetComponent<Rigidbody2D>();
            m_sprite = GetComponent<SpriteRenderer>();

            DataInit();
        }


        void Start()
        {
            phase = GameManager.Instance.phase;
            // targetPlayer = GameManager.Instance.playerControl.GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            if (!m_isLive) return;
            if (phase == GamePhase.Dev)
            {
                LockTarget();
            }
        }

        private void FixedUpdate()
        {
            if (!m_isLive) return;
            FollowTarget();
        }


        /// <summary>
        /// 敌人初始化，锁定敌人
        /// </summary>
        public void InitRecycle()
        {
            targetPlayer = GameManager.Instance.playerControl.GetComponent<Rigidbody2D>();
            m_isLive = true;
            DataInit();
        }

        public void DataInit()
        {
            runtimeData.currentSpeed = enemyDataSoTemplate.speed;
            runtimeData.currentHealth = enemyDataSoTemplate.maxHealth;
            runtimeData.currentHitCooldown = enemyDataSoTemplate.hitCooldown;
        }

        public void UnderHit(int underDamage)
        {
            runtimeData.currentHealth -= underDamage;

            if (runtimeData.currentHealth <= 0)
            {
                m_isLive = false;
                Dead();
            }
        }

        private void Dead()
        {
            GameManager.Instance.pool.DeSpawn(gameObject);
        }

        /// <summary>
        /// 锁定目标,Dev模式
        /// </summary>
        private void LockTarget()
        {
            if (targetPlayer == null && GameManager.Instance.playerControl != null)
            {
                targetPlayer = GameManager.Instance.playerControl.GetComponent<Rigidbody2D>();
            }
        }

        /// <summary>
        /// 追踪目标中
        /// </summary>
        private void FollowTarget()
        {
            if (!targetPlayer) return;
            if (bodyRangeSensor != null)
            {
                bodyRangeSensor.Pulse();
                m_isContactPlayer = bodyRangeSensor.GetNearestDetection();
            }

            Vector2 dir = (targetPlayer.position - m_rigid.position).normalized;
            if (m_isContactPlayer)
            {
                m_rigid.velocity = Vector2.zero;

                // 轻微击退玩家，保留很小的速度
                // m_rigid.velocity = speed / 50 * Time.fixedDeltaTime * dir;

                return;
            }

            m_rigid.velocity = runtimeData.currentSpeed * Time.fixedDeltaTime * dir;

            CorrectDirection();
        }

        /// <summary>
        /// 调整图片方向
        /// </summary>
        private void CorrectDirection()
        {
            if (m_rigid.velocity.x == 0) return;

            if (m_rigid.velocity.x < 0)
            {
                m_sprite.flipX = true;
            }

            if (m_rigid.velocity.x > 0)
            {
                m_sprite.flipX = false;
            }
        }
    }
}