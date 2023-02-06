using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"OnCollisionEnter" + collision.gameObject.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        // if(other.gameObject == gameObject) return;
        Debug.Log($"OnTriggerEnter" + other.gameObject.name);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log($"OnControllerColliderHit" + hit.gameObject.name);
    }
}
