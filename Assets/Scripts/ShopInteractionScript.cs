using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer))]
public class ShopInteractionScript : MonoBehaviour
{
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

    private void Start()
    {
        _spriteOn = false;
        _shopOpen = false;
        _shopCanvas.SetActive(false);

        _playerCurrency = PlayerPrefs.GetInt(ConstLabels.pref_player_currency);
        PlayerPrefs.SetInt(ConstLabels.pref_player_ammo, 6);
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
        if (PlayerPrefs.GetInt(ConstLabels.pref_upgrade_ammo) != 0)
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
            _playerCurrency -= _upgradeCost;
            PlayerPrefs.SetInt(ConstLabels.pref_upgrade_ammo, 2);
            PlayerPrefs.SetInt(ConstLabels.pref_player_currency, _playerCurrency);
            UpdateShop();
        }
    }

    public void UpgradeSpeed()
    {
        if (_upgradeCost < _playerCurrency)
        {
            _playerCurrency -= _upgradeCost;
            PlayerPrefs.SetInt(ConstLabels.pref_upgrade_speed, 1);
            PlayerPrefs.SetInt(ConstLabels.pref_player_currency, _playerCurrency);
            UpdateShop();
        }
    }
}
