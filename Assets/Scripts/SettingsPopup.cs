using UnityEngine;

public class SettingsPopup : MonoBehaviour
{
    [SerializeField] private AudioClip sound;
    [SerializeField] private GameObject exitPanel;
    [SerializeField] private UnityEngine.UI.Slider musicVolume;
    [SerializeField] private UnityEngine.UI.Slider soundVolume;

    private void Start()
    {
        if (Managers.Audio != null)
        {
            musicVolume.value = Managers.Audio.musicVolume;
            soundVolume.value = Managers.Audio.soundVolume;
        }
    }

    /// <summary>
    /// Выключение звука эффектов (в данном случае только UI).
    /// </summary>
    public void OnSoundToggle()
    {
        if (Managers.Audio == null) return;
        Managers.Audio.soundMute = !Managers.Audio.soundMute;
        Managers.Audio.PlaySound(sound);
    }

    /// <summary>
    /// Значение громкости с ползунка громкости звуков.
    /// </summary>
    /// <param name="volume"></param>
    public void OnSoundValue(float volume)
    {
        if (Managers.Audio == null) return;
        Managers.Audio.soundVolume = volume;
    }

    /// <summary>
    /// Селектор для выбора действия по кнопкам управления музыкой.
    /// </summary>
    /// <param name="selector"></param>
    public void OnPlayMusic(int selector)
    {
        if (Managers.Audio == null) return;
        
        Managers.Audio.PlaySound(sound);

        switch (selector)
        {
            case 1:
                Managers.Audio.NextMusic();
                break;
            case 2:
                Managers.Audio.PreviousMusic();
                break;
            default:
                Managers.Audio.StopMusic();
                break;
        }
    }

    /// <summary>
    /// Выключение звука музыки.
    /// </summary>
    public void OnMusicToggle()
    {
        if (Managers.Audio == null) return;
        Managers.Audio.musicMute = !Managers.Audio.musicMute;
    }

    /// <summary>
    /// Значение громкости с ползунка громкости музыки.
    /// </summary>
    /// <param name="volume"></param>
    public void OnMusicValue(float volume)
    {
        if (Managers.Audio == null) return;
        Managers.Audio.musicVolume = volume;
    }

    /// <summary>
    /// Активация панели с запросом на выход.
    /// </summary>
    public void OnExit()
    {
        exitPanel.SetActive(true);
    }

    /// <summary>
    /// Окно выхода из игры.
    /// </summary>
    /// <param name="choice"></param>
    public void OnUserClickYesNo(bool choice)
    {
        if (choice)
        {
            Application.Quit();
        }
        exitPanel.SetActive(false);
    }
}
