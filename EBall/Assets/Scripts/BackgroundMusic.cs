using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[RequireComponent(typeof(AudioSource))]
public class BackgroundMusic : MonoBehaviour
{
    [SerializeField] private BackgroundMusicData _backgroundMusicData;

    private AudioSource _audioSource;

   private void Awake()
   {
        if (File.Exists(_backgroundMusicData.GetFilePath()))
        {
            _backgroundMusicData.LoadState();
        }

        if(_backgroundMusicData.IsExist)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
   }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.Play();

        _backgroundMusicData.SaveState(true);
    }

    private void OnDisable()
    {
        _backgroundMusicData.SaveState(false);
    }

    [System.Serializable]
    public class BackgroundMusicData
    {
        public bool IsExist;

        public void LoadState()
        {
            var json = File.ReadAllText(GetFilePath());

            JsonUtility.FromJsonOverwrite(json, this);
        }

        public void SaveState(bool isExist)
        {
            IsExist = isExist;

            var json = JsonUtility.ToJson(this);

            File.WriteAllText(GetFilePath(), json);
        }

        public string GetFilePath()
        {
            return Application.persistentDataPath + $"/BackgroundMusicData.so";
        }
    }
}
