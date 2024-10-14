using UnityEngine;

public class ActionController : MonoBehaviour
{

    [SerializeField] private LayerMask _interactableLayer;
    [SerializeField] private float _rayDistance = 4f;
    [SerializeField] private GameObject _interactionPrompt;

    private IInteractable _currentInteractable;

    private void OnEnable()
    {
        References.Instance.InputController.OnInteractionPerformedEvent += OnInteractButtonPressed;
    }


    private void OnDisable()
    {
        References.Instance.InputController.OnInteractionPerformedEvent -= OnInteractButtonPressed;
    }

    private void FixedUpdate()
    {
        CheckForInteractable();
    }

    private void CheckForInteractable()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        Debug.DrawRay(transform.position, transform.forward * _rayDistance, Color.green);


        if (Physics.Raycast(ray, out hit, _rayDistance, _interactableLayer))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                _currentInteractable = interactable;
                ShowInteractionPrompt(true);
                return;
            }
        }

        _currentInteractable = null;
        ShowInteractionPrompt(false);
    }

    private void ShowInteractionPrompt(bool isVisible)
    {
        if (_interactionPrompt != null)
        {
            _interactionPrompt.SetActive(isVisible);
        }
    }

    private void OnInteractButtonPressed()
    {
        if (_currentInteractable != null)
        {
            _currentInteractable.Interact();
        }
    }
}
