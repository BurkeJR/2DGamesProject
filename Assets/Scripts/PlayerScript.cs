using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerScript : MonoBehaviour
{
    Rigidbody2D _rbody;
    const float SPEED = 2.5F;
    bool _facingRight = true;
    //bool _facingUp = true;
    Animator _anim;
    // Start is called before the first frame update
    void Start()
    {
        _rbody = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float x = SPEED * Input.GetAxisRaw("Horizontal");
        float y = SPEED * Input.GetAxisRaw("Vertical");
        _anim.SetFloat("Horizontal", x);
        _anim.SetFloat("Vertical", y);


        /*if (x > 0 && !_facingRight)
        {
            FlipH();
        }
        if (x < 0 && _facingRight)
        {
            FlipH();
        }*/

        /* if(x != 0)
         {
             _anim.SetBool("isWalking", true);
         }*/

        _anim.SetFloat("Speed", (_rbody.velocity = new Vector2(x, y)).sqrMagnitude);
        
    }

    void FlipH()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        _facingRight = !_facingRight;
    }

    //void FlipV()
    //{
    //    Vector3 currentScale = gameObject.transform.localScale;
    //    currentScale.y *= -1;
    //    gameObject.transform.localScale = currentScale;
    //    _facingUp = !_facingUp;
    //}
}
