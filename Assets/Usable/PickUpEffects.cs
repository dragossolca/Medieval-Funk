using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PickUpEffects : MonoBehaviour
{   
    public GameObject Player;
    public GameObject Canvas;
    private Health PlayerHealth;
    private Score PlayerScore;
    void Start()
    {
        PlayerHealth = Player.GetComponent<Health>();
        PlayerScore = Canvas.GetComponent<Score>();
    }
    void OnTriggerEnter(Collider other)
    {
        if(this.gameObject.tag=="PlayerHeal")
            PlayerHealth.health +=50;
        if(this.gameObject.tag=="PlayerScore") {
            PlayerHealth.health = 100;
            PlayerScore.scoreValue += 1000;
        }
        Destroy(GetComponent<Collider>().gameObject);
    }
}
