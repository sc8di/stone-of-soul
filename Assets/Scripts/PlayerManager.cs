﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }

    public Vector3 lastSavePosition  { get; private set; }

    public int health { get; private set; }
    public int maxHealth { get; private set; }
    public int score { get; private set; }

    public void Startup(NetworkService network)
    {
        Debug.Log($"Player manager starting...");

        score = 550;

        UpdateData(3, 3);

        status = ManagerStatus.Started;
    }

    /// <summary>
    /// Обновление данных по здоровью.
    /// </summary>
    /// <param name="health"></param>
    /// <param name="maxHealth"></param>
    public void UpdateData(int health, int maxHealth)
    {
        this.health = health;
        this.maxHealth = maxHealth;
    }

    /// <summary>
    /// Изменение данных по очкам.
    /// </summary>
    /// <param name="value"></param>
    public void ChangeScore(int value)
    {
        score += value;
        if (score < 0) score = 0;
        Messenger.Broadcast(GameEvent.SCORE_UPDATED);
        if (score == 0) Messenger.Broadcast(GameEvent.LEVEL_FAILED); 
    }

    /// <summary>
    /// Изменение количества жизней.
    /// </summary>
    /// <param name="value"></param>
    public void ChangeHealth(int value)
    {
        health += value;
        
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        else if (health < 0)
        {
            health = 0;
        }
        
        if (health == 0) Messenger.Broadcast(GameEvent.LEVEL_FAILED);
        else StartCoroutine(Respawn());
        
        Messenger.Broadcast(GameEvent.HEALTH_UPDATED);
    }

    /// <summary>
    /// Апдейт последней позиции при активации чекпоинта.
    /// </summary>
    /// <param name="checkpoint"></param>
    public void UpdateLastAutosavePosition(Vector3 checkpoint)
    {
        lastSavePosition = checkpoint;
    }
    
    /// <summary>
    /// Респаун игрока с возвращением на чекпоинт.
    /// </summary>
    /// <returns></returns>
    private IEnumerator Respawn()
    {
        // Player disolve.
        Debug.Log("Player disolving;");
        
        yield return new WaitForSeconds(2);

        Messenger.Broadcast(GameEvent.RETURN_TO_CHECKPOINT);
    }

    /// <summary>
    /// Респаун в начало уровня.
    /// </summary>
    public void RespawnFull()
    {
        UpdateData(maxHealth, maxHealth);
    }
}
