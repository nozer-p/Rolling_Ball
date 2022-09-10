using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float mass;
    private float delta;
    private Vector3 direction;

    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        transform.Translate(direction * delta * Time.deltaTime);
    }

    public void ChangeDelta(float delta)
    {
        this.delta = delta;
    }

    public void ChangeDirection(Vector3 direction)
    {
        direction = direction.normalized;
        this.direction = new Vector3(-direction.x, direction.z, -direction.y);
    }

    public void Rebound()
    {
        direction = Vector3.Reflect(direction, Vector3.left);
    }
}