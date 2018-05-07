using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    private float speed;

    void Start()
    {
        speed = Random.Range(1.0f, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime * speed);
    }
}
