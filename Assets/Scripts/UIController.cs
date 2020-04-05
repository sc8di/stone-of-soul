using System.Collections;
using TMPro;
using UnityEngine;
using Cursor = UnityEngine.Cursor;

public class UIController : MonoBehaviour
{
    [SerializeField] private SettingsPopup popup; // Окно настроек.
    [SerializeField] private TextMeshProUGUI levelEnding; // Текст для завершение уровня.
    [SerializeField] private GameObject[] lifes;
    [SerializeField] private TextMeshProUGUI score; 

    private void Awake()
    {
        Messenger.AddListener(GameEvent.HEALTH_UPDATED, OnHealthUpdated);
        Messenger.AddListener(GameEvent.LEVEL_COMPLETE, OnLevelComplete);
        Messenger.AddListener(GameEvent.LEVEL_FAILED, OnLevelFailed);
        Messenger.AddListener(GameEvent.GAME_COMPLETE, OnGameComplete);
        Messenger.AddListener(GameEvent.SCORE_UPDATED, OnScoreUpdated);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.HEALTH_UPDATED, OnHealthUpdated);
        Messenger.RemoveListener(GameEvent.LEVEL_COMPLETE, OnLevelComplete);
        Messenger.RemoveListener(GameEvent.LEVEL_FAILED, OnLevelFailed);
        Messenger.RemoveListener(GameEvent.GAME_COMPLETE, OnGameComplete);
        Messenger.RemoveListener(GameEvent.SCORE_UPDATED, OnScoreUpdated);
    }

    private void Start()
    {
        popup.gameObject.SetActive(false);

        OnScoreUpdated();

        OnHealthUpdated();
    }

    /// <summary>
    /// Вызов настроек. Активация курсора. Либо обратное действие.
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bool isShowingSettings = popup.gameObject.activeSelf;
            popup.gameObject.SetActive(!isShowingSettings);

            if (isShowingSettings)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }

    /// <summary>
    /// Изменение количества жизней.
    /// </summary>
    private void OnHealthUpdated()
    {
        if (Managers.Player == null) return;

        if (Managers.Player.health < Managers.Player.maxHealth)
        {
            lifes[Managers.Player.health].SetActive(false);
        }
    }

    /// <summary>
    /// Изменение очков.
    /// </summary>
    private void OnScoreUpdated()
    {
        if (Managers.Player == null) return;

        score.text = "Time to death\n" + Managers.Player.score;
        if (Managers.Player.score < 500) score.color = Color.yellow;
        if (Managers.Player.score < 250) score.color = Color.red;

        if (score.color != Color.cyan)
        {
            if (Managers.Player.score > 500) score.color = Color.cyan;
        }
    }

    private void OnLevelComplete()
    {
        StartCoroutine(CompleteLevel());
    }

    /// <summary>
    /// Завершение уровня.
    /// </summary>
    /// <returns></returns>
    private IEnumerator CompleteLevel()
    {
        levelEnding.gameObject.SetActive(true);
        levelEnding.text = "YOU DIED!";

        yield return new WaitForSeconds(2);

        levelEnding.text = "Just kidding, level complete. ;3";

        yield return new WaitForSeconds(2);

        Managers.Mission.GoToNext();
    }

    private void OnLevelFailed()
    {
        StartCoroutine(FailLevel());
    }

    /// <summary>
    /// Завершение уровня поражением.
    /// </summary>
    /// <returns></returns>
    private IEnumerator FailLevel()
    {
        levelEnding.gameObject.SetActive(true);
        levelEnding.text = "YOU DIED!";

        yield return new WaitForSeconds(2);

        Managers.Player.RespawnFull();
        Managers.Mission.RestartCurrentLevel();
    }
    
    /// <summary>
    /// Завершение игры.
    /// </summary>
    private void OnGameComplete()
    {
        Time.timeScale = 0f;
        levelEnding.gameObject.SetActive(true);
        levelEnding.text = @"Demo complete. ¯\_(ツ)_/¯";
    }
}