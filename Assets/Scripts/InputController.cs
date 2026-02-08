using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.UI.Image;

public class InputController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    #region Properties
    //References to shoot and aim inputs
    public bool FirePressed { get; private set; }
    public Vector2 AimPosition { get; private set; }
    #endregion

    #region Fields
    // Reference to the player's Movoment component
    private Rigidbody2D player;

    [SerializeField]
    private float speed = 5f;

    [Header("Aim / Raycast")]
    [SerializeField]
    private LayerMask aimLayerMask = ~0; // all layers by default

    [SerializeField]
    private float aimRayDistance = 50f;

    [SerializeField]
    private bool debugAim = true;
    #endregion
    #region Unity Callbacks
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        AIMInput();
    }
    #endregion
    #region Private Methods

    void PlayerMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        player.linearVelocity = new Vector2(moveX * speed, moveY * speed);

    }
    void AIMInput()
    {
        // Update FirePressed based on input
        FirePressed = Input.GetButtonDown("Fire1");
        if (Camera.main == null) return;
        // Update AimPosition based on mouse position
        Vector3 mouseScreen = Input.mousePosition;
        mouseScreen.z = Mathf.Abs(Camera.main.transform.position.z);

        // Convertir a mundo (3D)
        Vector3 mouseWorld3 = Camera.main.ScreenToWorldPoint(mouseScreen);
        // Para 2D nos interesa sólo x,y
        Vector2 mouseWorld = new Vector2(mouseWorld3.x, mouseWorld3.y);

        // Raycast 2D desde el jugador hacia la posición del ratón
        Vector2 origin = transform.position;
        Vector2 direction = (mouseWorld - origin).normalized;

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, aimRayDistance, aimLayerMask);
        if (hit.collider != null && hit.collider.gameObject.tag!="Player")
        {
            AimPosition = hit.point;
        }
        else
        {
            // Si no hay impacto, apunta al ratón (o al máximo de la distancia del rayo)
            float distToMouse = Vector2.Distance(origin, mouseWorld);
            AimPosition = distToMouse <= aimRayDistance ? mouseWorld : origin + direction * aimRayDistance;
        }

        if (debugAim)
        {
            Debug.DrawLine(origin, AimPosition, Color.green);
            Debug.DrawRay(origin, direction * Mathf.Min(aimRayDistance, Vector2.Distance(origin, AimPosition)), Color.cyan);
        }
    }
    #endregion
}
