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

    bool _spriteOn;
    bool _shopOpen;

    int _playerCurrency;
    int _upgradeCost = 100;

    AudioSource _as;
    public AudioClip _menuClick;

    private void Start()
    {
        _spriteOn = false;
        _shopOpen = false;
        _shopCanvas.SetActive(false);

        _playerCurrency = PlayerPrefs.GetInt(ConstLabels.pref_player_currency);
        _weapon.ResetAmmo();
        UpdateShop();
    }

    

    private void Update()
    {
        // sprite is on so player is in proximity to open shop
        if (_spriteOn && Input.GetKeyDown(KeyCode.E))
        {
            // shop interaction
            /* if some button is pressed, open shop (probably call a method in player that tells it
             * to disable movement, then when shop is closed enable movement again)
             */
            if (_shopOpen)
            {
                _shopOpen = !_shopOpen;
                _shopCanvas.SetActive(false);
                ResumeGame();
            }
            else
            {
                _shopOpen = !_shopOpen;
                _shopCanvas.SetActive(true);
                PauseGame();
            }
        }
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
        if (!_spriteOn)
        {
            _spriteOn = true;
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_spriteOn)
        {
            _spriteOn = false;
            GetComponent<SpriteRenderer>().enabled = false;
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

        if (PlayerPrefs.GetInt(ConstLabels.pref_upgrade_melee) != 0)
        {
            _meleeCostText.text = "----";
            _meleeButton.GetComponent<Button>().interactable = false;
        }
        if (PlayerPrefs.GetInt(ConstLabels.pref_upgrade_gun) != 0)
        {
            _gunCostText.text = "----";
            _gunButton.GetComponent<Button>().interactable = false;
        }
        if (PlayerPrefs.GetInt(ConstLabels.pref_upgrade_ammo) != 1)
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
        if (_upgradeCost < _playerCurrency)
        {
            _as.PlayOneShot(_menuClick);
            _playerCurrency -= _upgradeCost;
            PlayerPrefs.SetInt(ConstLabels.pref_upgrade_melee, 1);
            PlayerPrefs.SetInt(ConstLabels.pref_player_currency, _playerCurrency);
            UpdateShop();
        }
    }

    public void UpgradeGun()
    {
        if (_upgradeCost < _playerCurrency)
        {
            _as.PlayOneShot(_menuClick);
            _playerCurrency -= _upgradeCost;
            PlayerPrefs.SetInt(ConstLabels.pref_upgrade_gun, 1);
            PlayerPrefs.SetInt(ConstLabels.pref_player_currency, _playerCurrency);
            UpdateShop();
        }
    }

    public void UpgradeAmmo()
    {
        if (_upgradeCost < _playerCurrency)
        {
            _as.PlayOneShot(_menuClick);
            _playerCurrency -= _upgradeCost;
            PlayerPrefs.SetInt(ConstLabels.pref_upgrade_ammo, 2);
            PlayerPrefs.SetInt(ConstLabels.pref_player_currency, _playerCurrency);
            UpdateShop();
            _weapon.ResetAmmo();
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
