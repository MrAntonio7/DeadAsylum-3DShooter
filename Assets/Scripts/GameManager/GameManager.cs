using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    //Singleton
    public static GameManager instance
    {
        get;
        private set;
    }
    public int gunAmmo1 = 25;
    public int gunAmmo2 = 30;
    public int vidas = 10;
    public int granadas = 10;
    public int tipoDeArma = 2; //1 escopeta, //2 granada
    public bool muerto = false;
    public int nZombies;
    public TextMeshProUGUI textNZombies;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textNZombies.text = nZombies.ToString();
        if (nZombies <= 0 )
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("FinScene");
        }
    }
}
