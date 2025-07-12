using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // ref to the player, the vehicle
    public GameObject player;
    public float damping = 1;

    //offset camera
    public Vector3 offset = new Vector3(-0.3f, 0.32f, -0.06f);

    // Update is called once per frame after the update method : use to slooth the camera movement behind the vehicle
    void LateUpdate()
    {
        float angleY = player.transform.eulerAngles.y;
        transform.rotation = Quaternion.Euler(0,angleY,0);
        transform.position = player.transform.position + offset;
    }
}
