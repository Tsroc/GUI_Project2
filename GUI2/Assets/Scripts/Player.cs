using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask foregroundLayerMask;
    [SerializeField] private GameObject failureNotification;
    [SerializeField] private Text coinsCollectedText;
    private Rigidbody2D rigidbody;
    private BoxCollider2D boxCollider;
    private Animator anim;
    [SerializeField] private float movementSpeed = 5.0f;
    [SerializeField] private float jumpVelocity = 13.0f;
    [SerializeField] private List<GameObject> teleportList;
    private int direction;
    private bool hasDestination = false;
    private bool canMove = true;
    private int coinsCollected = 0;




    void Start()
    {
        rigidbody = transform.GetComponent<Rigidbody2D>();
        boxCollider = transform.GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (canMove)
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

    /*
     * === Keyboard movement: start ===
     */
    private void Jump(float jumpVelocity)
    {
        ClearDestination();
        rigidbody.velocity = Vector2.up * jumpVelocity;
    }

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

    public void ClearDestination()
    {
        hasDestination = false;
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

    public void Movement(string direction)
    {
        Debug.Log("Called");
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

    public void MovementStep(string direction)
    {
        ClearDestination();

        if(direction == "left")
        {
            rigidbody.velocity = new Vector3(-3f, 0, 0);
        }
        else if (direction == "right")
        {
            rigidbody.velocity = new Vector3(3f, 0, 0);
        }
    }
    /*
     * === Keyboard movement: end ===
     */

    private void setCanMove(bool b)
    {
        canMove = b;
        anim.SetInteger("direction", 0);
    }

    private bool IsGrounded()
    {
        // Currently allowing double jumps...
        RaycastHit2D raycast = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down * .1f, foregroundLayerMask);
        //Debug.Log(raycast.collider.tag);
        return raycast.collider != null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Coin")
        {
            Destroy(collision.gameObject);
            coinsCollected++;
            // Increase coin count.
            int newCoinCount = coinsCollected;// prevCoin++;
            coinsCollectedText.text = newCoinCount.ToString();

        }
        else if ((collision.tag == "Door Trigger"))
        {
            collision.gameObject.GetComponent<Door>().OpenDoor();
        }
        else if (collision.tag == "Lava")
        {
            Debug.Log("You died!");
            // Call end game menu
            setCanMove(false);
            failureNotification.gameObject.SetActive(true);
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.tag == "Door Trigger"))
        {
            collision.gameObject.GetComponent<Door>().CloseDoor();
        }
    }


    public void InteractWithPortal()
    {
        // Get distance from portal
        Vector3 portalPos = GameObject.FindGameObjectWithTag("Portal").transform.position;
        if (Vector3.Distance(gameObject.transform.position, portalPos) < 2)
        {

            Debug.Log("Moving on to the next level.");
            setCanMove(false);
            GameObject portal = GameObject.FindGameObjectWithTag("Portal");
            portal.GetComponent<Portal>().ActivatePortal();
        }
        else
        {
            Debug.Log("You are not close enough!");
        }
    }

    public void InteractWithTeleport()
    {
        bool found = false;
        foreach(GameObject tp in teleportList) {
            Vector3 tpFrom = tp.transform.position;

            if (Vector3.Distance(gameObject.transform.position, tpFrom) < 2)
            {
                found = true;

                Vector3 tpTo = tp.GetComponent<Teleport>().GetTeleportPos();
                Debug.Log(tpTo);
                // Wait x seconds
                gameObject.transform.position = tpTo;
                break;
            }
        }
        if (!found)
        {
            Debug.Log("You are not close enough!");
        }

    }



    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Block")
        {
            Debug.Log("BOUNCY BLOCK.");
            Jump(40.0f);
        }
        
    }
    */

}
