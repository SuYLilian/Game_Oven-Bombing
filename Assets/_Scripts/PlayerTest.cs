using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed;
    public Vector3 playerFix;
    private void Start()
    {
        playerFix = gameObject.transform.position;
    }
    void Update()
    {
        float horizontalInput = 0f;
        float verticalInput = 0f;

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            horizontalInput -= 1f; // Move left
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            horizontalInput += 1f; // Move right
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            verticalInput += 1f; // Move forward
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            verticalInput -= 1f; // Move backward
        }

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);

        movement.Normalize();
        transform.position += movement * moveSpeed * Time.deltaTime;
      
        if(movement != Vector3.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation(movement, Vector3.up);

            gameObject.transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, newRotation, rotationSpeed);

        }
    }
}
