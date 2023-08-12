using UnityEngine;

public enum Speeds { Slow = 0, Normal = 1, Fast = 2, Faster = 3,Fastest = 4 };
public enum Gamemodes { Cube = 0, Ship = 1};
public class PlayerMovement : MonoBehaviour
{

    public Speeds currentSpeed;
    public Gamemodes currentGamemode;
    float[] SpeedValues = { 8.6f, 10.4f, 12.96f, 15.6f, 19.27f };

    public Transform GroundCheckPosition;
    public float GroundCheckRadius;
    public LayerMask GroundMask;
    public Transform playerSprite;
    bool Jump;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Jump = false;
    }
    private void Update()
    {
        //Movement
        Vector3 movement = Vector3.right * SpeedValues[(int)currentSpeed] * Time.deltaTime;

        transform.Translate(movement);

        //Mode function fly or jump
        Invoke(currentGamemode.ToString(), 0);
    }

    private bool OnGround()
    {
        return Physics2D.OverlapBox(GroundCheckPosition.position, (Vector2.right * 1.1f) + (Vector2.up * GroundCheckRadius), 0, GroundMask);
    }

    void Ship()
    {

        playerSprite.rotation = Quaternion.Euler(0, 0, rb.velocity.y * 2.5f);
        if(Input.GetMouseButton(0))
        {
            rb.gravityScale = -4.314969f;
        }
        else
        {
            rb.gravityScale = 4.314969f;
        }
    }
    void Cube()
    {
        //Check if on ground
        if (OnGround())
        {
            Vector3 Rotation = playerSprite.rotation.eulerAngles;
            Rotation.z = Mathf.Round(Rotation.z / 90) * 90;
            playerSprite.rotation = Quaternion.Euler(Rotation);
            
            //Jump
            if (Input.GetMouseButton(0))
            {
                Jump = true;
            }
            if(Jump)
            {
                Jump = false;
                rb.velocity = Vector2.zero;
                rb.AddForce(Vector2.up * 26.6581f, ForceMode2D.Impulse);
            }
        }
        else
        {
            playerSprite.Rotate(Vector3.back, 452.4152186f * Time.deltaTime);

        }
    }
    public void ChangeByPortal(Gamemodes GameMode, Speeds Speed, int State)
    {
        switch(State) 
        {
            case 0:
                currentSpeed = Speed;
                break;
            case 1:
                currentGamemode = GameMode;
                break;

        }
    }
}
