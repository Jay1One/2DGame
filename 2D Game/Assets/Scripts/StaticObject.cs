using System;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class StaticObject : MonoBehaviour, IHitbox
    {
        [SerializeField] private LevelObjectData objectData;
        [SerializeField] private int health = 1;
        [SerializeField] private GameObject coinPrefab;
        
        private bool isDroppingCoins;
        private Rigidbody2D rigidbody;
        public int Health
        {
            get=>health;
            private set
            {
                health = value;
                if (health < 0f)
                {
                    Die();
                }
            }
        }
        public void Hit(int damage)
        {
            Health -= damage;
        }

        public void Die()
        {
            if (isDroppingCoins)
            {
                var coinWidth = coinPrefab.GetComponent<CircleCollider2D>().radius;
                int k = 1;
                for (int i = 0; i < Random.Range(4,5); i++)
                {
                    var coinPositionOffset =   i * k * coinWidth;
                  var  coin=Instantiate(coinPrefab, transform.position, quaternion.identity);
                  coin.transform.position += new Vector3(coinPositionOffset,0,0);
                  k *= -1;
                }
                
                
            }
            Destroy(gameObject);
        }

        private void Start()
        {
            health = objectData.Health;
            rigidbody = GetComponent<Rigidbody2D>();
            rigidbody.bodyType = objectData.IsStatic ? RigidbodyType2D.Static : RigidbodyType2D.Dynamic;
            coinPrefab = objectData.CoinPrefab;
            if (coinPrefab != null)
            {
                isDroppingCoins = true;
            }
        }

        #region Editor Only
#if  UNITY_EDITOR
        
        [ContextMenu("Rename")]
        private void Rename()
        {
            if (objectData!= null)
            {
                gameObject.name = objectData.Name;
            }
        }

        [ContextMenu("Move Right")]
        private void MoveRight()
        {
         Move(Vector2.right);   
        }
[ContextMenu("Move Left")]
        private void MoveLeft()
        {
            Move(Vector2.left);
        }

        private void Move(Vector2 direction)
        {
            var collider = GetComponent<Collider2D>();
            var size = collider.bounds.max;
            transform.Translate((direction=size));
        }

        [ContextMenu("Create a copy above")]
        private void CreateCopyAbove()
        {
            Instantiate(gameObject);
            Move(Vector2.up);
        }
#endif
        #endregion
    }
}