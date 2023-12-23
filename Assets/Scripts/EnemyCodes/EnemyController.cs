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
        public Animator anim;
        public RangeSensor2D bodyRangeSensor;


        private Collider2D m_coll;
        private Rigidbody2D m_rigid;

        private SpriteRenderer m_sprite;

        private bool m_isLive = true;
        private bool m_isContactPlayer;
        private bool m_isOnHit;

        private static readonly int Hit = Animator.StringToHash("hit");
        private static readonly int Dead1 = Animator.StringToHash("dead");


        private void Awake()
        {
            m_rigid = GetComponent<Rigidbody2D>();
            m_sprite = GetComponent<SpriteRenderer>();
            m_coll = GetComponent<Collider2D>();
            anim = GetComponent<Animator>();
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
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Hit")) return;
            FollowTarget();
        }

        private void OnEnable()
        {
            m_isLive = true;
            anim.SetBool(Dead1, false);
            m_coll.enabled = true;
            m_rigid.simulated = true;
            m_sprite.sortingOrder = 2;
        }

        /// <summary>
        /// 敌人初始化，锁定敌人
        /// </summary>
        public void InitRecycle()
        {
            targetPlayer = GameManager.Instance.playerControl.GetComponent<Rigidbody2D>();
            DataInit();
        }

        private void DataInit()
        {
            runtimeData.currentSpeed = enemyDataSoTemplate.speed;
            runtimeData.currentHealth = enemyDataSoTemplate.maxHealth;
            runtimeData.currentHitCooldown = enemyDataSoTemplate.hitCooldown;
        }

        public void UnderHit(int underDamage, float force)
        {
            runtimeData.currentHealth -= underDamage;

            if (runtimeData.currentHealth >= 0)
            {
                anim.SetTrigger(Hit);
                m_rigid.velocity = Vector2.zero;
                if (force > 0)
                {
                    var playerPos = targetPlayer.position;
                    var dir = ((Vector2)transform.position - playerPos).normalized;
                    m_rigid.AddForce(dir * force, ForceMode2D.Impulse);
                }
            }
            else
            {
                m_isLive = false;
                anim.SetBool(Dead1, true);
                m_coll.enabled = false;
                m_rigid.simulated = false;
                m_sprite.sortingOrder = 1;
                GameManager.Instance.killCount += 1;
                GameManager.Instance.GetExp(1);
            }
        }

        public void Dead()
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