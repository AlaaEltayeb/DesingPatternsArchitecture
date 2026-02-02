using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class UIManager : MonoBehaviour, IUIManager
{
    [Inject]
    private IGameManager _gameManager;

    public TextMeshProUGUI GoldText;
    public TextMeshProUGUI WaveText;
    public TextMeshProUGUI LivesText;

    [SerializeField]
    private TextMeshProUGUI _messageText;
    public Button StartWaveButton;

    public Button BuildGunnerButton;
    public Button BuildCannonButton;
    public Button BuildFrostButton;

    private void Start()
    {
        RefreshUI();

        if (StartWaveButton != null)
            StartWaveButton.onClick.AddListener(() => WaveManager.Instance.StartNextWave());

        if (BuildGunnerButton != null)
            BuildGunnerButton.onClick.AddListener(() => TowerManager.Instance.BuildTower(TowerType.Gunner));
        if (BuildCannonButton != null)
            BuildCannonButton.onClick.AddListener(() => TowerManager.Instance.BuildTower(TowerType.Cannon));
        if (BuildFrostButton != null)
            BuildFrostButton.onClick.AddListener(() => TowerManager.Instance.BuildTower(TowerType.Frost));
    }

    public void UpdateInGameMessage(string newMessage)
    {
        _messageText.text = newMessage;
    }

    public void RefreshUI()
    {
        if (GoldText != null)
            GoldText.text = $"Gold: {_gameManager.Gold}";
        if (WaveText != null)
            WaveText.text = "Wave: " + WaveManager.Instance.Wave
                + (WaveManager.Instance.WaveInProgress ? " (Running)" : "");
        if (LivesText != null)
            LivesText.text = $"Lives: {_gameManager.Lives}";
    }
}