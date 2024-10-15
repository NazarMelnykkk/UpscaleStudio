using UnityEngine;

public class LevelFinishController : MonoBehaviour
{
    [SerializeField] private MenuController _menuController;
    [SerializeField] private LayerMask _targetLayer;

    private void OnTriggerEnter(Collider other)
    {
        if ((_targetLayer.value & (1 << other.gameObject.layer)) > 0)
        {
            _menuController.SetMenuState(MenuState.Win);
        }
    }
}
