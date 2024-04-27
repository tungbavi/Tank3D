using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TankController : MonoBehaviour,IPlayerHealth
{
    public CharacterController controller;
    public float speed = 17f;
    public float gravity = -15f;
    private Vector3 velocity;
    private float turnSmoothVelocity = 0f;
    private float turnSmoothTime = .1f;
    [SerializeField] private ParticleSystem[] particleSystems;
    private float maxHealth;
    private float currentHealth;
    [SerializeField] private Slider healthBar;
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private Text pointText;
    [SerializeField] private Text timeText;
    public int score;
    [SerializeField] private Image UI;
    [SerializeField] private GameObject enemyUI;
    [SerializeField] private Joystick joystick;
    public static TankController instance;
    [SerializeField] private Slider sliderHealth;
    
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        score = 0;
        audioSource.clip = audioClip;
        maxHealth = 500;
        currentHealth = maxHealth;
        healthBar.value = currentHealth;
        for (int i = 0; i < particleSystems.Length; i++)
        {
            particleSystems[i].Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = currentHealth;
        sliderHealth.value = currentHealth;
        TankMovement();
        if (currentHealth <= 0 || score == 40)
            StartCoroutine(GameOverUI());
        //pointText.text = " Kills: " + score.ToString();
    }

    private void OnDisable()
    {
        for (int i = 0; i < particleSystems.Length; i++)
        {
            particleSystems[i].Stop();
        }
    }

    private void TankMovement()
    {
        // float x = Input.GetAxis("Vertical");
        // float z = Input.GetAxis("Horizontal");
        float x = joystick.Vertical;
        float z = joystick.Horizontal;
        Vector3 moveDirection = new Vector3(x, 0f, -z).normalized;
        if (moveDirection.magnitude > 0f)
        {
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity,
                turnSmoothTime);
            Quaternion targetRotation = Quaternion.Euler(0f, angle, 0f);
            transform.rotation = targetRotation;
            
        }
        controller.Move(moveDirection * speed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }

    IEnumerator GameOverUI()
    {
        particleSystem.Play();
        audioSource.Play();
        yield return new WaitForSeconds(.5f);
        sliderHealth.gameObject.SetActive(false);
        enemyUI.SetActive(false);
        UI.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
}
