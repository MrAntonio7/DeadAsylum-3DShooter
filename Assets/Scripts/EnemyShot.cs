using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    public GameObject enemyBullet;
    public Transform spawnBullet;
    public Transform playerPosition;
    public float bulletVelocity = 100;
    public float maxDistance = 100;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("ShootPlayer", 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShootPlayer()
    {
        if (playerPosition != null && CanSeeTarget())
        {
            transform.LookAt(playerPosition);
            Vector3 playerDirection = playerPosition.position - transform.position;
            GameObject newBullet = Instantiate(enemyBullet, spawnBullet.position, spawnBullet.rotation);
            newBullet.GetComponent<Rigidbody>().AddForce(playerDirection * bulletVelocity, ForceMode.Force);
            Invoke("ShootPlayer", 2);
        }
    }
    bool CanSeeTarget()
    {
        if(playerPosition == null)
        {
            return false;
        }
        Vector3 direccion = playerPosition.position - transform.position;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direccion, out hit, maxDistance))
        {
            if (hit.collider.transform == playerPosition)
            {
                return true;
            }
        }
        return false;
    }
}
