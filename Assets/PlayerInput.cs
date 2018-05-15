using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    private Vector3 targetPoint;
    private bool isSeekingPosition = false;

    public float moveSpeed = 5.0f;

	// Use this for initialization
	void Start () {
        targetPoint = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        if (GlobalInput.GetMouseButtonDown(0))
        {
            RaycastHit info;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(GlobalInput.mousePosition), out info, 100))
            {
                isSeekingPosition = true;
                targetPoint = info.point;
                targetPoint.y = transform.position.y;
            }

        }

        if (isSeekingPosition)
        {
            transform.LookAt(targetPoint, Vector3.up);
            float moveMagnitude = Time.deltaTime * moveSpeed;
            if (Vector3.Distance(transform.position, targetPoint) <= moveMagnitude)
            {
                transform.position = targetPoint;
                isSeekingPosition = false;
            }
            else
            {
                Vector3 moveDirection = targetPoint - transform.position;
                transform.position += moveDirection.normalized * moveMagnitude;
            }
        }

    }
}
