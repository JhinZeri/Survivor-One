using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerCodes
{
    public class PlayerController : MonoBehaviour
    {
        public Vector2 moveVector2;
        public float speed;

        private PlayerInput m_input;

        private Rigidbody2D m_rigid;


        private SpriteControl m_spriteControl;

        private void Awake()
        {
            m_input = GetComponent<PlayerInput>();
            m_rigid = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            m_spriteControl = GetComponentInChildren<SpriteControl>();
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
            m_spriteControl.CorrectSpriteDirection(moveVector2);
        }
    }
}