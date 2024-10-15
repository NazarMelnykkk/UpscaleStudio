using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum MenuState
{
    None = 0,
    Lose,
    Win,
    Menu
}

public class MenuController : MonoBehaviour
{

    [SerializeField] private BaseHealthController _healthController;
    [SerializeField] private FirstPersonController _playerController;

    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private GameObject _menuView;
    [SerializeField] private TextMeshProUGUI _menuText;

    [SerializeField] private string _loseMessage = "You Lose!";
    [SerializeField] private string _winMessage = "You Win!";

    [SerializeField] private string _fireDeathMessage = "You were burned!";
    [SerializeField] private string _enemyDeathMessage = "An enemy defeated you!";

    [SerializeField] private SceneConfig _mainMenuScene;

    private MenuState _state = MenuState.None;
    private bool _isOpened = false;
    private float _startTime;

    private void Start()
    {
        _startTime = Time.time;
        Close();
    }

    private void OnEnable()
    {
        References.Instance.InputController.OnMenuPerformedEvent += ToggleMenu;
        _healthController.OnDieEvent += HandleDeath;
    }

    private void OnDisable()
    {
        References.Instance.InputController.OnMenuPerformedEvent -= ToggleMenu;
        _healthController.OnDieEvent -= HandleDeath;
    }

    private void HandleDeath(DamageType damageType)
    {
        _state = MenuState.Lose;
        SetMenuState(MenuState.Lose);

        switch (damageType)
        {
            case DamageType.Fire:
                _menuText.text = _fireDeathMessage;
                break;
            case DamageType.Enemy:
                _menuText.text = _enemyDeathMessage;
                break;
            default:
                _menuText.text = _loseMessage;
                break;
        }
    }

    public void SetMenuState(MenuState state)
    {
        if (_isOpened && state != _state)
        {
            Open(state);
        }
        else if (!_isOpened)
        {
            Open(state);
        }
    }

    private void Open(MenuState state)
    {
        _playerController.cameraCanMove = false;
        _playerController.playerCanMove = false;

        _state = state;

        Cursor.lockState = CursorLockMode.Confined;

        switch (_state)
        {
            case MenuState.Lose:
                
                break;
            case MenuState.Win:
                float elapsedTime = Time.time - _startTime;
                _menuText.text = _winMessage + "\nTime: " + FormatTime(elapsedTime);
                break;
            case MenuState.Menu:
                _menuText.text = "";
                break;
        }

        _menuView.SetActive(true);
        _isOpened = true;
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60F);
        int seconds = Mathf.FloorToInt(time % 60F);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void ToggleMenu()
    {
        if (_isOpened)
        {
            Close();
        }
        else
        {
            Open(MenuState.Menu);
        }
    }

    private void Close()
    {
        _state = MenuState.None;
        _menuView.SetActive(false);

        _playerController.cameraCanMove = true;
        _playerController.playerCanMove = true;

        _isOpened = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
