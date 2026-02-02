using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public TextMeshProUGUI GoldText;
    public TextMeshProUGUI WaveText;
    public TextMeshProUGUI LivesText;

    public TextMeshProUGUI MessageText;
    public Button StartWaveButton;

    public Button BuildGunnerButton;
    public Button BuildCannonButton;
    public Button BuildFrostButton;

    private void Awake()
    {
        if (Instance != null)
            return;

        Instance = this;
    }

    private void Start()
    {
        RefreshUI();

        if (Instance.StartWaveButton != null)
            Instance.StartWaveButton.onClick.AddListener(() => WaveManager.Instance.StartNextWave());

        if (Instance.BuildGunnerButton != null)
            Instance.BuildGunnerButton.onClick.AddListener(() => TowerManager.Instance.BuildTower(TowerType.Gunner));
        if (Instance.BuildCannonButton != null)
            Instance.BuildCannonButton.onClick.AddListener(() => TowerManager.Instance.BuildTower(TowerType.Cannon));
        if (Instance.BuildFrostButton != null)
            Instance.BuildFrostButton.onClick.AddListener(() => TowerManager.Instance.BuildTower(TowerType.Frost));
    }

    public void RefreshUI()
    {
        if (GoldText != null)
            GoldText.text = $"Gold: {UglyDTGameManager.Instance.Gold}";
        if (WaveText != null)
            WaveText.text = "Wave: " + WaveManager.Instance.Wave
                + (WaveManager.Instance.WaveInProgress ? " (Running)" : "");
        if (LivesText != null)
            LivesText.text = $"Lives: {UglyDTGameManager.Instance.Lives}";
    }
}