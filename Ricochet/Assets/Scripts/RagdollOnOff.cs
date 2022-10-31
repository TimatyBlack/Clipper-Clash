using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollOnOff : MonoBehaviour
{
    public BoxCollider mainCollider;
    public GameObject thisRig;
    public Animator animator;

    public bool isDead = false;

    void Start()
    {
        GetRagdollBits();
        RagdollModeOff();
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody>())
        {
            RagdollModeOn();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Obstacle>())
        {
            RagdollModeOn();
        }
    }

    Collider[] ragdollColliders;
    Rigidbody[] limbsRigidbodies;
    void GetRagdollBits()
    {
        ragdollColliders = thisRig.GetComponentsInChildren<Collider>();
        limbsRigidbodies = thisRig.GetComponentsInChildren<Rigidbody>();
    }

    public void RagdollModeOn()
    {
        isDead = true;

        animator.enabled = false;

        foreach (Collider col in ragdollColliders)
        {
            col.enabled = true;
        }

        foreach (Rigidbody rb in limbsRigidbodies)
        {
            rb.isKinematic = false;
        }

        mainCollider.enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    public void RagdollModeOff()
    {
        foreach(Collider col in ragdollColliders)
        {
            col.enabled = false;
        } 
        
        foreach(Rigidbody rb in limbsRigidbodies)
        {
            rb.isKinematic = true;
        }

        animator.enabled = true;
        mainCollider.enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
    }
}
