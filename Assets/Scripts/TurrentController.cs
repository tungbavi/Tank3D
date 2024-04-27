using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurrentController : MonoBehaviour
{
    public float rotationSpeed = 20f;
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform bulletPosition;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;
    private bool isShooting = false;
    [SerializeField] private Joystick joystick;
    
    private void Awake()
    {
        audioSource.clip = audioClip;
    }

    private void Update()
    {
        RotateByJoystick();
    }
    
    IEnumerator Shoot()
    {
        while (true)
        {
            prefab = ObjectPool.instance.spawnFromPool("Bullet", bulletPosition.position, bulletPosition.rotation);
            audioSource.Play();
            yield return new WaitForSeconds(.1f);
            if (!Input.GetMouseButton(0)) 
            {
                isShooting = false; 
                yield break; 
            }
        }
    }

    private void RotateByKeyboard()
    {
        Vector3 mousePositionScreen = Input.mousePosition;
        Vector3 mousePositionWorld =
            Camera.main.ScreenToWorldPoint(new Vector3(mousePositionScreen.x, mousePositionScreen.y, 25f));
        Vector3 direction = mousePositionWorld - transform.position;
        direction.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        if (Input.GetMouseButtonDown(0) && !isShooting)
        {
            isShooting = true;
            StartCoroutine(Shoot());
        }
    }

    private void RotateByJoystick()
    {
        Vector3 direction = new Vector3(joystick.Vertical, 0, -joystick.Horizontal).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            if (!isShooting)
            {
                isShooting = true;
                StartCoroutine(Shoot());
            }
        }
    }
}