using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponShop : MonoBehaviour
{
    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private Player _player;
    [SerializeField] private WeaponView _weaponTemplate;
    [SerializeField] private GameObject _weaponContainer;
    [SerializeField] private StartMenu _startMenu;

    private List<WeaponView> _templates = new List<WeaponView>();
    public event UnityAction<Weapon> WeaponChoosed;

    private void Start()
    {
        for(int i = 0; i < _weapons.Count; i++)
        {
            AddItem(_weapons[i]);
        }
    }

    private void OnEnable()
    {
        _startMenu.GameStarted += StartGame;
    }

    private void OnDisable()
    {
        _startMenu.GameStarted -= StartGame;
    }

    private void OnApplicationQuit()
    {
        foreach(WeaponView view in _templates)
        {
            view.SellButtonClick -= OnSellButtonClick;
        }
    }

    private void AddItem(Weapon weapon)
    {
        var template = Instantiate(_weaponTemplate, _weaponContainer.transform);
        _templates.Add(template);
        template.SellButtonClick += OnSellButtonClick;
        template.Render(weapon);
    }

    private void OnSellButtonClick(Weapon weapon, WeaponView weaponView)
    {
        if(weapon.IsBuyed != true)
        {
            TrySellWeapon(weapon, weaponView);
        }
        else
        {
            SellUpgrades(weapon);
        }
    }

    private void TrySellWeapon(Weapon weapon, WeaponView weaponView)
    {
        if(_player.Money >= weapon.Price)
        {
            _player.BuyWeapon(weapon);
            weapon.Buy();
            weaponView.Sell(weapon);
        }
    }

    private void SellUpgrades(Weapon weapon)
    {
        WeaponChoosed?.Invoke(weapon);
    }

    private void StartGame()
    {
        SetStartWeaponsValues();
        _player.StartGame();

        for (int i = 0; i < _templates.Count; i++)
        {
            _templates[i].Render(_weapons[i]);
        }
    }

    private void SetStartWeaponsValues()
    {
        for(int i = 0; i < _weapons.Count; i++)
        {
            _weapons[i].SetStartValue();
        }
    }
}
