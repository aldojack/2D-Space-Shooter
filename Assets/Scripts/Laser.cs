using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    // Start is called before the first frame update
    private float _laserSpeed = 8.0f;

    // Update is called once per frame
    void Update()
    {
        ShootAndDestroy();
    }

    void ShootAndDestroy()
    {
        transform.Translate(Vector3.up * _laserSpeed * Time.deltaTime);

        if (transform.position.y > 7)
        {
            Destroy(gameObject);
        }
    }
}
