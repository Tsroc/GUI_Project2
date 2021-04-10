using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/*
 * This class is used for interactions between the GrammarContoller and other classes.
 */
public class GrammarAdaptor : MonoBehaviour
{
    private readonly float INTERACTION_DISTANCE = 2.0f;

    [SerializeField] private List<GameObject> teleportList;
    [SerializeField] private Button nextLevelButton;
    private SceneController sc;
    private GameObject player;
    private GameObject portal;

    // Start is called before the first frame update
    void Start()
    {
        sc = GameObject.FindObjectOfType<SceneController>();
        player = GameObject.FindGameObjectWithTag("Player");
        portal = GameObject.FindGameObjectWithTag("Portal");
    }

    /*
     * Returns the player object
     */
    public GameObject GetPlayer()
    {
        return player;
    }

    /*
     * Player interactions
     */
    public void PlayerInteractions(string action)
    {
        if(action == "portal")
        {
            bool found = false;

            // Check the portal first
            if(GetDistance(portal) < INTERACTION_DISTANCE)
            {
                found = true;
                player.GetComponent<Player>().InteractWithPortal(portal);
            }
            if (!found)
            {
                // Check teleport
                foreach (GameObject teleport in teleportList)
                {
                    if (GetDistance(teleport) < INTERACTION_DISTANCE)
                    {
                        found = true;
                        player.GetComponent<Player>().InteractWithTeleport(teleport);
                    }
                }
            }


            if (!found)
            {
                Debug.Log("You are not close enough!");
            }
        }
    }

    public void PlayerAction(string action, string direction)
    {
        switch (action)
        {
            case "jump":
                player.GetComponent<Player>().Jump(direction);
                break;
            case "dash":
                player.GetComponent<Player>().Dash(direction);
                break;
            case "step":
                player.GetComponent<Player>().Step(direction);
                break;
            case "move":
                player.GetComponent<Player>().Move(direction);
                break;
        }
    }

    public void StopMovement()
    {
        player.GetComponent<Player>().ClearDestination();
    }

    private float GetDistance(GameObject destination)
    {
        return Vector3.Distance(player.transform.position, destination.transform.position);
    }

    public bool IsLevelComplete()
    {
        return player.GetComponent<Player>().IsLevelComplete();

    }

    public void NextLevel()
    {
        nextLevelButton.onClick.Invoke();
    }

}
