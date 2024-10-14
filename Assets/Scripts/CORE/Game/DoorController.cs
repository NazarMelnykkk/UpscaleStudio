using System.Collections;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private SoundConfig _soundConfig;
    [SerializeField] private ObjectSoundController _objectSoundController;

    [Header("Open config")]
    [SerializeField] private float _moveDistance = 2.15f; 
    [SerializeField] private float _moveDuration = 3.2f;

    [ContextMenu("Open")]
    public void OpenGrate()
    {
        _objectSoundController.PlaySound(_soundConfig);
        StartCoroutine(RaiseGrate());
    }

    private IEnumerator RaiseGrate()
    {
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition + Vector3.up * _moveDistance;
        float elapsedTime = 0;

        while (elapsedTime < _moveDuration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / _moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
    }
}
