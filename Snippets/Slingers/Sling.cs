using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This scripts allows to sling
 * By simply looking at where the object should move to. 
 * But it only uses a forward direction, 
 * it's impossible to sling sideways like an actual pendulum
 */
public class Sling : MonoBehaviour {

    [SerializeField]
    private GameObject fulcrum;
    new private Rigidbody rigidbody;

	void Start () {
        rigidbody = GetComponent<Rigidbody>();
	}

    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            rigidbody.AddForce(new Vector3(200, 0, 0));
        }
        Vector3 dist = fulcrum.transform.position - this.rigidbody.position;
        print(dist);
        Vector3 distN = dist.normalized;
        Debug.DrawRay(this.rigidbody.position, distN, Color.red);
        Vector3 right = Vector3.Cross(distN, Vector3.up);
        Debug.DrawRay(this.rigidbody.position, right * 2, Color.green );
        Vector3 forward = Vector3.Cross(right, distN).normalized;
        Debug.DrawRay(this.rigidbody.position, forward * 2, Color.blue);

        //forwardMagnitude
        float magnitude = rigidbody.velocity.magnitude;

        if(Vector3.Dot(forward, rigidbody.velocity) <= 0)
        {
            rigidbody.velocity = forward * magnitude * -1;
        }
        else
        {
            rigidbody.velocity = forward * magnitude;
        }
        
    }
    
}
