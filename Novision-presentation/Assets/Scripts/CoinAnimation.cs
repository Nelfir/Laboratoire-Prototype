using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAnimation : MonoBehaviour
{
	public float speed = 1f;
	public float height = 0.5f;	
	Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        pos =transform.position;
    }

    // Update is called once per frame
    void Update()
    {
    float newY = Mathf.Sin(Time.time * speed) * height + pos.y;
	transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
