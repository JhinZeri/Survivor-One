using System;
using Managers;
using Micosmo.SensorToolkit;
using UnityEngine;

namespace EnemyCodes
{
    public class EnemyController : MonoBehaviour
    {
        public GamePhase phase;
        public Rigidbody2D targetPlayer;
        public float speed;

        private Rigidbody2D m_rigid;

        private SpriteRenderer m_sprite;

        private bool m_isLive = true;
        private bool m_isContactPlayer;

        public RangeSensor2D bodyRangeSensor;

        private void Awake()
        {
            m_rigid = GetComponent<Rigidbody2D>();
            m_sprite = GetComponent<SpriteRenderer>();
        }


        // Start is called before the first frame update
        void Start()
        {
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
            m_rigid.position = GameManager.Instance.enemyFactory.RandomPosition();
            targetPlayer = GameManager.Instance.playerControl.GetComponent<Rigidbody2D>();
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

            m_rigid.velocity = speed * Time.fixedDeltaTime * dir;

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