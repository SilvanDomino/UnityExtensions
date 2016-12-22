using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 0.7f;
    private Vector3 movement;

    [SerializeField]
    private float jumpPower = 200;
    [SerializeField]
    private bool canJump = true;
    [SerializeField]
    private float jumpMoveSpeedMod = 0.6f;
    private float jumpMSMod = 1;

    [SerializeField]
    private float cameraSpeed = 1.5f;
    private Vector3 forward;

    public bool grounded = true;
    
    new private Rigidbody rigidbody;

    new private Camera camera;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        camera = Camera.main;
    }
    
    void FixedUpdate()
    {
        forward = transform.forward;
        Movement();
        //should move to update
        Jump();
        grounded = GetGrounded();
    }

    private void Jump()
    {
        if (Input.GetAxis("Jump") > 0.5f && canJump && grounded)
        {
            canJump = false;
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z);
            rigidbody.AddForce(0, jumpPower, 0);
            StartCoroutine(JumpWait());
        }
    }
    IEnumerator JumpWait()
    {
        yield return new WaitForSeconds(0.5f);
        canJump = true;
    }
    private bool GetGrounded()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out hit, 1.1f))
        {
            return true;
        }
        return false;
    }
    
    private void Movement()
    {
        //reset the velocity to require input. 
        rigidbody.velocity -= movement * 0.98f;

        //move camera rotation to a seperate class. 
        float xRot = Input.GetAxis("Mouse X") *cameraSpeed;
        float yRot = -Input.GetAxis("Mouse Y") * cameraSpeed;

        if (grounded) jumpMSMod = 1; else jumpMSMod = jumpMoveSpeedMod;
        transform.Rotate(0, xRot, 0);
        camera.transform.Rotate(yRot, 0, 0);

        //get movement dir
        
        Vector3 moveDir = (forward * Input.GetAxis("Vertical")) + (this.transform.right * Input.GetAxis("Horizontal"));
        //set Movement
        movement = moveDir * Time.fixedDeltaTime * movementSpeed * jumpMSMod;

        rigidbody.velocity += movement;
    }
}
