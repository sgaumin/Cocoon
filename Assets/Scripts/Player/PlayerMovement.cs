using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private PlayerInputs playerInputs;
    private PlayerAbilities abilities;
    private Vector3 forward, right;
    private float inputHorizontal;
    private float inputVertical;
    private Vector3 rightMovement, upMovement;

    void Start()
    {
        abilities = GetComponent<PlayerAbilities>();
        playerInputs = GetComponent<PlayerInputs>();
    }

    void Update()
    {
        // Retreive inputs
        inputHorizontal = playerInputs.MovementHorizontal;
        inputVertical = playerInputs.MovementVertical;

        forward = transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;

        rightMovement = right * moveSpeed * Time.deltaTime * inputHorizontal;
        upMovement = forward * moveSpeed * Time.deltaTime * inputVertical;

        if (PlayerController.gameMode == GameModes.House)
        {
            transform.position += rightMovement;
            transform.position += upMovement;
        }
    }
}

