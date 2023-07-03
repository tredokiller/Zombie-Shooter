using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Weapons.Scripts;

public class NewMenuManager : MonoBehaviour
{
    private WeaponSwitcher _weaponSwitcher;

    private void Awake()
    {
        _weaponSwitcher = GetComponent<WeaponSwitcher>();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(Scenes.Scenes.Playground.ToString());
    }

    public void SwitchNextWeapon()
    {
        
    }

    public void SwitchPrevWeapon()
    {
    }
}