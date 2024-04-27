using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BulletEnemy : MonoBehaviour
{
    private float  damage;
    [SerializeField] 
    private ParticleSystem bulletVFX;
    // [SerializeField] private AudioSource audioSource;
    // [SerializeField] private AudioClip audioClip;
    private void OnEnable()
    {
       // audioSource.clip = audioClip;
        damage = 2;
        StartCoroutine(ReturnToPool());
    }

    void Update()
    {
        transform.Translate(Vector3.forward *30*Time.deltaTime);
        StartCoroutine(CheckCollide());
    }

    IEnumerator ReturnToPool()
    {
        yield return new WaitForSeconds(2f);
        ObjectPool.instance.returnToPool(gameObject);
    }

    IEnumerator CheckCollide()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit,3f))
        {
            //audioSource.Play();
            IPlayerHealth player = hit.collider.GetComponent<IPlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(damage);
                bulletVFX.Play();
                yield return new WaitForSeconds(.1f);
                ObjectPool.instance.returnToPool(gameObject);
            }
        }
    }
}
