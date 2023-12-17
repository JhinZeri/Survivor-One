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


        public void CorrectSpriteDirection(Vector2 moveInput)
        {
            if (moveInput.x == 0) return;

            if (moveInput.x < 0)
            {
                m_playerSprite.flipX = true;
            }

            if (moveInput.x > 0)
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