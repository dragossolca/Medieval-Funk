using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private float speed = 10;
    private float speedRotation = 200;
    private string currentAnimation;
    private string idle = "Male Idle";
    private string run = "Male Sprint";
    public GameObject Canvas;
    private Health PlayerHealth;
    private Score PlayerScore;
    public GameObject Player;

    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        PlayerHealth = GetComponent<Health>();
        PlayerScore = Canvas.GetComponent<Score>();

    }

    void Update()
    {
        float move = Input.GetAxis("Vertical") * speed;        
        float rotate = Input.GetAxis("Horizontal") * speedRotation;        

        move *= Time.deltaTime;
        rotate *=Time.deltaTime;

        if(Input.GetKey(KeyCode.W)){transform.Translate(0,0,move); ChangeAnimationState(run);}
        if(Input.GetKeyUp(KeyCode.W)){ChangeAnimationState(idle);}
        transform.Rotate(0,rotate,0);

        if(PlayerHealth.health <= 0){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            PlayerScore.scoreValue = 0;
            PlayerHealth.health = 100;
            
        }
        

    }
    public void ChangeAnimationState(string anim)
    {
        if(currentAnimation == anim ) return;
    
        animator.SetTrigger(anim);
        currentAnimation = anim;
    
    }
   
}
