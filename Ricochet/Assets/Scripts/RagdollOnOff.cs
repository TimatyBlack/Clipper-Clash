using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollOnOff : MonoBehaviour
{
    public SkinnedMeshRenderer meshRenderer;
    public Material grey;
    public BoxCollider mainCollider;
    public GameObject thisRig;
    public Animator animator;
    public AudioSource dieSound;
    public float force = 25f;

    public bool isDead = false;

    void Start()
    {
        GetRagdollBits();
        RagdollModeOff();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody>())
        {
            RagdollModeOn();

            Vector3 direction = collision.transform.position - transform.position;

            for (int i = 0; i < limbsRigidbodies.Length; i++)
            {
                limbsRigidbodies[i].AddForce(-direction.normalized * force, ForceMode.Impulse);
            }
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
        dieSound.Play();

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

        meshRenderer.material = grey;
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
