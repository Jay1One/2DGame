using System;
using System.Collections;
using DefaultNamespace;
using UnityEngine;

   public class Platform : MonoBehaviour
   {
       [SerializeField] private bool moved;
       [SerializeField] private Vector3 distance = new Vector3(6f,0,0);
       [SerializeField] private float speed = 0.5f;
       private Vector3 startPosition;
       private Vector3 targetPosition;

       private void Start()
       {
           if (moved)
           {
               startPosition = transform.position;
               targetPosition = transform.position;
               targetPosition += distance;
               StartCoroutine(MovementProcess());
           }
       }

       private IEnumerator MovementProcess()
       {
           var k = 0f;
           var dir = 1f;
           while (true)
           {
               transform.position=Vector3.Lerp(startPosition,targetPosition,k);
               k += Time.deltaTime*dir*speed;
               if (k>1f)
               {
                   dir =-1;
                   k = 1f;
                   yield return new WaitForSeconds(1f);
               }
               if (k<0f)
               {
                   dir = 1;
                   k = 0f;
                   yield return new WaitForSeconds(1f);
               }
               yield return null;
           }
       }

       private void OnTriggerEnter2D(Collider2D other)
       {
           bool isMovementObject = other.transform.GetComponent<CharacterMovement>();
           if (isMovementObject)
           {
               other.transform.parent=transform;
           }
       }

       private void OnTriggerExit2D(Collider2D other)
       {
           if (other.transform.parent == transform)
           {
               other.transform.parent = null;
           }
       }
   }
