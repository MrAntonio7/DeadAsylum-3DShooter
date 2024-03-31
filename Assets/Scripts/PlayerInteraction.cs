using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public TextMeshProUGUI textGranada;
    public bool vulnerable = true;
    // Start is called before the first frame update
    void Start()
    {
        textGranada.text = GameManager.instance.granadas.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //void PerderVida()
    //{
    //    vulnerable = false;
    //    GameManager.instance.vidas -= 2;
    //    if (GameManager.instance.vidas <= 0)
    //    {
    //        Muerto();
    //    }
    //    else
    //    {
            
    //    }
    //}

    public void Muerto()
    {
        GameManager.instance.muerto = true;
        Transform transformacion = transform;
        float anguloX = -128f;
        transformacion.eulerAngles = new Vector3(anguloX, transformacion.eulerAngles.y, transformacion.eulerAngles.z);
    }
}
