using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform groundCheckTransform = null;
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private LayerMask playerMask;
    private bool jumpKeyWasPressed;
    private bool turnLeft, turnRight;
    private float speed;
    private int SuperJump;
    private Rigidbody rigidbodyComponent;

    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
        speed = 0.003f;
    }

    // Update is called once per frame
    void Update()
    {
        turnLeft = Input.GetKeyDown(KeyCode.A);
        turnRight = Input.GetKeyDown(KeyCode.D);

        if (turnLeft)
        {
            rigidbodyComponent.MovePosition(new Vector3(transform.position.x - 1 < -2 ? transform.position.x : transform.position.x - 1, transform.position.y, transform.position.z));
        }
        else if (turnRight)
        {
            rigidbodyComponent.MovePosition(transform.position.x + 1 > 2 ? transform.position : transform.position + Vector3.right);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpKeyWasPressed = true;
        }

        rigidbodyComponent.velocity = new Vector3(rigidbodyComponent.velocity.x, rigidbodyComponent.velocity.y, rigidbodyComponent.velocity.z + speed);
    }

    private void FixedUpdate()
    {
        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0)
        {
            return;
        }

        if (jumpKeyWasPressed)
        {
            int powerJump = 1;

            if (SuperJump > 0)
            {
                powerJump *= 2;
                SuperJump--;
            }

            rigidbodyComponent.AddForce(Vector3.up * 5 * powerJump, ForceMode.VelocityChange);
            jumpKeyWasPressed = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            rigidbodyComponent.transform.position = respawnPoint.position;
        }
        else if (other.gameObject.layer == 10)
        {
            rigidbodyComponent.transform.position = respawnPoint.position;
        }
        else if (other.gameObject.layer == 11)
        {
            SuperJump++;
            Destroy(other.gameObject);
        }
    }
}
