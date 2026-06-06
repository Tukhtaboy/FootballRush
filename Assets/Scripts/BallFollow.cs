using UnityEngine;

public class BallFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0.4f, 0f, 0f);

    void Update()
    {
        if (player != null)
        {
            transform.position = player.position + offset;
        }
    }
}