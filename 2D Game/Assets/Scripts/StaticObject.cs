using System;
using UnityEngine;

namespace DefaultNamespace
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class StaticObject : MonoBehaviour, IHitbox
    {
        [SerializeField] private LevelObjectData objectData;
        [SerializeField] private int health = 1;
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
            print("Object has been destroyed"); 
            Destroy(gameObject);
        }

        private void Start()
        {
            health = objectData.Health;
            rigidbody = GetComponent<Rigidbody2D>();
            rigidbody.bodyType = objectData.isStatic ? RigidbodyType2D.Static : RigidbodyType2D.Dynamic;
        }

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
    }
}