using System;
using Managers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerCodes
{
    public enum PlayerStates
    {
        Idle,
        Run,
        Dead
    }

    public class PlayerController : MonoBehaviour
    {
        public PlayerStates playerState;
        public Vector2 moveVector2;
        public float speed;

        private PlayerInput m_input;

        private Rigidbody2D m_rigid;

        [Header("子物体")] public SpriteControl spriteControl;

        public SpriteRenderer shadow;


        private void Awake()
        {
            m_input = GetComponent<PlayerInput>();
            m_rigid = GetComponent<Rigidbody2D>();

            // GameManager.Instance.playerControl = this;
        }


        private void OnEnable()
        {
            m_input.actions.Enable();
        }

        private void OnDisable()
        {
            m_input.actions.Disable();
        }


        private void FixedUpdate()
        {
            m_rigid.velocity = moveVector2 * (speed * Time.fixedDeltaTime);
        }

        // Update is called once per frame
        void Update()
        {
            moveVector2 = m_input.currentActionMap.FindAction("Move").ReadValue<Vector2>();

            SwitchState();

            spriteControl.CorrectSpriteDirection(moveVector2);
            spriteControl.PlayAnimation(playerState);

            if (playerState == PlayerStates.Dead)
            {
                shadow.gameObject.SetActive(false);
            }
            else
            {
                shadow.gameObject.SetActive(true);
            }
        }

        /// <summary>
        /// 切换角色状态
        /// </summary>
        private void SwitchState()
        {
            if (Keyboard.current.oKey.wasPressedThisFrame)
            {
                playerState = PlayerStates.Dead;
                return;
            }

            if (playerState != PlayerStates.Dead)
            {
                playerState = moveVector2 == Vector2.zero ? PlayerStates.Idle : PlayerStates.Run;
            }
            else
            {
                if (Keyboard.current.pKey.wasPressedThisFrame)
                {
                    playerState = PlayerStates.Idle;
                }
            }
        }
    }
}