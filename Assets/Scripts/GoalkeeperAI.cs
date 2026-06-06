using UnityEngine;

public class GoalkeeperAI : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float moveRange = 2f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float y =
            startPos.y +
            Mathf.Sin(Time.time * moveSpeed) * moveRange;

        transform.position =
            new Vector3(startPos.x, y, startPos.z);
    }
}