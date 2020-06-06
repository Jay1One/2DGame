using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour, IHitbox
{
    [SerializeField] private Rigidbody2D rigidbody2D;
    private void Start()
    {
        Destroy(gameObject,4f);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //нанести урон игроку если скороть камня большая
        if (other.gameObject.GetComponent<Player>()!=null && rigidbody2D.velocity.magnitude>0.01f)
        {
            other.gameObject.GetComponent<Player>().Hit(1);
            Destroy(gameObject);
        }
        
        //чтобы не отскакивать слишком много раз от эффектора
        if (other.collider.usedByEffector)
        {
            gameObject.layer = default;
        }
    }

    public int Health { get; }
    public void Hit(int damage)
    {
        Die();
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
