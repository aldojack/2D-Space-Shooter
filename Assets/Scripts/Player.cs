using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //Player
    [SerializeField]
    private float _playerSpeed = 3.5f;
    [SerializeField]
    private float _speedMultiplyer = 2;
    private float _horizontalInput;
    private float _verticalInput;
    [SerializeField]
    private int _playerLives = 3;

    //Laser

    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleLaserPrefab;

    private bool _tripleShotActive = false;
    private bool _speedBoostActive = false;
    private bool _shieldActive = false;

    [SerializeField]
    private GameObject _playerShield;

    [SerializeField]
    private float _laserRate = 0.5f;
    private float _laserCooldown = 0.0f;

    //Other
    private SpawnManager _spawnManager;
    private UiManager _uiManager;
    private Reload_Scene _sceneManager;

    [SerializeField]
    private int _score = 0;



    // Start is called before the first frame update
    void Start()
    {
        transform.position = Vector3.zero;
        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UiManager>();
        _sceneManager = GameObject.Find("SceneManager").GetComponent<Reload_Scene>();



        if (_uiManager == null)
        {
            Debug.LogError("UI Manager is NULL. ");

        }
        if (_spawnManager == null)
        {
            Debug.LogError("Spawn Manager is NULL. ");
        }

        if (_sceneManager == null)
        {
            Debug.LogError("Scene Manager is NULL. ");
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

        //if (Input.GetKeyDown(KeyCode.R) && _playerLives == 0)
        //{
        //    _sceneManager.RestartLevel();
        //}

    }

    void PlayerMovement()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(_horizontalInput, _verticalInput, 0);

            transform.Translate(direction * _playerSpeed * Time.deltaTime);

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

        if (_tripleShotActive == true)
        {
            Instantiate(_tripleLaserPrefab, transform.position, Quaternion.identity);
        }

        else
            { 
                Instantiate(_laserPrefab,transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
            }
    }

    public void Damage()
    {

        if (_shieldActive == true)
        {
            _shieldActive = false;
            _playerShield.SetActive(false);
            return;
        }
        _playerLives--;
        _uiManager.UpdateLives(_playerLives);

        if (_playerLives == 0) 
        {
            _spawnManager.OnPlayerDeath();
            _sceneManager.RestartLevel(true);
            Destroy(gameObject);
        }
    }

    public void TripleShotActive()
    {
        _tripleShotActive = true;
        StartCoroutine(TripleShotCooldown());
    }

    IEnumerator TripleShotCooldown()
    {
        yield return new WaitForSeconds(5.0f);
        _tripleShotActive = false;
    }

    public void SpeedBoostActive()
    {
        _speedBoostActive = true;
        _playerSpeed *= _speedMultiplyer;
        StartCoroutine(SpeedCooldown());
    }

    IEnumerator SpeedCooldown()
    {
        yield return new WaitForSeconds(5.0f);
        _playerSpeed /= _speedMultiplyer;
        _speedBoostActive = false;
    }

    public void ShieldActive()
    {
        _shieldActive = true;
        _playerShield.SetActive(true);
    }

    public void AddScore()
    {
        _score += 10;
        _uiManager.UpdateScore(_score);
    }

}
