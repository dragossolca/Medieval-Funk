using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Animator animator;
    public NavMeshAgent agent;
    string currentAnimation;
    string attack = "Female attack";
    string run = "Female run";
    string walk = "Female walk";
    string die = "Female die";
    [HideInInspector]public bool enemyDead;
    public Transform player;
    private Health playerHealth;
    private Score playerScore;
    public GameObject Canvas;
    public GameObject enemyPlayer;

    public LayerMask Ground, Player;

    public float health;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        playerHealth = enemyPlayer.GetComponent<Health>();
        playerScore = Canvas.GetComponent<Score>();
        enemyDead = false;
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, Player);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, Player);

        if (!playerInSightRange && !playerInAttackRange && health > 0){
            ChangeAnimationState(walk); 
            Patroling();
        }
        if (playerInSightRange && !playerInAttackRange && health > 0){
            ChangeAnimationState(run); 
            ChasePlayer();
        }
        if (playerInAttackRange && playerInSightRange && health > 0){
            ChangeAnimationState(attack); 
            AttackPlayer();
        }
        if(health <=0) {
            DieEnemy();
            health = 100;

        }
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 3f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, Ground))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            playerHealth.health -=10;
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
    public void DieEnemy()
    {
        ChangeAnimationState(die);
        Invoke("Respawn", 0.5f);
        

    }

    void Respawn(){
        enemyDead = true;
        gameObject.SetActive(false);
        playerScore.scoreValue += 100;
    }

    public void ChangeAnimationState(string anim)
    {
        if(currentAnimation == anim ) return;
    
        animator.SetTrigger(anim);
        currentAnimation = anim;
    
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
