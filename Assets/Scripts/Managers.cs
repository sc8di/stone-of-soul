using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(AudioManager))]
[RequireComponent(typeof(MissionManager))]
public class Managers : MonoBehaviour
{
    public static PlayerManager Player { get; private set; }
    public static AudioManager Audio { get; private set; }
    public static MissionManager Mission { get; private set; }

    private List<IGameManager> _startSequence;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        
        Player = GetComponent<PlayerManager>();
        Audio = GetComponent<AudioManager>();
        Mission = GetComponent<MissionManager>();
        
        _startSequence = new List<IGameManager>();
        _startSequence.Add(Player);
        _startSequence.Add(Audio);
        _startSequence.Add(Mission);

        StartCoroutine(StartupManagers());

        Audio.NextMusic();
    }

    /// <summary>
    /// Загружает все менеджеры.
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartupManagers()
    {
        NetworkService network = new NetworkService();
        
        foreach (IGameManager manager in _startSequence)
        {
            manager.Startup(network);
        }

        yield return null;

        int numModules = _startSequence.Count;
        int numReady = 0;

        while (numReady < numModules)
        {
            int lastReady = numReady;
            numReady = 0;

            foreach (IGameManager manager in _startSequence)
            {
                if (manager.status == ManagerStatus.Started) numReady++;
            }

            if (numReady > lastReady)
            {
                Debug.Log($"Progress: {numReady}/{numModules}");
                Messenger<int, int>.Broadcast(StartupEvent.MANAGERS_PROGRESS, numReady, numModules);
            }
            yield return null;
        }
        Debug.Log($"All managers started up");
        Messenger.Broadcast(StartupEvent.MANAGERS_STARTED);
    }
}
