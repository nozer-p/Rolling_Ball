using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask collisionMask;
    [SerializeField] private GameObject prefabSmoke;

    [SerializeField] private GameObject skin;

    [SerializeField] private float mass;
    [SerializeField] private float offset;
    [SerializeField] private float kef;
    private float force;
    private float forceFriction;
    private float g = 9.81f;

    private void FixedUpdate()
    {
        Move();
        Ray();
    }

    public void Move()
    {
        transform.Translate(Vector3.forward * force * Time.deltaTime);
        skin.transform.Rotate(force, 0f, 0f);
        
        if (force > 0)
        {
            forceFriction = kef * mass * g;
            force -= forceFriction;
        }
        else
        {
            force = 0f;
        }
    }

    public void ChangeDelta(float delta)
    {
        force = delta;
    }

    public void ChangeDirection(Vector3 direction)
    {
        float angle = Mathf.Atan2(direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, angle + 90f, 0f);
    }

    private void Ray()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Time.deltaTime * force + offset, collisionMask))
        {
            Vector3 reflectDir = Vector3.Reflect(ray.direction, hit.normal);
            float rot = 90 - Mathf.Atan2(reflectDir.z, reflectDir.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0f, rot, 0f);
            Instantiate(prefabSmoke, transform.position, Quaternion.Euler(0f, hit.transform.eulerAngles.y + 90, 0f));
        }
    }
}