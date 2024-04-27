using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletPlayer : MonoBehaviour
{
    private float  damage;
    [SerializeField] 
    private ParticleSystem bulletVFX;
    // [SerializeField] private AudioSource audioSource;
    // [SerializeField] private AudioClip audioClip;
    
    private void OnEnable()
    {
        //audioSource.clip = audioClip;
        damage = 30;
        StartCoroutine(ReturnToPool());
    }

    void Update()
    {
        transform.Translate(Vector3.forward * 45 * Time.deltaTime);
    }

    IEnumerator ReturnToPool()
    {
        yield return new WaitForSeconds(1f);
        ObjectPool.instance.returnToPool(gameObject);
    }

    IEnumerator OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("TankBlue") || other.collider.CompareTag("TankYellow") )
        {
           // audioSource.Play();
            var hp = other.gameObject.GetComponent<IEnemyHealth>();
            if (hp != null) 
                hp.TakeDamage(damage);
            bulletVFX.Play();
            yield return new WaitForSeconds(.1f);
            ObjectPool.instance.returnToPool(gameObject);
        }
       
    }
}
