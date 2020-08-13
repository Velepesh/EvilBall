using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private WeaponView _template;
    [SerializeField] private GameObject _itemContainer;
    [SerializeField] private Inventory _shopInventory;

    private void Start()
    {
        for (int i = 0; i < _shopInventory.GetCountOfWeapon(); i++)
        {
            AddItem(_shopInventory.GetGun(i));
        }
    }

    private void AddItem(Gun gun)
    {
        var view = Instantiate(_template, _itemContainer.transform);
        view.SellButtonClick += OnSellButtonClick;
        view.Render(gun);
    }

    private void OnSellButtonClick(Gun gun, WeaponView view)
    {
        TrySellWeapon(gun, view);
    }

    private void TrySellWeapon(Gun gun, WeaponView view)
    {
        if (gun.Price <= _wallet.Money)
        {
            _wallet.BuyWeapon(gun);
            gun.Buy();
            view.SellButtonClick -= OnSellButtonClick;
        }
    }
}
