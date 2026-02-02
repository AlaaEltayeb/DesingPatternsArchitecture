using UnityEngine;

public class WaveManager : MonoBehaviour
{
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
        UIManager.Instance.RefreshUI();

        var seq = EnemyManager.Instance.Waves[Mathf.Min(Wave - 1, EnemyManager.Instance.Waves.Length - 1)];

        if (Wave > EnemyManager.Instance.Waves.Length)
            seq = "RRTTRFRRFT";

        CancelInvoke(nameof(EnemyManager.Instance.SpawnFromSequence));
        EnemyManager.Instance._spawnSeq = seq;
        EnemyManager.Instance._spawnIndex = 0;
        InvokeRepeating(nameof(EnemyManager.Instance.SpawnFromSequence), 0.25f, 0.6f);
    }
}