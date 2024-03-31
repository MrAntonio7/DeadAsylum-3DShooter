using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public CharacterController characterController;

    //Gravedad
    public float gravity = -9.8f;
    private Vector3 velocity;

    //GroundCheck
    public Transform groundCheck;
    public float sphereRadius = 0.3f;
    public LayerMask groundMask;
    public bool isGrounded;
    public float jumpHeight = 300f;

    //Correr
    public bool isSrinting = false;
    public float sprintingSpeedMultiplier =3f;
    public float sprintSpeed = 1;

    public StamineBar stamineSlider;
    public float stamineAmount = 5;

    public StamineBar LifeSlider;
    public int life = 20;
    public int currentHealth; // Vida actual del jugador
    public int damagePerSecond = 1; // Cantidad de da�o por segundo

    public float amplitud = 0.1f; // La amplitud de la oscilaci�n
    public float velocidadOscilacion = 2f; // La velocidad de la oscilaci�n

    private AudioSource source;
    public bool currentHit;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        // Al inicio, la vida actual del jugador es igual a la vida m�xima
        currentHealth = life;
        //stamineSlider = FindAnyObjectByType<StamineBar>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = -Input.GetAxis("Horizontal");
        float z = -Input.GetAxis("Vertical");
        

        if (GameManager.instance.muerto)   
        {
            return;
        }
        

        Vector3 move = transform.right * x + transform.forward * z;
        characterController.Move(move*speed*Time.deltaTime*sprintSpeed);

        // Oscilaci�n vertical al andar
        ApplyVerticalOscillation();

        //Gravedad
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

        //GroundCheck
        isGrounded = Physics.CheckSphere(groundCheck.position, sphereRadius, groundMask);

        if (isGrounded&&velocity.y < 0)
        {
            velocity.y = -2f;

        }


        //Salto

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity * Time.deltaTime);
        }

        //Correr
        RunCheck();


    }

    private void RunCheck()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSrinting = !isSrinting;
            if (isSrinting)
            {
                sprintSpeed = sprintingSpeedMultiplier;
                stamineSlider.UseStamine(stamineAmount);
            }
            else
            {
                sprintSpeed = 1;
                stamineSlider.UseStamine(0);
            }
        }
    }


    //Carrera


    // Oscilaci�n vertical al andar
    private void ApplyVerticalOscillation()
    {
        float oscilacionY = amplitud * Mathf.Sin(velocidadOscilacion * Time.time);

        // Aplicar la oscilaci�n a la posici�n del jugador
        transform.position = new Vector3(transform.position.x, oscilacionY, transform.position.z);
    }

    // Corutina para restar vida al jugador cada segundo
    public IEnumerator TakeDamageOverTime()
    {
        
        // Repetir el proceso mientras la vida del jugador sea mayor que cero
        while (currentHealth > 0 && currentHit)
        {
            // Restar vida al jugador
            TakeDamage(damagePerSecond);

            // Esperar un segundo antes de restar m�s vida
            yield return new WaitForSeconds(1f);
        }
    }

    // M�todo para restar vida al jugador
    public void TakeDamage(int amount)
    {
        source.Play();
        // Restar la cantidad de da�o recibida a la vida actual del jugador
        currentHealth -= amount;

        // Asegurarse de que la vida actual del jugador no sea menor que cero
        currentHealth = Mathf.Max(currentHealth, 0);

        // Aqu� podr�as agregar cualquier l�gica adicional que desees al recibir da�o
        Debug.Log("�El jugador ha recibido " + amount + " puntos de da�o! Vida actual: " + currentHealth);

        // Verificar si la vida del jugador ha llegado a cero
        if (currentHealth <= 0)
        {
            // Aqu� podr�as ejecutar alguna l�gica cuando el jugador muera
            Debug.Log("�El jugador ha muerto!");
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("FinScene");
        }
    }
}
