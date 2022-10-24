using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ShopInteractionScript : MonoBehaviour
{
    bool _spriteOn;

    private void Start()
    {
        _spriteOn = false;
    }

    private void FixedUpdate()
    {
        // sprite is on so player is in proximity to open shop
        if (_spriteOn)
        {
            // shop interaction
            /* if some button is pressed, open shop (probably call a method in player that tells it
             * to disable movement, then when shop is closed enable movement again)
             */
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
}
