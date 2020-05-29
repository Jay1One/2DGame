using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarBonus : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private float effectLifeTime = 1;
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    void Start()
    {
        particleSystem.Stop();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.transform.root.GetComponent<Player>();
        if (player != null)
        {
            spriteRenderer.enabled = false;
            particleSystem.Play();
            Destroy(gameObject, effectLifeTime);
        }
    }
}
