using System;
using System.Timers;
using UnityEngine;

public enum EnemyState
{
    Sleep,
    Wait,
    StartWalking,
    Walk,
    StartAttacking,
    Attack,
    StartRunaway,
    Runaway,
    
}
public class Enemy : MonoBehaviour, IEnemy, IHitbox
{
    [SerializeField] private int health = 1;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform checkGroundPoint;
    [SerializeField] private Transform checkAttackPoint;
    [SerializeField] private Transform graphics;
    [SerializeField] private Transform helpers;
    [SerializeField] private bool isCoward;

    private GameManager gameManager;
    
    private EnemyState currentEnemyState=EnemyState.Sleep;
    private EnemyState nextState;
    
    
    private float wakeUpTimer;
    private float waitTimer;
    private float runawayTimer;
    private float attackTimer;
    private float currentDirection = 1f;
    
    public int Health
    {
        get=>health;
        private set
        {
            health = value;
            if (health < 0)
            {
                Die();
            }
        }
    }
    
    public void RegisterEnemy()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.Enemies.Add(this);
    }

    private void Update()
    {
        switch (currentEnemyState)
        {
            case EnemyState.Sleep:
                Sleep();
                break;
            case EnemyState.Wait:
                Wait();
                break;
            case EnemyState.StartWalking:
                animator.SetInteger("Walking", 1);
                currentEnemyState = EnemyState.Walk;
                break;
            case EnemyState.Walk:
                Walk();
                break;
            case EnemyState.StartAttacking:
                animator.SetTrigger("Attack");
                ((IHitbox) gameManager.Player).Hit(1);
                currentEnemyState = EnemyState.Attack;
                attackTimer = Time.time + 1f;
                break;
            case  EnemyState.Attack:
                Attack();
                break;
            case EnemyState.StartRunaway:
                animator.SetInteger("Walking", 1);
                animator.speed = 3;
                currentEnemyState = EnemyState.Runaway;
                runawayTimer = Time.time + 2f;
                break;
            case EnemyState.Runaway:
                Runaway();
                break;
        }
    }

    private void StartSleeping(float sleeptime=1f)
    {
        waitTimer = Time.time + sleeptime;
        currentEnemyState = EnemyState.Sleep;
    }
    private void Attack()
    {
        if (Time.time < attackTimer)
        {
            return;
        }

        currentEnemyState = EnemyState.Wait;
        nextState = EnemyState.StartWalking;
        waitTimer = Time.time + 0.2f;
    }

    private void Runaway()
    {
        if (runawayTimer < Time.time)
        {
            currentEnemyState = EnemyState.Wait;
            nextState = EnemyState.Walk;
            animator.speed = 1;
            waitTimer = Time.time + 0.2f;
            return;
        }
        
        float direction=(transform.position-((MonoBehaviour) gameManager.Player).transform.position).x;
        if (direction > 0)
        {
            currentDirection = 1;
            transform.Translate(Vector3.right * Time.deltaTime);
        }
        else
        {
            currentDirection = -1;
            transform.Translate(Vector3.left * Time.deltaTime);
        }
        graphics.localScale = new Vector3(currentDirection, 1f, 1f);
        float xAngle = currentDirection > 0f ? 0f : 180f;
        helpers.localEulerAngles = new Vector3(0f, xAngle, 0f);
        
           

    }


    private void Walk()
    {
        transform.Translate(transform.right * (Time.deltaTime * currentDirection));
        //проверяем возможность идти дальше
        RaycastHit2D hit = Physics2D.Raycast(checkGroundPoint.position, Vector2.down, 0.3f);
        if (hit.collider == null)
        {
            currentDirection *= -1;
            graphics.localScale = new Vector3(currentDirection, 1f, 1f);

            float xAngle = currentDirection > 0f ? 0f : 180f;
            helpers.localEulerAngles = new Vector3(0f, xAngle, 0f);
            currentEnemyState = EnemyState.Wait;
            nextState = EnemyState.StartWalking;
            waitTimer = Time.time + 0.2f;
            animator.SetInteger("Walking",0);
            return;
            
        }

        hit = Physics2D.Raycast(checkAttackPoint.position, checkAttackPoint.right, 0.2f);
        if (hit.collider != null)
        {
            var player = hit.collider.GetComponent<Player>();
            {
                if (player!=  null)
                {
                    currentEnemyState = EnemyState.StartAttacking;
                }
            }
        }
    }
    private void Sleep()
    {
        if (Time.time >= wakeUpTimer)
        {
            WakeUp();
        }
    }

    private void WakeUp()
    {
        var playerPosition = ((MonoBehaviour) gameManager.Player).transform.position;
        if (Vector3.Distance(transform.position, playerPosition) > 20f)
        {
            wakeUpTimer = Time.time + 1f;
            return;
        }

        currentEnemyState = EnemyState.Wait;
        nextState = EnemyState.StartWalking;
        waitTimer = Time.time + 0.1f;
    }

    private void Wait()
    {
        if (Time.time >= waitTimer)
        {
            currentEnemyState = nextState;
        }
    }

    private void Awake()
    {
        RegisterEnemy();
        waitTimer = Time.time + 1f;
        
    }

    public void Hit(int damage)
    {
        Health -= damage;
        if (isCoward)
        {
            currentEnemyState = EnemyState.StartRunaway;
        }
    }

    public void Die()
    {
        print("Enemy has died"); 
        animator.SetTrigger("Die");
        Destroy(gameObject,0.5f);
    }
}