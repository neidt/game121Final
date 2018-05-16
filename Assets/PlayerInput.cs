using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    public float moveSpeed = 5.0f;
    public Transform player;
    public float radiusOfSat = .05f;
    private Vector3 target;
    public float rayDist = 1f;
    public LayerMask floorOnly;
    private bool hitThisFrame = false;
    private Vector3 hitLocThisFrame = Vector3.zero;
    public int pickupCount = 4;
    public Vector3 offsetVec = new Vector3(0, 1, 0);
    public Vector3 rotatePoint;
    public Camera cam;
    public Camera sceneCam;
    public GameObject movingObst;
    // Use this for initialization
    void Start()
    {
        cam.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hitInfo;
        float step = moveSpeed * Time.deltaTime;
        Ray intoScreen = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(intoScreen, out hitInfo, 1000, floorOnly))
            {
                target = hitInfo.point;
                player.LookAt(target + offsetVec);
            }
        }

        if (Physics.Raycast(intoScreen, out hitInfo, 1000, floorOnly))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {

            }
        }

            if (Vector3.Distance(player.position, target) > radiusOfSat)
        {
            transform.position = Vector3.MoveTowards(player.transform.position, target + offsetVec, step);
        }

        if(pickupCount == 0)
        {
            sceneCam.enabled = false;
            cam.enabled = true;
            cam.transform.Rotate(Vector3.up, 1f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "pickup")
        {
            other.gameObject.SetActive(false);
            pickupCount--;
        }
    }
}
