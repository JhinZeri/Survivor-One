using System;
using UnityEngine;

namespace PlayerCodes
{
    public class SpriteControl : MonoBehaviour
    {
        public SpriteRenderer playerSprite;

        private void Awake()
        {
            playerSprite = GetComponent<SpriteRenderer>();
        }

        public void CorrectSpriteDirection(Vector2 moveInput)
        {
            if (moveInput.x == 0) return;

            if (moveInput.x < 0)
            {
                playerSprite.flipX = true;
            }

            if (moveInput.x > 0)
            {
                playerSprite.flipX = false;
            }
        }
    }
}