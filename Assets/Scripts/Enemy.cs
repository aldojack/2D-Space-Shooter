using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float enemySpeed = 4f;

    private Player _player;

    // Start is called before the first frame update
    void Start()
    {
            _player =  GameObject.Find("Player").GetComponent<Player>();

    }

    // Update is called once per frame
    void Update()
    {
        EnemyMovement();
    }

    void EnemyMovement()
    {
        
        transform.Translate(Vector3.down * enemySpeed * Time.deltaTime);

        if (transform.position.y < -6)
        {
            float randomX = Random.Range(-6.5f, 6.5f);
            transform.position = new Vector3(randomX, 7, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            Destroy(gameObject);
            if (player != null)
            {
                player.Damage();
            }


        }

        if (other.tag == "Laser")
        {
            if (_player != null)
            {
                _player.AddScore();
            }
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
