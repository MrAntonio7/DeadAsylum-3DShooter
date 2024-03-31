using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeapon : MonoBehaviour
{
    public GameObject[] arma;
    public GameObject[] CanvasArmas;

    // Start is called before the first frame update
    void Start()
    {
        ActivarArma(0);
    }

    // Update is called once per frame
    void Update()
    {
        CambiarArma();    
    }

    public void CambiarArma()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ActivarArma(0);
            
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ActivarArma(1);
        }
    }

    public void ActivarArma(int numArma)
    {
        for (int i = 0; i < arma.Length; i++)
        {
            arma[i].SetActive(false);
        }
        arma[numArma].SetActive(true);

        for (int i = 0; i < CanvasArmas.Length; i++)
        {
            CanvasArmas[i].SetActive(false);
        }
        CanvasArmas[numArma].SetActive(true);

        GameManager.instance.tipoDeArma = numArma + 1;
    }
}
