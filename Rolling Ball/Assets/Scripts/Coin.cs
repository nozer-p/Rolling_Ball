using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private GameObject roundUpCoin;
    [SerializeField] private GameObject coinLineWhite;
    [SerializeField] private GameObject cubes;
    [SerializeField] private float speedRotate;
    private ScoreManager scoreManager;

    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    private void FixedUpdate()
    {
        transform.Rotate(0f, speedRotate, 0f);        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Instantiate(roundUpCoin, transform.position, Quaternion.identity);
            Instantiate(coinLineWhite, transform.position, Quaternion.identity);
            Instantiate(cubes, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), Quaternion.Euler(-90f, 0f, 0f));
            scoreManager.AddCoin();
            Destroy(this.gameObject);
        }
    }
}