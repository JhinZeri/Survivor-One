using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class EmbeddedScript : MonoBehaviour
{
    public InputAction moveAction;

    public InputAction jumpAction;

    private void Awake()
    {
        jumpAction.started += OnJump;
        jumpAction.performed += ctx => { OnJump(ctx); };
        jumpAction.canceled += JumpActionOncanceled;
    }

    private void JumpActionOncanceled(InputAction.CallbackContext obj)
    {
        Debug.Log("取消跳跃");
    }

    private void JumpActionOnperformed(InputAction.CallbackContext obj)
    {
        Debug.Log("持续跳跃中");
    }

    void Update()
    {
        Vector2 moveAmount = moveAction.ReadValue<Vector2>();
        // if (moveAmount != Vector2.zero)
        // {
        //     Debug.Log("Move的向量为：" + moveAmount);
        // }
    }

    private void OnEnable()
    {
        moveAction.Enable();
        jumpAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        jumpAction.Disable();
    }

    private void OnJump(InputAction.CallbackContext obj)
    {
        Debug.Log("正在跳跃行为");
    }
}