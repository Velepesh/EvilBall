using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.IO;

public class Wallet : MonoBehaviour
{
    [SerializeField] private WalletData _walletData;

    public int Money { get; private set; }

    public event UnityAction<int> MoneyChanged;
    public event UnityAction<Gun> WeaponBought;

    private void Start()
    {
        AddMoney(_walletData.WalletBalance);
    }

    private void OnEnable()
    {
        _walletData.LoadState();
    }

    private void OnDisable()
    {
        _walletData.SaveState();
    }

    public void AddMoney(int money)
    {
        Money += money;
        _walletData.WalletBalance = Money;

        MoneyChanged?.Invoke(Money);
    }

    public void BuyWeapon(Gun gun)
    {
        Money -= gun.Price;
        _walletData.WalletBalance = Money;

        MoneyChanged?.Invoke(Money);
        WeaponBought?.Invoke(gun);
    }

    [System.Serializable]
    public class WalletData
    {
        public int WalletBalance = 0;

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
            return Application.persistentDataPath + $"/WalletData.so";
        }
    }
}