using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _playerSpeed = 20.0f;
    private float _horizontalInput;
    private float _verticalInput;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(_horizontalInput, _verticalInput, 0);

        //Allows player to move
        transform.Translate(direction * _playerSpeed * Time.deltaTime);


        //Player can exit either side to wrap around
        if (transform.position.x <= -11.30f)
        {
            transform.position = new Vector3(11.30f, transform.position.y, 0);
        }

        else if (transform.position.x >= 11.38f)
        {
            transform.position = new Vector3(-11.31f, transform.position.y, 0);
        }

        //Stops player leaving top or bottom of screen
        if (transform.position.y >= 5.7f)
        {
            transform.position = new Vector3(transform.position.x, 5.7f, 0);
        }

        else if (transform.position.y <= -3.8f)
        {
            transform.position = new Vector3(transform.position.x, -3.8f, 0);
        }
    }
}
