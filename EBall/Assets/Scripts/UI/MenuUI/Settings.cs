using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _resolutionDropdown;
    [SerializeField] private AudioMixerGroup _audioMixerGroup;
    [SerializeField] private Toggle _toggle;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _effectsSlider;
    [SerializeField] private SettingsData _settingsData;

    private Resolution[] _resolutions;

    private void Start()
    {
        SetMusicVolume(_settingsData.MusicVolume);
        SetEffectsVolume(_settingsData.EffectsVolume);
        SetScreenState(_settingsData.IsFullscreen);
        SetResolution();
    }

    private void OnEnable()
    {
        _settingsData.LoadState();
    }

    private void OnDisable()
    {
        _settingsData.SaveState();
    }

    private void SetResolution()
    {
        _resolutions = Screen.resolutions;
        _resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < _resolutions.Length; i++)
        {
            string option = _resolutions[i].width + " x " + _resolutions[i].height;
            options.Add(option);

            if (_resolutions[i].width == Screen.currentResolution.width &&
                _resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        if (_settingsData.ResolutionIndex < 0)
            _settingsData.ResolutionIndex = currentResolutionIndex;

        _resolutionDropdown.AddOptions(options);
        _resolutionDropdown.value = _settingsData.ResolutionIndex;
        _resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        _settingsData.ResolutionIndex = resolutionIndex;

        Resolution resolution = _resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetMusicVolume(float volume)
    {
        _settingsData.MusicVolume = volume;

        _musicSlider.value = volume;
        _audioMixerGroup.audioMixer.SetFloat("Music", volume);
    }

    public void SetEffectsVolume(float volume)
    {
        _settingsData.EffectsVolume = volume;

        _effectsSlider.value = volume;
        _audioMixerGroup.audioMixer.SetFloat("Effects", volume);
    }

    public void SetScreenState(bool isFullscreen)
    {
        _settingsData.IsFullscreen = isFullscreen;

        Screen.fullScreen = isFullscreen;
        _toggle.isOn = isFullscreen;
    }

    [System.Serializable]
    public class SettingsData
    {
        public int ResolutionIndex = int.MinValue;
        public float MusicVolume = 0f;
        public float EffectsVolume = 0f;
        public bool IsFullscreen = true;

        public void LoadState()
        {
            var json = File.ReadAllText(GetFilePath());

            JsonUtility.FromJsonOverwrite(json, this);
        }

        public void SaveState()
        {
            var json = JsonUtility.ToJson(this);

            File.WriteAllText(GetFilePath(), json);
        }

        private string GetFilePath()
        {
            return Application.persistentDataPath + $"/SettingsData.so";
        }
    }
}

