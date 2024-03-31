using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairMove : MonoBehaviour
{
    public Transform jugador; // Referencia al transform del jugador
    public GameObject chair;
    public float velocidad = 5f; // Velocidad a la que avanza el objeto
    public bool moviendo = false;
    private bool primeraVez  = true;
    public float tiempo = 0.3f;


    void Start()
    {

        
    }

    void FixedUpdate()
    {
        if (moviendo)
        {
            Vector3 nuevaPosicion = chair.transform.position + -chair.transform.forward * velocidad * Time.deltaTime;
            chair.transform.position = nuevaPosicion;
            
            Invoke("DesactivarMovimiento",tiempo);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && primeraVez)
        {
            moviendo = true;
            primeraVez = false;
            chair.GetComponent<AudioSource>().Play();
        }
        
    }


    public void DesactivarMovimiento()
    {
        moviendo = false;
    }
}
