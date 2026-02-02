using UnityEngine;
using VContainer;

public class WaveManager : MonoBehaviour, IWaveManager
{
    [Inject]
    private IEnemiesManager _enemiesManager;

    [Inject]
    private IUIManager _uiManager;

    public static WaveManager Instance;

    public int Wave;
    public bool WaveInProgress;

    private void Awake()
    {
        if (Instance != null)
            return;

        Instance = this;
    }

    public void StartNextWave()
    {
        if (WaveInProgress)
            return;

        Wave++;
        WaveInProgress = true;
        _uiManager.RefreshUI();

        CancelInvoke(nameof(_enemiesManager.SpawnFromSequence));
        _enemiesManager.ResetWave(Wave);
        InvokeRepeating(nameof(_enemiesManager.SpawnFromSequence), 0.25f, 0.6f);
    }
}