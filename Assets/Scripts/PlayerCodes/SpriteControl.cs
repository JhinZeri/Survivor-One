using System;
using UnityEngine;

namespace PlayerCodes
{
    public class SpriteControl : MonoBehaviour
    {
        private SpriteRenderer m_playerSprite;

        private Animator m_animator;


        private void Awake()
        {
            m_playerSprite = GetComponent<SpriteRenderer>();
            m_animator = GetComponent<Animator>();
        }


        /// <summary>
        /// 切换图片方向
        /// </summary>
        /// <param name="moveSpeed">速度Vector2</param>
        public void CorrectSpriteDirection(Vector2 moveSpeed)
        {
            if (moveSpeed.x == 0) return;

            if (moveSpeed.x < 0)
            {
                m_playerSprite.flipX = true;
            }

            if (moveSpeed.x > 0)
            {
                m_playerSprite.flipX = false;
            }
        }

        
        public void PlayAnimation(PlayerStates state)
        {
            switch (state)
            {
                case PlayerStates.Idle:
                    m_animator.Play("Idle");
                    break;
                case PlayerStates.Run:
                    m_animator.Play("Run");
                    break;
                case PlayerStates.Dead:
                    m_animator.Play("Dead");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }
    }
}