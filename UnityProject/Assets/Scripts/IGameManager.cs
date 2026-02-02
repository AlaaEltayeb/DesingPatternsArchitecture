using UnityEngine;

public interface IGameManager
{
    int Gold { get; }
    int Lives { get; }
    Transform BulletParent { get; }

    void UpdateGold(int newGold);
    void UpdateLives(int newLives);
}