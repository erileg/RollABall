using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    private float speed;

    void Start()
    {
        speed = Random.Range(2.0f, 2.5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime * speed);
		var oldPostion = transform.localPosition;
		transform.localPosition = new Vector3(oldPostion.x, Mathf.Sin(Time.time * speed) + 1.5f, oldPostion.z);
    }
}
