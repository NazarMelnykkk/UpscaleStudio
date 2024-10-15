using TMPro;
using UnityEngine;

public class KeyCounterController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _keyCounterText;
    [SerializeField] private int _totalKeys = 3;  

    private int _keysCollected = 0;


    private void OnEnable()
    {
        KeyController.OnKeyCollected += UpdateKeyCounter;
        UpdateKeyCounterText();
    }

    private void OnDisable()
    {
        KeyController.OnKeyCollected -= UpdateKeyCounter;
    }

    private void UpdateKeyCounter()
    {
        _keysCollected++;
        UpdateKeyCounterText();
    }

    private void UpdateKeyCounterText()
    {
        _keyCounterText.text = $"Key {_keysCollected}/{_totalKeys}";
    }
}
