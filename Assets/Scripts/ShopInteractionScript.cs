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

    bool _spriteOn;
    bool _shopOpen;

    private void Start()
    {
        _spriteOn = false;
        _shopOpen = false;
        _shopCanvas.SetActive(false);
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
}
