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


    [SerializeField]
    private GameObject[] _playerEngine = new GameObject[2];



    //Laser

    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleLaserPrefab;

    private bool _tripleShotActive = false;
    private bool _speedBoostActive = false;
    [SerializeField]
    private bool _shieldActive = false;

    [SerializeField]
    private GameObject _playerShield;

    [SerializeField]
    private float _laserRate = 0.5f;
    private float _laserCooldown = 0.0f;

    //Other
    private SpawnManager _spawnManager;
    private UiManager _uiManager;
    private GameManager _sceneManager;


    [SerializeField]
    private AudioClip _laserSound;
    [SerializeField]
    private AudioClip _explosionSound;

    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private int _score = 0;



    // Start is called before the first frame update
    void Start()
    {
        transform.position = Vector3.zero;
        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UiManager>();
        _sceneManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        _audioSource = GetComponent<AudioSource>();

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

        if (_audioSource == null)
        {
            Debug.LogError("Audio Source on player is NULL. ");
        }
        else
        {
            _audioSource.clip = _laserSound;
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

        _audioSource.Play();
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

        int randomEngine = Random.Range(0, 2);

        if (_playerLives == 2)
        {
            _playerEngine[randomEngine].SetActive(true);
        }
        
        else if (_playerLives == 1)
        {
            if(_playerEngine[0].activeSelf == true)
            {
                _playerEngine[1].SetActive(true);
            }

            else
            {
                _playerEngine[0].SetActive(true);
            }
        }

        _uiManager.UpdateLives(_playerLives);

        if (_playerLives == 0)
        {

            _spawnManager.OnPlayerDeath();
            _sceneManager.GameOver();
            Destroy(gameObject,2.3f);
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
