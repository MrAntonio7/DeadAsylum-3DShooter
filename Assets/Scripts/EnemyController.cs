using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public Transform jugador; // Referencia al transform del jugador
    public float velocidad = 5f; // Velocidad a la que seguirá el objeto al jugador
    public Animator anim;
    public bool follow = false;
    public int vida = 20;
    public GameObject prefab;
    public float tiempoAtaque = 1f; // Tiempo en segundos entre cada ataque
    private bool enemigoMuriendo = true;
    private Rigidbody rb;

    //private bool enemigoAtacando = false; // Indica si el enemigo está atacando actualmente
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

    }

    void Update()
    {
        if (follow)
        {
            // Verifica si el jugador está asignado
            if (jugador != null)
            {
                // Calcula la dirección hacia la posición del jugador
                Vector3 direccion = jugador.position - transform.position;
                direccion.y = 0; // Ignora la componente Y para mantener la altura del objeto constante
                direccion.Normalize(); // Normaliza la dirección para que tenga longitud 1

                // Calcula la rotación para que el objeto mire hacia el jugador
                Quaternion rotacionHaciaJugador = Quaternion.LookRotation(direccion);

                // Aplica la rotación al objeto, solo rotando en el eje Y
                transform.rotation = Quaternion.Euler(0f, rotacionHaciaJugador.eulerAngles.y, 0f);

                // Calcula la posición a la que se moverá el objeto en este frame
                Vector3 nuevaPosicion = transform.position + direccion * velocidad * Time.deltaTime;

                // Asigna la nueva posición al objeto
                transform.position = nuevaPosicion;
            }
        }
        if (vida <= 0)
        {
            StartCoroutine(ZombieMuerto(2));
        }
    }
    public void QuitarVida()
    {
        vida--;
        StartCoroutine("AnimacionDanio", 0.2);
        
    }
    
    public IEnumerator AnimacionDanio(int sec)
    {
        anim.SetBool("damage", true);
        yield return new WaitForSeconds(sec);
        anim.SetBool("damage", false);
    }
    public IEnumerator ZombieMuerto(int sec)
    {
        if (enemigoMuriendo)
        {
            anim.SetBool("dead", true);
            FreezeAllPosition();
            enemigoMuriendo = false;
            //anim.SetBool("dead", false);
            yield return new WaitForSeconds(sec);
            GameManager.instance.nZombies--;
            Destroy(gameObject);
        }

    }
    // Método para congelar todas las posiciones
    void FreezeAllPosition()
    {
        // Asegurarse de que el Rigidbody no sea nulo
        if (rb != null)
        {
            // Obtener las constraints actuales
            RigidbodyConstraints constraints = rb.constraints;

            // Activar el freeze position en todos los ejes
            constraints |= RigidbodyConstraints.FreezePositionX;
            constraints |= RigidbodyConstraints.FreezePositionY;
            constraints |= RigidbodyConstraints.FreezePositionZ;

            // Aplicar las nuevas constraints al Rigidbody
            rb.constraints = constraints;
        }
    }


}
