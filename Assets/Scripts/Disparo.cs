using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Disparo : MonoBehaviour
{

    public Transform spawnPoint; //PuntoSalida
    public Transform spawnPointRotation; //PuntoSalidaRotacion
    public GameObject bullet;
    public GameObject effect;

    public float shotForce = 400;
    public float shotRate = 0.1f; 
    private float shotRateTime = 0;

    //private AudioSource shotSource;
    //public AudioClip shotClip;
    private bool sePuedeRecargar=true;

    public TextMeshProUGUI textAmmo1;
    public TextMeshProUGUI textAmmo2;
    public ParticleSystem[] shootEffect;

    public GameObject arma1;
    public GameObject arma2;
    public GameObject soundReload1;
    public GameObject soundReload2;

    public bool soundPlayed = false;

    private void Awake()
    {
        //shotSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        textAmmo1.text = GameManager.instance.gunAmmo1.ToString();
        textAmmo2.text = GameManager.instance.gunAmmo2.ToString();
        for (int i = 0; i < shootEffect.Length; i++)
        {
            shootEffect[i].Stop();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        // Restablece la bandera cuando se suelta la tecla 'R'
        if (Input.GetKeyUp(KeyCode.R))
        {
            soundPlayed = false;
        }

        if (Input.GetButton("Fire1"))
        {

            //if (GameManager.instance.tipoDeArma == 1 && GameManager.instance.gunAmmo <1)
            //{
            //    return;
            //}
            //if (GameManager.instance.tipoDeArma == 2 && GameManager.instance.granadas < 1)
            //{
            //    return;
            //}
            if (Time.time > shotRateTime && GameManager.instance.gunAmmo1 > 0 && GameManager.instance.tipoDeArma == 1)
            {
                GameManager.instance.gunAmmo1--;
                textAmmo1.text = GameManager.instance.gunAmmo1.ToString();
                GameObject newBullet = Instantiate(bullet, spawnPoint.position, spawnPointRotation.rotation);
                //GameObject newEffect = Instantiate(effect, spawnPoint.position, spawnPointRotation.rotation);
                newBullet.GetComponent<Rigidbody>().AddForce(spawnPoint.forward*shotForce*Time.deltaTime, ForceMode.Impulse);
                //shotSource.PlayOneShot(shotClip);
                shotRateTime = Time.time + shotRate;
                arma1.GetComponent<AudioShoot>().Shoot();
                for (int i = 0; i < shootEffect.Length; i++)
                {
                    shootEffect[i].Play();
                }
                Destroy (newBullet, 3);
                //Destroy (newEffect, 3);
        }
            if (Time.time > shotRateTime && GameManager.instance.gunAmmo2 > 0 && GameManager.instance.tipoDeArma == 2)
            {
                GameManager.instance.gunAmmo2--;
                textAmmo2.text = GameManager.instance.gunAmmo2.ToString();
                GameObject newBullet = Instantiate(bullet, spawnPoint.position, spawnPointRotation.rotation);
                //GameObject newEffect = Instantiate(effect, spawnPoint.position, spawnPointRotation.rotation);
                newBullet.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * shotForce * Time.deltaTime, ForceMode.Impulse);
                //shotSource.PlayOneShot(shotClip);
                shotRateTime = Time.time + shotRate;
                arma2.GetComponent<AudioShoot>().Shoot();
                for (int i = 0; i < shootEffect.Length; i++)
                {
                    shootEffect[i].Play();
                }
                Destroy(newBullet, 3);
                //Destroy (newEffect, 3);
            }
            
        }

        if (Input.GetKey("r") && sePuedeRecargar && GameManager.instance.tipoDeArma == 1)
        {

            // Verifica si se presiona la tecla 'R' y si el sonido aún no se ha reproducido
            if (Input.GetKeyDown(KeyCode.R) && !soundPlayed)
            {
                // Reproduce el sonido y establece la bandera en true para indicar que ya se ha reproducido
                soundReload1.GetComponent<SoundReload>().Reload();
                soundPlayed = true;
            }


            sePuedeRecargar = false;
            textAmmo1.text = GameManager.instance.gunAmmo1.ToString();
            //Quaternion rotacionOriginal = GameObject.FindGameObjectWithTag("Arma").transform.rotation;
            //GameObject.FindGameObjectWithTag("Arma").transform.Rotate(Vector3.down, 5*Time.deltaTime);
            StartCoroutine(EsperarSeg(2));
            GameManager.instance.gunAmmo1 = 25;
            //soundReload1.GetComponent<SoundReload>().Reload();
            //GameObject.FindGameObjectWithTag("Arma").transform.rotation = rotacionOriginal;
            sePuedeRecargar = true;

        }
        if (Input.GetKey("r") && sePuedeRecargar && GameManager.instance.tipoDeArma == 2)
        {
            // Verifica si se presiona la tecla 'R' y si el sonido aún no se ha reproducido
            if (Input.GetKeyDown(KeyCode.R) && !soundPlayed)
            {
                // Reproduce el sonido y establece la bandera en true para indicar que ya se ha reproducido
                soundReload2.GetComponent<SoundReload>().Reload();
                soundPlayed = true;
            }
            sePuedeRecargar = false;
            textAmmo2.text = GameManager.instance.gunAmmo2.ToString();
            //Quaternion rotacionOriginal = GameObject.FindGameObjectWithTag("Arma").transform.rotation;
            //GameObject.FindGameObjectWithTag("Arma").transform.Rotate(Vector3.down, 5*Time.deltaTime);
            StartCoroutine(EsperarSeg(2));
            GameManager.instance.gunAmmo2 = 30;
            soundReload2.GetComponent<SoundReload>().Reload();
            //GameObject.FindGameObjectWithTag("Arma").transform.rotation = rotacionOriginal;
            sePuedeRecargar = true;

        }

    }

    IEnumerator EsperarSeg(int s)
    {                                                                                        
        yield return new WaitForSeconds(s);
    }
}
