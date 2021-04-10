using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask foregroundLayerMask;
    [SerializeField] private GameObject failureNotification;
    [SerializeField] private Text coinsCollectedText;
    [SerializeField] private float movementSpeed = 5.0f;
    [SerializeField] private float jumpVelocity = 13.0f;
    private Rigidbody2D rigidbody;
    private BoxCollider2D boxCollider;
    private Animator anim;
    private int coinsCollected = 0;
    private int direction;
    private bool hasDestination = false;
    private bool isLevelCompleted = false;

    void Start()
    {
        rigidbody = transform.GetComponent<Rigidbody2D>();
        boxCollider = transform.GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isLevelCompleted)
        {
            if (hasDestination)
            {
                gameObject.transform.Translate(((this.movementSpeed/2)*direction) * Time.deltaTime, 0, 0);

                if  ((Input.GetAxisRaw("Horizontal") != 0)||(Input.GetKeyDown(KeyCode.Space)) ){
                    hasDestination = false;
                }
            }
            else if (Input.GetAxisRaw("Horizontal") == 1)
            {
                MoveRight();
            }
            else if (Input.GetAxisRaw("Horizontal") == -1)
            {
                MoveLeft();
            }
            else
            {
                anim.SetInteger("direction", 0);
            }
            if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
            {
                Jump(jumpVelocity);
            }
        }
    }

    public void ClearDestination()
    {
        hasDestination = false;
    }

    public bool IsLevelComplete()
    {
        return isLevelCompleted;
    }

    private void LevelComplete(bool b)
    {
        isLevelCompleted = b;
        anim.SetInteger("direction", 0);
    }

    private bool IsGrounded()
    {
        // Currently allowing double jumps...
        RaycastHit2D raycast = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down * .1f, foregroundLayerMask);
        //Debug.Log(raycast.collider.tag);
        return raycast.collider != null;
    }
    /*
     * === Voice command actions: start ===
     *
     */

    // Jump, string arg sets the direction of the jump. 
    public void Jump(string action)
    {
        ClearDestination();

        if (action == "left")
        {
            rigidbody.velocity = new Vector3(-2, 15, 0);
        }
        else if (action == "right")
        {
            rigidbody.velocity = new Vector3(2, 15, 0);
        }
        else if (action == "up")
        {
            rigidbody.velocity = Vector2.up * jumpVelocity;
        }
    }

    // Dash, string arg sets the direction of the jump. 
    public void Dash(string action)
    {
        ClearDestination();

        if (action == "left")
        {
            rigidbody.velocity = new Vector3(-25, 2, 0);
        }
        else if (action == "right")
        {
            rigidbody.velocity = new Vector3(25, 2, 0);
        }
        else if (action == "up")
        {
            rigidbody.velocity = Vector2.up * jumpVelocity;
        }
    }

    public void Move(string direction)
    {
        ClearDestination();
        if(direction == "left")
        {
            anim.SetInteger("direction", -1);
            this.direction = -1;

        }
        else if (direction == "right")
        {
            anim.SetInteger("direction", 1);
            this.direction = 1;
        }
        hasDestination = true;
    }

    public void Step(string direction)
    {
        ClearDestination();

        if(direction == "left")
        {
            rigidbody.velocity = new Vector3(-4f, 0, 0);
        }
        else if (direction == "right")
        {
            rigidbody.velocity = new Vector3(4f, 0, 0);
        }
    }

    /*
     * === Voice command actions: end ===
     *
     */

    /*
     * === Keyboard movement: start ===
     *
     */
    // Jump, float arg sets the upwards velocity of the jump.
    private void Jump(float jumpVelocity)
    {
        ClearDestination();
        rigidbody.velocity = Vector2.up * jumpVelocity;
    }

    private void MoveLeft()
    {
        ClearDestination();
        anim.SetInteger("direction", -1);
        gameObject.transform.Translate(-this.movementSpeed * Time.deltaTime, 0, 0);
    }

    private void MoveRight()
    {
        ClearDestination();
        anim.SetInteger("direction", 1);
        gameObject.transform.Translate(this.movementSpeed * Time.deltaTime, 0, 0);
    }

    /*
     * === Keyboard movement: end ===
     */

    /*
     * Player interactions
     */
    public void InteractWithPortal(GameObject portal)
    {
        ClearDestination();
        LevelComplete(true);
        // Wait x seconds
        portal.GetComponent<Portal>().ActivatePortal();
    }

    public void InteractWithTeleport(GameObject teleportFrom)
    {
        ClearDestination();
        Vector3 teleportTo = teleportFrom.GetComponent<Teleport>().GetTeleportPos();
        // Wait x seconds
        gameObject.transform.position = teleportTo;
    }

    /*
     * Triggers & collisions.
     */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Coin")
        {
            Destroy(collision.gameObject);
            coinsCollected++;
            coinsCollectedText.text = coinsCollected.ToString();

        }
        else if ((collision.tag == "Door Trigger"))
        {
            collision.gameObject.GetComponent<Door>().OpenDoor();
        }
        else if (collision.tag == "Lava")
        {
            Debug.Log("You died!");
            // Call end game menu
            LevelComplete(true);
            failureNotification.gameObject.SetActive(true);
        }
        else if (collision.tag == "Instruction")
        {
            collision.gameObject.GetComponent<Instruction>().Activate();
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.tag == "Door Trigger"))
        {
            collision.gameObject.GetComponent<Door>().CloseDoor();
        }
    }

}
