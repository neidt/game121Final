using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    public LayerMask movingObst;
    public float moveSpeed = 3.0f;
    public Transform start;
    public Transform stop;
    public bool isAtStart = false;
    public bool isAtStop = false;
    // Use this for initialization
    void Start()
    {
        isAtStart = true;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hitInfo;
        Ray intoScreen = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (isAtStart)
        {
            transform.position += transform.right * moveSpeed * Time.deltaTime;
        }
        else if (isAtStop)
        {
            transform.position += -transform.right * moveSpeed * Time.deltaTime;
        }

        if (Physics.Raycast(intoScreen, out hitInfo, 1000, movingObst))
        {
            if (Input.GetKeyDown(KeyCode.Space) && isAtStart)
            {
                isAtStart = false;
                isAtStop = true;
            }
            else if(Input.GetKeyDown(KeyCode.Space) && isAtStop)
            {
                isAtStop = false;
                isAtStart = true;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().transform == start)
        {
            isAtStop = false;
            isAtStart = true;
            
        }
        else if (other.GetComponent<Collider>().transform == stop)
        {
            isAtStart = false;
            isAtStop = true;
        }
    }
}
