using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This scripts allows a pendulum to sling
 * I used my highschool physics knowledge to come to this solution.
 * The reason a ball on a string doesn't fall down is because the string(and object it's tied to) provide a force that negates the gravity (or basically any other forces).
 * I broke down the forces applied to the rigidbody into two components. One having the same direction as the 'string' the ball is tied to. The other is the remaining factor.
 */

public class Sling3 : MonoBehaviour {

    [SerializeField]
    private GameObject fulcrum;
    new private Rigidbody rigidbody;

    private Vector3 gravity = new Vector3(0, -9.3f, 0);
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddForce(new Vector3(6, 9, 0), ForceMode.Impulse);
        //rigidbody.useGravity = false;
    }
    /*
     * Magic math method
     * 
     * 
     */
	void FixedUpdate () {
        if(rigidbody.velocity != Vector3.zero)
        {
            Vector3 currentVelocity = rigidbody.velocity;
            Vector3 dist = rigidbody.position - fulcrum.transform.position;

            
            Vector3 currentVelocityN = currentVelocity.normalized;
            Vector3 distN = dist.normalized;

            float labda = Vector3.Dot(currentVelocityN, distN) / Vector3.Dot(currentVelocityN, currentVelocityN);
            //component 1 is the component which
            Vector3 component1 = dist * labda;
            Vector3 component2 = currentVelocity - component1;

            rigidbody.velocity = component2;
        }
        
	}

    private void Update()
    {
        Debug.DrawLine(fulcrum.transform.position, rigidbody.position, Color.red);
    }
}
