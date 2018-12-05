using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Configuration
    [SerializeField] private float _velocity;
    [SerializeField] private float _jump;
    [SerializeField] private float _health;
    [SerializeField] private float _mana;

    // Cache
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Animator _animator;

    [SerializeField] private GameObject _castVfx;
    [SerializeField] private AudioClip _castSfx;

    private List<PickupableObject> _pickUps;

    private bool _isJumping;

    // Use this for initialization
    void Start()
    {
        _pickUps = new List<PickupableObject>();
    }

    public void AddPickup(PickupableObject pickupableObject)
    {
        _pickUps.Add(pickupableObject);
    }

    public void RemovePickup(PickupableObject pickupableObject)
    {
        _pickUps.Remove(pickupableObject);
    }

    public bool HaveGotInPickups(PickupableObject pickupableObject)
    {
        return _pickUps.Contains(pickupableObject);
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        JumpOrCast();
    }

    private void Run()
    {
        var horAxis = Input.GetAxis("Horizontal");
        var horVelocity = _velocity * horAxis;
        _rigidbody.velocity = new Vector2(_velocity * horAxis, _rigidbody.velocity.y);
        _animator.SetBool("IsRunning", Mathf.Abs(horVelocity) > 0);
        transform.localScale = new Vector3(Mathf.Sign(horVelocity), 1, 1);
    }

    private void JumpOrCast()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (_isJumping)
            {
                Cast();
            }
            else
            {
                _isJumping = true;
                Jump();
            }
        }
    }

    private void Jump()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jump);
    }

    private void Cast()
    {
        CastVfx();
    }

    private void CastVfx()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _isJumping = false;
    }
}