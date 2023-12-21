using UnityEngine;
using UnityEngine.InputSystem;

namespace TestCodes
{
    public class RotationTest : MonoBehaviour
    {
        public bool worldSpace;
        float m_speed;

        void Start()
        {
            //Set the speed of the rotation
            m_speed = 20.0f;
            //Rotate the GameObject a little at the start to show the difference between Space and Local
            transform.Rotate(60, 0, 60);
        }

        void Update()
        {
            //Rotate the GameObject in World Space if in the m_WorldSpace state
            if (worldSpace)
                transform.Rotate(Vector3.up * m_speed * Time.deltaTime, Space.World);
            //Otherwise, rotate the GameObject in local space
            else
                transform.Rotate(Vector3.up * m_speed * Time.deltaTime, Space.Self);

            //Press the Space button to switch between world and local space states
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                //Make the current state switch to the other state
                worldSpace = !worldSpace;
                //Output the Current state to the console
                Debug.Log("World Space : " + worldSpace.ToString());
            }
        }
    }
}
