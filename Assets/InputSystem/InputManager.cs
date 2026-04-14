
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
    public event UnityAction<bool> Grab = delegate { };
    public event UnityAction<bool> Attack = delegate { };

    public PlayerInputActions inputActions;

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
    public void DisablePlayerActions()
    {
        inputActions.Disable();
    }
    public void DisableMovementActions()
    {
        inputActions.FindAction("Move").Disable();
        inputActions.FindAction("Jump").Disable();
        inputActions.FindAction("Grab").Disable();
        inputActions.FindAction("Interact").Disable();

    }
    public void EnableMovementActions()
    {
        inputActions.FindAction("Move").Enable();
        inputActions.FindAction("Jump").Enable();
        inputActions.FindAction("Grab").Enable();
        inputActions.FindAction("Interact").Enable();
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                Attack.Invoke(true);
                break;
            case InputActionPhase.Canceled:
                Attack.Invoke(false);
                break;
        }
    }

    public void OnGrab(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                Grab.Invoke(true);
                break;
            case InputActionPhase.Canceled:
                Grab.Invoke(false);
                break;
        }
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
