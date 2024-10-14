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
        InputActions.Character.Interaction.performed += InteractionPerformed;

        InputActions.Character.Menu.performed += MenuPerformed;

    }

    private void OnDisable()
    {
        InputActions.Character.Interaction.performed -= InteractionPerformed;

        InputActions.Character.Menu.performed -= MenuPerformed;
    }

    #region Interaction

    public Action OnInteractionPerformedEvent;

    private void InteractionPerformed(InputAction.CallbackContext context)
    {
        OnInteractionPerformedEvent?.Invoke();
    }
    #endregion

    #region Menu

    public Action OnMenuPerformedEvent;

    private void MenuPerformed(InputAction.CallbackContext context)
    {
        OnMenuPerformedEvent?.Invoke();
    }

    #endregion
}
