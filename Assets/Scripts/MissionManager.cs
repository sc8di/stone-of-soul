using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }
    
    public int curLevel { get; private set; }
    public int maxLevel { get; private set; }

    private NetworkService _network;

    public void Startup(NetworkService service)
    {
        Debug.Log("Mission manager starting...");

        _network = service;

        UpdateData(0, 2);

        status = ManagerStatus.Started;
    }

    /// <summary>
    /// Обновление данных по уровням.
    /// </summary>
    /// <param name="curLevel"></param>
    /// <param name="maxLevel"></param>
    public void UpdateData(int curLevel, int maxLevel)
    {
        this.curLevel = curLevel;
        this.maxLevel = maxLevel;
    }
    
    /// <summary>
    /// Следующий уровень.
    /// </summary>
    public void GoToNext()
    {
        if (curLevel < maxLevel)
        {
            curLevel++;
            string name = $"Level{curLevel}";
            Debug.Log($"Loading {name}.");
            SceneManager.LoadScene(name);
        }
        else
        {
            Debug.Log("Last level.");
            Messenger.Broadcast(GameEvent.GAME_COMPLETE);
        }
    }

    /// <summary>
    /// Завершение уровня.
    /// </summary>
    public void ReachObjective()
    {
        Messenger.Broadcast(GameEvent.LEVEL_COMPLETE);
    }

    /// <summary>
    /// Рестарт текущего уровня.
    /// </summary>
    public void RestartCurrentLevel()
    {
        string name = $"Level{curLevel}";
        Debug.Log($"Loading {name}.");
        SceneManager.LoadScene(name);
    }
}
