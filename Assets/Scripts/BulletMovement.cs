using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{

    [SerializeField] float bulletSpeed = 3f; // How fast the bullet should go
    [SerializeField] float timeBeforeDestruction = 3f; // Destroy the bullet once enough time passes
    void Start ()
    {
        StartCoroutine("DestroyBullet");
    }


    // Update is called once per frame
    void Update()
    {

        transform.Translate(new Vector3(0, bulletSpeed, 0) * Time.deltaTime);

    }


    IEnumerator DestroyBullet()
    {
        // Wait the set time before executing
        yield return new WaitForSeconds(timeBeforeDestruction);
        
        // Destroy after enough time passed
        Destroy(this.gameObject);


    }


}
