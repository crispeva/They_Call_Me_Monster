using System;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.UI.Image;

public class InputController : MonoBehaviour
{
    #region Properties
    public bool FirePressed { get; private set; }
    public Vector2 AimPosition { get; private set; }
    #endregion

    #region Fields
    private Rigidbody2D player;
    [SerializeField]
    private float speed = 5f;

    [Header("Aim / Raycast")]
    [SerializeField]
    private LayerMask aimLayerMask = ~0;

    [SerializeField]
    private float aimRayDistance = 50f;
    [SerializeField]
    private bool debugAim = true;

    public event Action<float> OnMovement;
    #endregion

    #region Unity Callbacks
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }

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
        
        float moveMagnitude = Mathf.Sqrt(moveX * moveX + moveY * moveY);
        OnMovement?.Invoke(moveMagnitude);
        
        player.linearVelocity = new Vector2(moveX * speed, moveY * speed);
        Flip(moveX);
    }

    void Flip(float moveX)
    {
        if (moveX < 0)
            transform.localScale = new Vector3((float)-0.8, (float)0.8, (float)0.8);
        else if (moveX > 0)
            transform.localScale = new Vector3((float)0.8, (float)0.8, (float)0.8);
    }

    void AIMInput()
    {
        FirePressed = Input.GetButtonDown("Fire1");
        if (Camera.main == null) return;

        Vector3 mouseScreen = Input.mousePosition;
        mouseScreen.z = Mathf.Abs(Camera.main.transform.position.z);

        Vector3 mouseWorld3 = Camera.main.ScreenToWorldPoint(mouseScreen);
        Vector2 mouseWorld = new Vector2(mouseWorld3.x, mouseWorld3.y);

        Vector2 origin = transform.position;
        Vector2 direction = (mouseWorld - origin).normalized;

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, aimRayDistance, aimLayerMask);
        if (hit.collider != null && hit.collider.gameObject.tag != "Player")
        {
            AimPosition = hit.point;
        }
        else
        {
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
