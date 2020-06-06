using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private GameManager gameManager;
    private void OnCollisionEnter2D(Collision2D other)
    {
        var player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            GetComponent<Collider2D>().enabled = false;
            Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
            rigidbody.bodyType = RigidbodyType2D.Kinematic;
            rigidbody.velocity=Vector2.zero;
            gameManager = FindObjectOfType<GameManager>();
            StartCoroutine(MoveToUI());
        }
        
    }

    private IEnumerator MoveToUI()
    {
        Camera mainCamera = FindObjectOfType<Camera>();
        Vector2 startPosition = mainCamera.WorldToScreenPoint(transform.position);
        Vector2 endPosition = gameManager.CoinUIImage.rectTransform.position;
        var timer = 1f;
        var k = 0f;
        while (timer > 0)
        {
            timer-= Time.deltaTime;
            k += Time.deltaTime;
            Vector2 position = mainCamera.ScreenToWorldPoint(Vector2.Lerp(startPosition, endPosition, k));
            transform.position = position;
            yield return null;
        }
        gameManager.Coins++;
        Destroy(gameObject);
    }
}
