using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField] private Player player;

    private void FixedUpdate()
    {
        transform.position = player.gameObject.transform.position;
    }
}
