using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    [SerializeField]
    float xConstraint;
    [SerializeField]
    float yConstraint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float deltaX = player.transform.position.x - gameObject.transform.position.x;
        float deltaY = player.transform.position.y - gameObject.transform.position.y;
        deltaX *= 0.1f;
        deltaY *= 0.1f;
        gameObject.transform.position += new Vector3(deltaX, deltaY, 0);
        if (gameObject.transform.position.x > xConstraint || gameObject.transform.position.x < -xConstraint)
        {
            gameObject.transform.position -= new Vector3(deltaX, 0, 0);
        }

        if (gameObject.transform.position.y > yConstraint || gameObject.transform.position.y < -yConstraint)
        {
            gameObject.transform.position -= new Vector3(0, deltaY, 0);
        }
    }
}
