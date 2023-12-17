using UnityEngine;
using UnityEngine.InputSystem;

public class WorkflowDirect : MonoBehaviour
{
    private Keyboard _keyboard;

    // Update is called once per frame
    void Update()
    {
        _keyboard = Keyboard.current;

        if (_keyboard == null)
        {
            Debug.Log("没有发现键盘");
            return;
        }

        if (_keyboard[Key.A].wasPressedThisFrame)
        {
            Debug.Log("A键被按下了");
        }

        if (_keyboard.bKey.wasPressedThisFrame)
        {
            Debug.Log("B键被按下了");
        }

        if (_keyboard.ctrlKey.wasPressedThisFrame)
        {
            Debug.Log("Ctrl 键被按下了");
        }

        if (_keyboard.cKey.wasPressedThisFrame)
        {
            var num = _keyboard.cKey.ReadValue();
            Debug.Log("C键被按下，反馈值为" + num);
        }

        if (_keyboard.cKey.wasReleasedThisFrame)
        {
            var num = _keyboard.cKey.ReadValue();
            Debug.Log("C键松开，反馈值为" + num);
        }

        // var keyCNum = _keyboard.cKey.ReadValue();
        // Debug.Log("C键的值为" + keyCNum);
    }
}