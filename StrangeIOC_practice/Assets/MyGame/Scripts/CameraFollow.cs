using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target = null;
    Vector3 offset = Vector3.zero;
    // Use this for initialization
    void Start()
    {
        if (target == null)
        {
            Debug.Log("Attach a target player to the camera to follow");
        }
        offset = target.position - this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = target.position - offset;
    }
}
