using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private int coins;
    private int coinsTmp;
    [SerializeField] private TextMeshProUGUI coinsText;
    private float timeBtwGoals;
    [SerializeField] private float startTimeBtwGoals;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        coins = 0;
    }

    private void Update()
    {
        if (timeBtwGoals > 0 && coinsTmp < coins) 
        {
            coinsTmp += 1;
            animator.SetBool("Add", true);
            timeBtwGoals -= Time.deltaTime;
        }
        else
        {
            animator.SetBool("Add", false);
            timeBtwGoals = 0f;
        }
        
        coinsText.text = coinsTmp.ToString();    
    }

    public void AddCoin()
    {
        timeBtwGoals = startTimeBtwGoals;
        coins += 50;
    }
}