using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    private void OnEnable()
    {
        Destroy(gameObject,2.39f);
    }
}
