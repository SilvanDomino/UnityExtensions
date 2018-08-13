using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This scripts allows a pendulum to sling
 * Creating an (inverse) sphere collider around the object
 * I got this idea from a blogpost, but that blogpost would do its own collision detection and resolving
 * In Unity that would've been a bit too difficult if you were to use Rigidbody for physics such as collisions 
 *      (You'd have to do the calculations right after the physicsloop, which isn't possible)
 * So I ended up with this hack/solution.
 * 
 * The inverse sphere is added in this folder.
 */
public class Sling2 : MonoBehaviour {

    [SerializeField]
    private GameObject fulcrum;
    new private Rigidbody rigidbody;
    [SerializeField]
    private GameObject sling;
    
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetSling();
        }
    }

    void SetSling()
    {
        float dist = Vector3.Distance(fulcrum.transform.position, rigidbody.position);
        sling.transform.position = fulcrum.transform.position;
        sling.transform.localScale = Vector3.one * (dist*2 + 1) * 50;
    }
}
