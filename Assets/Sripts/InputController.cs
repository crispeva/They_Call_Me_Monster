using UnityEngine;

public class InputController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    #region Properties
    #endregion

    #region Fields
    private Rigidbody2D player;
    private float speed = 5f;
    #endregion
    #region Unity Callbacks
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovoment();
    }
    #endregion
    #region Private Methods

    void PlayerMovoment()
    {
        //player.Translate(Vector3.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime);
        //player.Translate(Vector3.up * Input.GetAxis("Vertical") * speed * Time.deltaTime);
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        player.linearVelocity = new Vector2(moveX * speed, moveY * speed);

    }
    #endregion
}
