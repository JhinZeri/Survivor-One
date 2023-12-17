using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput _input;

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();

        _input.currentActionMap.FindAction("Fire", true).started += ctx => { Debug.Log("触发事件Started，输入系统接收到了"); };
        _input.currentActionMap.FindAction("Fire", true).performed += ctx =>
        {
            Debug.Log("触发事件Performed，表示按键已经按下了，交互完成");
        };
        _input.currentActionMap["Fire"].canceled += ctx => { Debug.Log("触发事件Canceled，表示这个按键结束交互了"); };
    }


    // Update is called once per frame
    void Update()
    {
        Debug.Log(_input.currentActionMap.FindAction("Fire").phase);
        // if (_input.currentActionMap.FindAction("Fire").phase == InputActionPhase.Started)
        // {
        //     Debug.Log("开始进行射击");
        // }

        // if (_input.currentActionMap["Fire"].phase == InputActionPhase.Performed)
        // {
        //     Debug.Log("持续射击中");
        // }
        // if (_input.currentActionMap["Fire"].IsPressed())
        // {
        //    
        // }
    }
}