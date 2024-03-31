using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LifeBar : MonoBehaviour
{
    public Slider lifeBar;
    private float maxLife;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        maxLife = player.GetComponent<PlayerController>().currentHealth;   
    }

    // Update is called once per frame
    void Update()
    {
        lifeBar.value = player.GetComponent<PlayerController>().currentHealth;
    }
 
}
