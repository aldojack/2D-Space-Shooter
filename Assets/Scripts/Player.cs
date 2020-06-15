using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _playerSpeed = 3.5f;
    private float _horizontalInput;
    private float _verticalInput;
    [SerializeField]
    private int _playerLives = 3;

    private SpawnManager _spawnManager;

    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _laserRate = 0.5f;
    private float _laserCooldown = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = Vector3.zero;
        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        
        if (_spawnManager == null)
        {
            Debug.LogError("Spawn Manager is NULL. ");
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _laserCooldown)
        {
            ShootLaser();
        }

    }

    void PlayerMovement()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(_horizontalInput, _verticalInput, 0);

        transform.Translate(direction * _playerSpeed * Time.deltaTime);

        //Clamps Y movement between -3.8 and 0
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        if (transform.position.x <= -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }

        else if (transform.position.x >= 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }

    }

    void ShootLaser()
    {
        _laserCooldown = Time.time + _laserRate;
        Instantiate(_laserPrefab,transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
    }

    public void Damage()
    {
        _playerLives--;

        if (_playerLives <=0)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(gameObject);
        }
    }
}
