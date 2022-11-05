using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameRestarter : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private Button _restartButton;

    private void OnEnable()
    {
        _player.PlayerDying += OnPlayerDying;
        _restartButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _player.PlayerDying -= OnPlayerDying;
        _restartButton.onClick.RemoveListener(OnButtonClick);
    }

    private void OnPlayerDying()
    {
        _menuPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    private void OnButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}