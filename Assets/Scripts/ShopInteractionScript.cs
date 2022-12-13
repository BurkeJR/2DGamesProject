using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer))]
public class ShopInteractionScript : MonoBehaviour
{
    public DayNightScript _dnScript;
    public WeaponParent _weapon;
    public ExitInteractionScript _eiScript;

    public GameObject _shopCanvas;

    public Text _meleeCostText;
    public Text _gunCostText;
    public Text _bulletCostText;
    public Text _speedCostText;

    public GameObject _meleeButton;
    public GameObject _gunButton;
    public GameObject _bulletButton;
    public GameObject _speedButton;

    public GameObject _eButton;

    SpriteRenderer _srender;

    bool _canOpenShop;
    bool _shopOpen;

    int _playerCurrency;
    int _upgradeCost = 100;

    AudioSource _as;
    public AudioClip _menuClick;

    private void Start()
    {
        _as = GetComponent<AudioSource>();
        _canOpenShop = false;
        _shopOpen = false;
        _shopCanvas.SetActive(false);
        _srender = GetComponent<SpriteRenderer>();
        _eButton.SetActive(false);

        _playerCurrency = PlayerPrefs.GetInt(ConstLabels.pref_player_currency);
        _weapon.ResetAmmo();
        UpdateShop();
    }

    

    private void Update()
    {
        // sprite is on so player is in proximity to open shop
        if (_canOpenShop && Input.GetKeyDown(KeyCode.E))
        {
            if (_shopOpen)
            {
                CloseShop();
            }
            else
            {
                _shopOpen = !_shopOpen;
                _shopCanvas.SetActive(true);
                PauseGame();
            }
        }
    }

    public void CloseShop()
    {
        _shopOpen = !_shopOpen;
        _shopCanvas.SetActive(false);
        ResumeGame();
    }

    private void FixedUpdate()
    {
        if (!_dnScript._daytime)
        {
            _eiScript.SwitchScenes();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_canOpenShop)
        {
            _canOpenShop = true;
            _srender.enabled = true;
            _eButton.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_canOpenShop)
        {
            _canOpenShop = false;
            _srender.enabled = false;
            _eButton.SetActive(false);
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0;
    }

    void ResumeGame()
    {
        Time.timeScale = 1;
    }

    void UpdateShop()
    {
        if (_playerCurrency < _upgradeCost)
        {
            _meleeButton.GetComponent<Button>().interactable = false;
            _gunButton.GetComponent<Button>().interactable = false;
            _bulletButton.GetComponent<Button>().interactable = false;
            _speedButton.GetComponent<Button>().interactable = false;
        }
        if (PlayerPrefs.GetInt(ConstLabels.pref_upgrade_melee) == 1)
        {
            _meleeCostText.text = "150";
        }
        if (PlayerPrefs.GetInt(ConstLabels.pref_upgrade_melee) == 2)
        {
            _meleeCostText.text = "----";
            _meleeButton.GetComponent<Button>().interactable = false;
        }
        if (PlayerPrefs.GetInt(ConstLabels.pref_player_gun_spread) != 0)
        {
            _gunCostText.text = "----";
            _gunButton.GetComponent<Button>().interactable = false;
        }
        if (PlayerPrefs.GetInt(ConstLabels.pref_upgrade_ammo) == 2)
        {
            _bulletCostText.text = "150";
        }
        if (PlayerPrefs.GetInt(ConstLabels.pref_upgrade_ammo) == 4)
        {
            _bulletCostText.text = "----";
            _bulletButton.GetComponent<Button>().interactable = false;
        }
        if (PlayerPrefs.GetInt(ConstLabels.pref_upgrade_speed) != 0)
        {
            _speedCostText.text = "----";
            _speedButton.GetComponent<Button>().interactable = false;
        }
    }

    public void UpgradeMelee()
    {
        if ((PlayerPrefs.GetInt(ConstLabels.pref_upgrade_melee) == 0 && _upgradeCost < _playerCurrency) ||
            ((PlayerPrefs.GetInt(ConstLabels.pref_upgrade_melee) == 1) && _upgradeCost + 50 < _playerCurrency))
        {
            _as.PlayOneShot(_menuClick);
            
            if (PlayerPrefs.GetInt(ConstLabels.pref_upgrade_melee) == 0)
            {
                _playerCurrency -= _upgradeCost;
                PlayerPrefs.SetInt(ConstLabels.pref_upgrade_melee, 1);
                PlayerPrefs.SetInt(ConstLabels.pref_player_currency, _playerCurrency);
                UpdateShop(); 
            }
            else if (PlayerPrefs.GetInt(ConstLabels.pref_upgrade_melee) == 1)
            {
                _playerCurrency -= _upgradeCost + 50;
                PlayerPrefs.SetInt(ConstLabels.pref_upgrade_melee, 2);
                PlayerPrefs.SetInt(ConstLabels.pref_player_currency, _playerCurrency);
                UpdateShop();
            }
        }
    }

    public void UpgradeGun()
    {
        if (_upgradeCost < _playerCurrency)
        {
            _as.PlayOneShot(_menuClick);
            _playerCurrency -= 150;
            PlayerPrefs.SetInt(ConstLabels.pref_player_gun_spread, 1);
            PlayerPrefs.SetInt(ConstLabels.pref_player_currency, _playerCurrency);
            UpdateShop();
        }
    }

    public void UpgradeAmmo()
    {
        if ((PlayerPrefs.GetInt(ConstLabels.pref_upgrade_ammo) == 1 && _upgradeCost < _playerCurrency) || 
            ((PlayerPrefs.GetInt(ConstLabels.pref_upgrade_ammo) == 2) && _upgradeCost+50 < _playerCurrency))
        {
            _as.PlayOneShot(_menuClick);
            if(PlayerPrefs.GetInt(ConstLabels.pref_upgrade_ammo) == 1)
            {
                _playerCurrency -= _upgradeCost;
                PlayerPrefs.SetInt(ConstLabels.pref_upgrade_ammo, 2);
                PlayerPrefs.SetInt(ConstLabels.pref_player_currency, _playerCurrency);
                UpdateShop();
                _weapon.ResetAmmo();
            }
            else if (PlayerPrefs.GetInt(ConstLabels.pref_upgrade_ammo) == 2)
            {
                _playerCurrency -= _upgradeCost + 50;
                PlayerPrefs.SetInt(ConstLabels.pref_upgrade_ammo, 4);
                PlayerPrefs.SetInt(ConstLabels.pref_player_currency, _playerCurrency);
                UpdateShop();
                _weapon.ResetAmmo();
            }
            
        }
    }

    public void UpgradeSpeed()
    {
        if (_upgradeCost < _playerCurrency)
        {
            _as.PlayOneShot(_menuClick);
            _playerCurrency -= _upgradeCost;
            PlayerPrefs.SetInt(ConstLabels.pref_upgrade_speed, 1);
            PlayerPrefs.SetInt(ConstLabels.pref_player_currency, _playerCurrency);
            UpdateShop();
        }
    }
}
