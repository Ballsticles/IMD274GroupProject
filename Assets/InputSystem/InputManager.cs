
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

using static PlayerInputActions;

[CreateAssetMenu(fileName = "InputManager", menuName = "Platformer/InputManager")]
public class InputManager : ScriptableObject, IPlayerActions
{
    public event UnityAction<Vector2> Move = delegate {  };
    public event UnityAction<Vector2, bool> Look = delegate { };
    public event UnityAction EnableMouseControlCamera = delegate { };
    public event UnityAction DisableMouseControlCamera = delegate { };
    public event UnityAction<bool> Jump = delegate { };

    PlayerInputActions inputActions;

    public Vector3 Direction => inputActions.Player.Move.ReadValue<Vector2>();


    private void OnEnable()
    {
        if (inputActions == null)
        {
            inputActions = new PlayerInputActions();
            inputActions.Player.SetCallbacks(this);
        }
        inputActions.Enable();
        
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    public void EnablePlayerActions()
    {
        inputActions.Enable();
        
    }


    public void OnAttack(InputAction.CallbackContext context)
    {
        //noop
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
       //noop
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        //noop
    }

    public void OnJump(InputAction.CallbackContext context)
    {
       switch (context.phase)
        {
            case InputActionPhase.Started:
                Jump.Invoke(true);
                break;
            case InputActionPhase.Canceled:
                Jump.Invoke(false);
                break;
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Look.Invoke(context.ReadValue<Vector2>(), IsDeviceMouse(context));
    }


    bool IsDeviceMouse(InputAction.CallbackContext context)
    {
        return context.control.device.name == "Mouse";
    }


    public void OnMouseControlCamera(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                EnableMouseControlCamera(); break;
            case InputActionPhase.Canceled:
                DisableMouseControlCamera(); break;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Move.Invoke(context.ReadValue<Vector2>());
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        //noop
    }
}
