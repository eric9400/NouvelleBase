using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyfeed : MonoBehaviour
{
    public float DestroyTime = 4f;

    private void OnEnable()
    {
        Destroy(gameObject, DestroyTime);
    }
}
