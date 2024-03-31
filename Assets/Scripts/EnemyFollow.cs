using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public GameObject enemy;
    private CapsuleCollider follow;
    // Start is called before the first frame update
    void Start()
    {
        follow = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemy.GetComponent<EnemyController>().follow = true;
            enemy.GetComponent<EnemyController>().anim.SetBool("walking", true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemy.GetComponent<EnemyController>().follow = false;
            enemy.GetComponent<EnemyController>().anim.SetBool("walking", false);
        }
    }
}
