using UnityEngine;

public class PortalTeleporter : MonoBehaviour {

    public Transform player;
    public Transform reciever;

    private bool playerIsOverlapping = false;

    // Update is called once per frame
    void Update () {
        if (playerIsOverlapping)
        {
            Vector3 portalToPlayer = player.position - transform.position;
            float dotProduct = Vector3.Dot(transform.up, portalToPlayer);
            
            // If this is true then the player has moved across the portal
            if (dotProduct < 0f)
            {
                Debug.Log("teleporting!");
                
                player.GetComponent<PlayerController>().PauseController();

                // Rotation
                float rotationDiff = -Quaternion.Angle(transform.rotation, reciever.rotation);
                rotationDiff += 180;
                player.Rotate(Vector3.up, rotationDiff);

                // Movement
                Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
                player.position = reciever.position + positionOffset;

                if (this.tag == "Grass Portal")
                {
                    player.GetComponent<PlayerController>().SetNotOnWater();
                }
                else if (this.tag == "Water Portal")
                {
                    player.GetComponent<PlayerController>().SetOnWater();
                }

                playerIsOverlapping = false;
            }
        }
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.tag == "Player")
        {
            playerIsOverlapping = true;
        }
    }

    void OnTriggerExit (Collider other)
    {
        if (other.tag == "Player")
        {
            playerIsOverlapping = false;
        }
    }
}