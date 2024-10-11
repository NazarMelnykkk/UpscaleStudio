using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    public CharacterInputActions InputActions;

    private void Awake()
    {
        InputActions = new CharacterInputActions();
        InputActions.Enable();
    }

    private void OnEnable()
    {
        InputActions.Character.MoveDirection.performed += MovePerformed;
        InputActions.Character.MoveDirection.canceled += MoveCanceled;

        InputActions.Character.Fire1.performed += Fire1Performed;
        InputActions.Character.Fire1.canceled += Fire1Canceled;

        InputActions.Character.Fire2.performed += Fire2Performed;
        InputActions.Character.Fire2.canceled += Fire2Canceled;

        InputActions.Character.Scroll.performed += OnScrollPerformed;

        InputActions.Character.Interaction.performed += InteractionPerformed;

        InputActions.Character.Menu.performed += MenuPerformed;

        InputActions.Character.Jump.performed += JumpPerformed;

    }

    private void OnDisable()
    {
        InputActions.Character.MoveDirection.performed -= MovePerformed;
        InputActions.Character.MoveDirection.canceled -= MoveCanceled;

        InputActions.Character.Fire1.performed -= Fire1Performed;
        InputActions.Character.Fire1.canceled -= Fire1Canceled;

        InputActions.Character.Fire2.performed -= Fire2Performed;
        InputActions.Character.Fire2.canceled -= Fire2Canceled;

        InputActions.Character.Scroll.performed -= OnScrollPerformed;

        InputActions.Character.Interaction.performed -= InteractionPerformed;

        InputActions.Character.Menu.performed -= MenuPerformed;

        InputActions.Character.Jump.performed -= JumpPerformed;
    }

    public Vector2 MousePosition() => Input.mousePosition;
    public Vector2 MouseInWorldPosition => Camera.main.ScreenToWorldPoint(MousePosition());

    #region Move

    public Action<Vector2> OnMovePerformedEvent;
    private void MovePerformed(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();
        OnMovePerformedEvent?.Invoke(direction);
    }

    public Action<Vector2> OnMoveCanceledEvent;
    private void MoveCanceled(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();
        OnMoveCanceledEvent?.Invoke(direction);
    }

    #endregion

    #region Fire1
    public Action OnFire1PerformedEvent;

    private void Fire1Performed(InputAction.CallbackContext context)
    {
        OnFire1PerformedEvent?.Invoke();
    }

    public Action OnFire1CanceledEvent;

    private void Fire1Canceled(InputAction.CallbackContext context)
    {
        OnFire1CanceledEvent?.Invoke();
    }
    #endregion

    #region Fire2
    public Action OnFire2PerformedEvent;

    private void Fire2Performed(InputAction.CallbackContext context)
    {
        OnFire2PerformedEvent?.Invoke();
    }

    public Action OnFire2CanceledEvent;

    private void Fire2Canceled(InputAction.CallbackContext context)
    {
        OnFire2CanceledEvent?.Invoke();
    }
    #endregion

    #region Interaction

    public Action OnInteractionPerformedEvent;

    private void InteractionPerformed(InputAction.CallbackContext context)
    {
        OnInteractionPerformedEvent?.Invoke();
    }
    #endregion

    #region Scroll

    public event Action<float> OnScrollPerformedIvent;
    private void OnScrollPerformed(InputAction.CallbackContext context)
    {
        float scrollDelta = context.ReadValue<Vector2>().y;

        OnScrollPerformedIvent?.Invoke(scrollDelta);
    }

    #endregion

    #region Menu

    public Action OnMenuPerformedEvent;

    private void MenuPerformed(InputAction.CallbackContext context)
    {
        OnMenuPerformedEvent?.Invoke();
    }

    #endregion

    #region Action

    public Action OnJumpPerformedEvent;

    private void JumpPerformed(InputAction.CallbackContext context)
    {
        OnJumpPerformedEvent?.Invoke();
    }

    #endregion

    #region Jump

    #endregion

    #region Jump

    #endregion

}
