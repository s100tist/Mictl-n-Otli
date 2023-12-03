using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        // Move player left and right
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(10 * horizontalInput * Time.deltaTime * Vector3.right);
    }
}
