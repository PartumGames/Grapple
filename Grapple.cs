using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{

	//--this script assumes that it is a component of the "Player"(Unity FPS Player).
	//--it also assumes that there is a rigidbody component on this gameobject.

    public Camera cam;
    public float grappleDist;

    public bool doGrapple = false;

    public float speed;

    private Vector3 grapplePos;

    private Rigidbody myBody;


    private void Start()
    {
        myBody = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            DoGrappleRay();
        }

        if(doGrapple && Input.GetKeyDown(KeyCode.G))
        {
            doGrapple = false;
            myBody.isKinematic = false;
        }

        if (doGrapple)
        {
            GrappletoPos();
            CheckDist();
        }
    }



    private void GrappletoPos()
    {
        transform.position = Vector3.Lerp(transform.position, grapplePos, speed * Time.deltaTime);
    }

    private void CheckDist()
    {
        float dist = Vector3.Distance(transform.position, grapplePos);
		
        if (dist <= 1f)//--1f should should not be hard coded like this.
        {
            doGrapple = false;
            myBody.isKinematic = false;
        }
    }

    private void DoGrappleRay()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, grappleDist))
        {
            grapplePos = hit.point;
            doGrapple = true;
            myBody.isKinematic = true;
        }
    }
}
