using UnityEngine;

public class GameManager : MonoBehaviour, IGameManager
{
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
            UIManager.Instance.RefreshUI();
            if (WaveManager.Instance.WaveInProgress)
            {
                WaveManager.Instance.WaveInProgress = false;
                UIManager.Instance.MessageText.text = "You Lose, (Reload Scene Manually)";
                Debug.Log("You Lose, (Reload Scene Manually)");
            }
        }

        if (WaveManager.Instance.WaveInProgress)
        {
            var anyAlive = false;
            for (var i = 0; i < EnemyManager.Instance.Enemies.Count; i++)
            {
                var e = EnemyManager.Instance.Enemies[i];
                if (e != null && EnemyManager.Instance.Enemies[i].Hp > 0)
                {
                    anyAlive = true;
                    break;
                }
            }

            if (!anyAlive)
            {
                WaveManager.Instance.WaveInProgress = false;
                UIManager.Instance.MessageText.text = "Wave Completed!";
                Debug.Log("Wave Completed!");
                UIManager.Instance.RefreshUI();
            }
        }
    }
}