using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Forcefield : MonoBehaviour
{
    [SerializeField] float pushForce = 2f;
    Vector2 pushDirection;

    List<Rigidbody2D> rigidbodys = new List<Rigidbody2D>();
    List<float> timers = new List<float>();

    private void Awake() {

        //Finds the one-dimensional direction, where the force field pushes the player. It will always push towards the X- or Y-axis.
        Collider2D thisCollider = GetComponent<BoxCollider2D>();
        Vector2 closestPoint = thisCollider.ClosestPoint(Vector2.zero);
        closestPoint.Normalize();
        if (Mathf.Abs(closestPoint.x) > Mathf.Abs(closestPoint.y)) {
            if (closestPoint.x < 0) {
                pushDirection = new Vector2(1, 0);
            } else if (closestPoint.x >= 0) {
                pushDirection = new Vector2(-1, 0);
            }

        } else if (Mathf.Abs(closestPoint.y) >= Mathf.Abs(closestPoint.x)) {
            if (closestPoint.y < 0) {
                pushDirection = new Vector2(0, 1);
            } else if (closestPoint.x >= 0) {
                pushDirection = new Vector2(0, -1);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
        float timer = 1;
        if (rb != null) {
            rigidbodys.Add(rb);
            timers.Add(timer);
        }     
    }

    private void OnTriggerStay2D(Collider2D collision) {

        for(int i = 0; i < rigidbodys.Count; i++) {
            timers[i] += Time.fixedDeltaTime;

            if(rigidbodys[i].GetComponent<RockBehaviour>() != null) {
                rigidbodys[i].AddForce(pushDirection * pushForce * timers[i]);
            } else {
                rigidbodys[i].AddForce(pushDirection * pushForce * timers[i] * timers[i]);
            }

            
        }    
    }

    private void OnTriggerExit2D(Collider2D collision) {
        
        int index = rigidbodys.IndexOf(collision.GetComponent<Rigidbody2D>());
        if (index != -1) {
            rigidbodys.RemoveAt(index);
            timers.RemoveAt(index);
        }
    }
}
