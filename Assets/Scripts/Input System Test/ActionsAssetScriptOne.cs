using UnityEngine;
using UnityEngine.InputSystem;

public class ActionsAssetScriptOne : MonoBehaviour
{
    // assign the actions asset to this field in the inspector:
    public InputActionAsset actions;

    // private field to store move action reference
    private InputAction _moveAction;
    private Keyboard _keyboard;

    void Awake()
    {
        // find the "move" action, and keep the reference to it, for use in Update
        _moveAction = actions.FindActionMap("Player").FindAction("Move");

        // for the "jump" action, we add a callback method for when it is performed
        actions.FindActionMap("Player").FindAction("Fire").performed += OnFire;
    }

    void Update()
    {
        _keyboard = Keyboard.current;
        Vector2 moveVector = _moveAction.ReadValue<Vector2>();

        if (_keyboard.kKey.wasPressedThisFrame)
        {
            Debug.Log("Move:" + moveVector);
        }
    }

    private void OnFire(InputAction.CallbackContext context)
    {
        Debug.Log("Fire!");
    }

    void OnEnable()
    {
        actions.FindActionMap("Player").Enable();
    }

    void OnDisable()
    {
        actions.FindActionMap("Player").Disable();
    }
}