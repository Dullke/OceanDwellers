using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine;

[CreateAssetMenu(fileName = "Input Reader", menuName = "Data/Input Reader")]
public class SOInputReader : ScriptableObject, GameInput.IPlayerActions, GameInput.IUIActions
{
    private GameInput gameInput;

    public event UnityAction<Vector2> onMove;
    public event UnityAction<int> shootCannons;

    #region Initialization

    private void OnEnable()
    {
        if (gameInput == null)
        {
            gameInput = new GameInput();
            gameInput.Player.SetCallbacks(this);
            gameInput.UI.SetCallbacks(this);
        }

        EnableGameplayInput();
    }

    public void EnableGameplayInput()
    {
        gameInput.UI.Disable();
        gameInput.Player.Enable();
    }
    public void DisableAllInput()
    {
        gameInput.Player.Disable();
        gameInput.UI.Disable();
    }
    public void EnableUIInput()
    {
        gameInput.Player.Disable();
        gameInput.UI.Enable();
    }

    #endregion


    public void OnFrontFire(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            shootCannons?.Invoke(0);
    }

    public void OnRightFire(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            shootCannons?.Invoke(1);
    }

    public void OnLeftFire(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            shootCannons?.Invoke(2);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        onMove?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnRightAttack(InputAction.CallbackContext context)
    {
    }



    public void OnCancel(InputAction.CallbackContext context)
    {
    }

    public void OnClick(InputAction.CallbackContext context)
    {
    }

    public void OnLook(InputAction.CallbackContext context)
    {
    }

    public void OnMiddleClick(InputAction.CallbackContext context)
    {
    }


    public void OnRightClick(InputAction.CallbackContext context)
    {
    }

    public void OnScrollWheel(InputAction.CallbackContext context)
    {
    }

    public void OnSubmit(InputAction.CallbackContext context)
    {
    }


    private void OnDestroy()
    {
        DisableAllInput();
    }

}
