using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    public Animator animator;
    private CapsuleCollider hit;
    public GameObject player;
    public bool pegando;
    // Start is called before the first frame update
    void Start()
    {
        hit = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("hit", true);
            pegando = true;
            player.GetComponent<PlayerController>().currentHit = pegando;
            StartCoroutine(player.GetComponent<PlayerController>().TakeDamageOverTime());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("hit", false);
            pegando = false;
            player.GetComponent<PlayerController>().currentHit = pegando;
            StopCoroutine(player.GetComponent<PlayerController>().TakeDamageOverTime());
        }
    }
}
