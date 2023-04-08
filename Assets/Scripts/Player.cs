using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController character;
    private Vector3 direction;

    public float gravity = 9.81f * 2f;
    public float jumpForce = 8f;


    private void Awake()
    {
        character= GetComponent<CharacterController>();
    }

    //We need this function to avoid movement in game over (script disabled)
    private void OnEnable()
    {
        direction = Vector3.zero;
    }

    private void Update()
    {
        direction += Vector3.down * gravity * Time.deltaTime;

        if (character.isGrounded)
        {
            direction = Vector3.down;

            if (Input.GetButton("Jump"))
            {
                direction= Vector3.up * jumpForce;
            }             
        }

        character.Move(direction * Time.deltaTime);
    }
}
