using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class EnemyController : MonoBehaviour, IEnemyHealth
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    private Transform target;
    private float maxHealth;
    private float currentHealth;
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform bulletPosition;
    [SerializeField] private Slider healthBar;
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;
    private bool hasShot = false;
    // Start is called before the first frame update
    void OnEnable()
    {
        audioSource.clip = audioClip;
        maxHealth = 300;
        currentHealth = maxHealth;
        healthBar.value = currentHealth;
        target = FindObjectOfType<TankController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = currentHealth;
        if (navMeshAgent.isActiveAndEnabled)
        {
            navMeshAgent.SetDestination(target.position);
            FaceTarget();
            if (Vector3.Distance(transform.position, target.position) <= navMeshAgent.stoppingDistance)
                StartCoroutine(Shoot());
        }

        if (currentHealth <= 0)
            EnemyDie();
        
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = lookRotation;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }

    IEnumerator Shoot()
    {
        if (!hasShot)
        {
            hasShot = true;
            prefab = ObjectPool.instance.spawnFromPool("BulletEnemy", bulletPosition.position, bulletPosition.rotation);
            yield return new WaitForSeconds(1f);
            hasShot = false;
        }
    }

    private void EnemyDie()
    {
        Spawn.instance.remainingEnemy--;
        particleSystem = Instantiate (particleSystem,transform.position,transform.rotation).GetComponent<ParticleSystem> ();
        gameObject.SetActive(false);
        hasShot = false;
        TankController.instance.score++;
        particleSystem.Play();
        audioSource.Play();
    }

   
}