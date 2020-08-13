using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[CreateAssetMenu(fileName = "Gun", menuName = "Gun/Gun", order = 51)]
public class Gun : ScriptableObject
{
    [SerializeField] private GameObject _weapon;
    [SerializeField] private Sprite _icon;
    [SerializeField] private GunData _gunData;

    public GameObject Weapon => _weapon;
    public Sprite Icon => _icon;
    public string Label => _gunData.Label;
    public int Price => _gunData.Price;
    public bool IsBuyed => _gunData.IsBuyed;

    private void OnEnable()
    {
        _gunData.LoadState();
    }

    private void OnDisable()
    {
        _gunData.SaveState();
    }

    public void Buy()
    {
        _gunData.IsBuyed = true;
    }

    [System.Serializable]
    public class GunData
    {
        public string Label;
        public int Price;
        public bool IsBuyed = false;

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
            return Application.persistentDataPath + $"/{Label}.so";
        }
    }
}