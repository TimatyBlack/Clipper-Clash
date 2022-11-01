using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Shooting : MonoBehaviour
{
    [SerializeField] private float force = 5;

    private Transform pad_transform;

    public GameObject spring;
    public GameObject hitEffect;

    public event System.Action onShoot;

    private Vector3 finalShotPos;
    private Vector3 finalSpringScale;

    private bool isShooting = false;
    private bool isHit = false;

    public int shotsLeft = 1;
    void Start()
    {
        pad_transform = this.transform;
        finalShotPos = new Vector3(pad_transform.localPosition.x + 1.5f, 
                                   pad_transform.localPosition.y, 
                                   pad_transform.localPosition.z);

        finalSpringScale = new Vector3(2.6f, 
                                       spring.transform.localScale.y, 
                                       spring.transform.localScale.z);
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0) && isShooting == false && shotsLeft > 0)
        {
            shotsLeft -= 1;
            isShooting = true;
            pad_transform.DOLocalMove(finalShotPos, 0.2f).SetLoops(2, LoopType.Yoyo).OnComplete(() => { isShooting = false; });
            spring.transform.DOScale(finalSpringScale, 0.2f).SetLoops(2, LoopType.Yoyo);
            onShoot();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Rigidbody>(out Rigidbody rigidbody) && isShooting == true)
        {   
            if(other.gameObject.TryGetComponent<RagdollOnOff>(out RagdollOnOff ragdolls))
            {
                ragdolls.RagdollModeOn();
                Rigidbody[] rigidbodiesInChilddren = other.GetComponentsInChildren<Rigidbody>();
                for(int i = 0; i < rigidbodiesInChilddren.Length; i++)
                {
                    rigidbodiesInChilddren[i].AddForce(transform.right * (force*0.5f), ForceMode.Impulse);
                }
            }

            rigidbody.AddForce(transform.right * force, ForceMode.Impulse);
            rigidbody.AddTorque(Vector3.forward * -180);
            Debug.DrawRay(transform.position, transform.right * force, Color.red, 5);
        }

        if (isHit == false && !other.gameObject.CompareTag("Wall"))
        {
            Instantiate(hitEffect, transform.position, Quaternion.identity);
            isHit = true;
        }
    }
}
