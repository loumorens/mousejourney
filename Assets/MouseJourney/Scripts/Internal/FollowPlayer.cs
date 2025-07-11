using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // ref to the player, the vehicle
    public GameObject player;

    //offset camera
    public Vector3 offset = new Vector3(-0.3f, 0.32f, -0.06f);

    // Update is called once per frame after the update method : use to slooth the camera movement behind the vehicle
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
