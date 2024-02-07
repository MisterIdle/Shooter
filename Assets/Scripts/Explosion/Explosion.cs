using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float explosionDuration;

    private void Start()
    {
        Destroy(gameObject, explosionDuration);
    }
}
