using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float speed = 3.0f;


    [SerializeField]
    private int _powerupID; // 0 = Triple Shot 1 = Speed 2 = Shield

    // Update is called once per frame
    void Update()
    {

        PowerUpMove();
    }

    void PowerUpMove()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y < -5.6)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag =="Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {

            switch (_powerupID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedBoostActive();
                        break;
                    case 2:
                        Debug.Log("Shield power up collected");
                        break;
                    default:
                        Debug.Log("Default value");
                        break;
                }
            }

            Destroy(gameObject);
        }
    }

}

