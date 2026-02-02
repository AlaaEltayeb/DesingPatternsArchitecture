using UnityEngine;
using VContainer;

public class GameManager : MonoBehaviour, IGameManager
{
    [Inject]
    private IEnemiesManager _enemiesManager;

    [Inject]
    private IUIManager _uiManager;

    [field: SerializeField]
    public Transform BulletParent { get; set; }

    public int Gold { get; private set; } = 200;

    public int Lives { get; private set; } = 20;

    public void UpdateGold(int newGold)
    {
        Gold += newGold;
    }

    public void UpdateLives(int newLives)
    {
        Lives += newLives;
    }

    private void Update()
    {
        if (Lives <= 0)
        {
            Lives = 0;
            _uiManager.RefreshUI();
            if (WaveManager.Instance.WaveInProgress)
            {
                WaveManager.Instance.WaveInProgress = false;
                _uiManager.UpdateInGameMessage("You Lose, (Reload Scene Manually)");
                Debug.Log("You Lose, (Reload Scene Manually)");
            }
        }

        if (WaveManager.Instance.WaveInProgress)
        {
            var anyAlive = false;
            for (var i = 0; i < _enemiesManager.Enemies.Count; i++)
            {
                var e = _enemiesManager.Enemies[i];
                if (e != null && _enemiesManager.Enemies[i].Hp > 0)
                {
                    anyAlive = true;
                    break;
                }
            }

            if (!anyAlive)
            {
                WaveManager.Instance.WaveInProgress = false;
                _uiManager.UpdateInGameMessage("Wave Completed!");
                Debug.Log("Wave Completed!");
                _uiManager.RefreshUI();
            }
        }
    }
}