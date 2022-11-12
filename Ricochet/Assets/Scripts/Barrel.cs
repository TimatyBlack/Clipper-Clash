using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    private MeshRenderer barRenderer;
    private Material[] materials;

    public AudioSource explosionSound;
    public GameObject explosionEffect;
    public Color explosionColor = Color.red;

    public float delay = 1.5f;
    public float radius = 1f;
    public float force = 5f;

    private float countdown;
    private bool hasExploded = false;
    private bool countIsStarted = false;

    void Start()
    {
        barRenderer = GetComponent<MeshRenderer>();
        materials = barRenderer.materials;
        countdown = delay;
    }

    void Update()
    {
       
    }

    void Explode()
    {   
        explosionSound.Play();
        Instantiate(explosionEffect, transform.position, transform.rotation);

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach(Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            Rigidbody[] ragdollRb = nearbyObject.GetComponentsInChildren<Rigidbody>();
            RagdollOnOff enemie = nearbyObject.GetComponent<RagdollOnOff>();

            if(enemie != null)
            {
                enemie.RagdollModeOn();
                for(int i = 0; i < ragdollRb.Length; i++)
                {
                    ragdollRb[i].AddExplosionForce(force, transform.position, radius);
                }
            }

            if(rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }
        }

        Destroy(gameObject);
    }

    IEnumerator Countdown()
    {
        Color[] startColors = new Color[materials.Length];
        float startCountdown = countdown;

        for(int i = 0; i<startColors.Length; i++)
        {
            startColors[i] = materials[i].color;
        }

        while (countdown > 0f)
        {
            countdown -= Time.deltaTime;

            for(int i = 0; i < materials.Length; i++)
            {
                barRenderer.materials[i].color = Color.Lerp(explosionColor, startColors[i], countdown / startCountdown);
            }


            yield return null;
        }

        if(!hasExploded && !countIsStarted)
        {
            countIsStarted = true;
            Explode();
            hasExploded = true;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Shooting>())
        {
            StartCoroutine(Countdown());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Wall"))
        {
            StartCoroutine(Countdown());
        }
    }
}
