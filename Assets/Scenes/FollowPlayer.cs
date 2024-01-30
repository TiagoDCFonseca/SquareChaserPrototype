using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    private Vector3 offSet = new Vector3(0, 6, -25);
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Player");
        if (player == null)
        {
            Debug.LogError("Player GameObject not found");
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(player != null)
        {
            transform.position = player.transform.position + offSet;
        }
    }
}
