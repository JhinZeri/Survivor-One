using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActionsAssetScriptTwo : MonoBehaviour
{
    private InputControls _inputControls;

    private void Awake()
    {
        _inputControls = new InputControls();

        _inputControls.Player.Fire.performed += FireOnperformed;
    }

    private void FireOnperformed(InputAction.CallbackContext obj)
    {
        Debug.Log("开始射击！");
    }

    private void OnEnable()
    {
        _inputControls.Enable();
    }

    private void OnDisable()
    {
        _inputControls.Disable();
    }

    
}