using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerScript : MonoBehaviour
{
    Rigidbody2D _rbody;
    float SPEED;
    Animator _anim;
    Vector2 _pointerInput, movementInput;
    [SerializeField] private InputActionReference movement, attack, pointerPos;
    WeaponParent _WeaponParent;

    public Vector2 MovementInput { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        _rbody = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _WeaponParent = GetComponentInChildren<WeaponParent>();
        SPEED = 4F + PlayerPrefs.GetInt(ConstLabels.pref_upgrade_speed);
    }

    // Update is called once per frame
    void Update()
    {

        _pointerInput = GetPointerInput();
        
        _WeaponParent.PointerPosition = _pointerInput;
    }

    private void FixedUpdate()
    {
        /*float x = SPEED * Input.GetAxisRaw("Horizontal");
        float y = SPEED * Input.GetAxisRaw("Vertical");*/
        movementInput = SPEED * movement.action.ReadValue<Vector2>();

        Vector3 mouse = GetPointerInputForPlayer();
        _anim.SetFloat("Horizontal", mouse.x);
        _anim.SetFloat("Vertical", mouse.y);

        _anim.SetFloat("Speed", (_rbody.velocity = new Vector2(movementInput.x, movementInput.y)).sqrMagnitude);
        
    }


    private Vector2 GetPointerInput()
    {
        Vector3 mousePos = pointerPos.action.ReadValue<Vector2>();
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
    private Vector2 GetPointerInputForPlayer()
    {
        Vector3 mousePos = pointerPos.action.ReadValue<Vector2>();
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos) - transform.position;
    }

    private void OnEnable()
    {
        attack.action.performed += PerformAttack;
    }

    private void OnDisable()
    {
        attack.action.performed -= PerformAttack;
    }
    private void PerformAttack(InputAction.CallbackContext obj)
    {
        _WeaponParent.Attack();
    }


}
