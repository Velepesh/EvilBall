using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class WeaponView : MonoBehaviour
{
    [SerializeField] private TMP_Text _label;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _sellButton;

    private Gun _gun;

    public event UnityAction<Gun, WeaponView> SellButtonClick;

    private void OnEnable()
    {
        _sellButton.onClick.AddListener(OnButtonClick);
        _sellButton.onClick.AddListener(TryLockItem);
    }

    private void OnDisable()
    {
        _sellButton.onClick.RemoveListener(OnButtonClick);
        _sellButton.onClick.RemoveListener(TryLockItem);
    }

    private void TryLockItem()
    {
        if (_gun.IsBuyed)
            _sellButton.interactable = false;
    }

    public void Render(Gun gun)
    {
        _gun = gun;
        _label.text = gun.Label;
        _price.text = gun.Price.ToString();
        _icon.sprite = gun.Icon;

        TryLockItem();
    }

    private void OnButtonClick()
    {
        SellButtonClick?.Invoke(_gun, this);
    }
}