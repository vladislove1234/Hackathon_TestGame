using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var vec = Vector3.Lerp(transform.position, player.transform.position, speed);
        transform.position = new Vector3(vec.x, vec.y, transform.position.z);
    }
}
