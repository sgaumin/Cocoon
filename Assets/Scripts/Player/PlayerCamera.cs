using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float speedVertical;
    public float speedHorizontal;
    public float maxAngleHorizontal;
    public float maxAngleVertical;

    private Camera cam;
    private PlayerInputs playerInputs;
    private float xRot = 0f;
    private float yRot = 0f;
    private float lookX;
    private float lookY;

    void Start()
    {
        playerInputs = GetComponent<PlayerInputs>();
        cam = GetComponentInChildren<Camera>();
    }

    void Update()
    {
        // Retreive inputs
        lookX = playerInputs.CameraVertical * speedVertical;
        lookY = playerInputs.CameraHorizontal * speedHorizontal;

        Vector3 rotVerti = new Vector3(-lookX, 0, 0);

        if (GameSystem.instance.gameState == GameSystem.gameStates.Playing)
        {
            // Rotation Verticale
            xRot -= lookX;
            xRot = Mathf.Clamp(xRot, -maxAngleVertical, maxAngleVertical);

            Vector3 eulerAngles = cam.transform.eulerAngles;
            cam.transform.eulerAngles = new Vector3(xRot, eulerAngles.y, eulerAngles.z);

            // Rotation Horizontale
            Vector3 rotHori = new Vector3(0, -lookY, 0);

            // Conditions on scenes
            if (PlayerController.gameMode == GameModes.House)
                transform.Rotate(rotHori);

            if (PlayerController.gameMode == GameModes.Hideout)
            {
                yRot -= lookY;
                yRot = Mathf.Clamp(yRot, 180 - maxAngleHorizontal, 180 + maxAngleHorizontal);
                Vector3 eulerAnglesBis = transform.eulerAngles;
                transform.eulerAngles = new Vector3(eulerAnglesBis.x, yRot, eulerAnglesBis.z);
            }
        }
    }
}
