using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float speed = 3.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {        

        transform.Translate(Vector3.down * speed * Time.deltaTime);
        //When leave screen destory object
        if (transform.position.y <-5.6)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //OnTriggerCollison
        //Only collectable by the Player
        //On collected, destory

        if (other.tag =="Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.TripleShotActive();
            }

            Destroy(gameObject);
        }
    }

}
