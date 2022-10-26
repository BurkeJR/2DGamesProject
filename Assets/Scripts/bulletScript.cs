using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class bulletScript : MonoBehaviour
{
    public float speed = 10f;
    Rigidbody2D _rbody;
    // Start is called before the first frame update
    void Start()
    {
        _rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _rbody.velocity = transform.right * speed;
    }
}
