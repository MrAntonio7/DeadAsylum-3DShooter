using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity = new Vector3(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            
            //Debug.Log("Bala y enemigo");
            collision.gameObject.GetComponent<EnemyController>().QuitarVida();
            Destroy(gameObject);
        }
    }
}
