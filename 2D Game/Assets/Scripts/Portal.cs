using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Transform exitPoint;
    [SerializeField] private Animator animator;
    private Player player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.GetComponent<Player>())
        {
            player = other.transform.GetComponent<Player>();
            animator.SetTrigger("Portal");
            StartCoroutine(PortalProcess());
        }
    }

    private IEnumerator PortalProcess()
    {
        var playerAnimator = player.Animator;
        playerAnimator.SetTrigger("Portal");
        yield return new WaitForSeconds(1f);
        player.transform.position = exitPoint.position;
        
    }
}
