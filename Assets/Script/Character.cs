using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rgbd;

    [SerializeField] 
    private float _speed;
    private Vector3 _velocity;

    void Update()
    {
        _velocity.x = Input.GetAxis("Horizontal");
        _velocity.z = Input.GetAxis("Vertical");

    }

    private void FixedUpdate()
    {
        _velocity = _velocity.normalized*_speed*Time.deltaTime;
        _rgbd.MovePosition(transform.position + _velocity);   
    }

    private void Move()
    {
        
    }
}
