using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float enemySpeed = 4f;

    private Player _player;
    [SerializeField]

    private Animator _anim;
    private AudioSource _explosionSound;

    // Start is called before the first frame update
    void Start()
    {
        _player =  GameObject.Find("Player").GetComponent<Player>();
        _explosionSound = GameObject.Find("Explosion").GetComponent<AudioSource>();

        if (_player == null)
        {
            Debug.LogError("Player is NULL");
        }

        _anim = GetComponent<Animator>();
         
        if (_anim == null)
        {
            Debug.LogError("Animator is NULL");

        }

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


            if (player != null)
            {
                //Play explosion sound
                _explosionSound.Play();
                player.Damage();
            }

            _anim.SetTrigger("OnEnemyDeath");
            enemySpeed = 0;
            Destroy(gameObject,2.30f);
        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);

            if (_player != null)
            {
                _player.AddScore();
            }

            //Play explosion sound
            _explosionSound.Play();
            _anim.SetTrigger("OnEnemyDeath");
            enemySpeed = 0;
            Destroy(gameObject, 2.30f);

        }
    }

}
