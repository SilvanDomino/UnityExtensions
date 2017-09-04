using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPathing : MonoBehaviour {

    LineRenderer lineRenderer;
    GameObject go;
    new Camera camera;
    [SerializeField]
    private GameObject jumpCube;

    public List<Vector3> line = new List<Vector3>();
    public bool jumpEnabled = true;

	void Start()
    {
        camera = Camera.main;
        lineRenderer = this.GetComponent<LineRenderer>();
        lineRenderer.numPositions = 15;
        go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        go.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
    }
    void Update()
    {
        if (jumpEnabled)
        {
            RaycastHit hit;
            line.Clear();
            for (int i = 0; i < 20; i++)
            {
                Vector3 pos1 = new Vector3(0, -0.5f, 0) + this.transform.position + ExtrapolateLine(i);
                Vector3 pos2 = new Vector3(0, -0.5f, 0) + this.transform.position + ExtrapolateLine(i + 1);
                Debug.DrawLine(pos1, pos2);
                line.Add(pos1);
                if (Physics.Linecast(pos1, pos2, out hit))
                {
                    if (hit.transform.tag == Tags.jumpable)
                    {
                        jumpCube.transform.position = hit.point;
                        break;
                    }
                }

            }
            lineRenderer.numPositions = line.Count;
            for (int i = 0; i < line.Count; i++)
            {
                lineRenderer.SetPosition(i, line[i]);
                lineRenderer.startWidth = 0.5f;
                lineRenderer.endWidth = 0.04f;
            }

        }
        
        
    }

    Vector3 ExtrapolateLine(float i)
    {
        //TODO: Replace camera with VR input
        float yRotation = (Mathf.PI/180) * -camera.transform.rotation.eulerAngles.x;
        Vector3 forward = transform.forward;
        Vector3 linePos = new Vector3(forward.x * i, CreateTrajectory(i, yRotation, 19.81f, 15, 1) , forward.z * i);
        return linePos;
    }

    float CreateTrajectory(float x, float angle, float grav, float velocity, float height)
    {
        float left = height + x * Mathf.Tan(angle);
        float top = grav * Mathf.Pow(x, 2);
        float bot = 2 * Mathf.Pow(velocity * Mathf.Cos(angle), 2);
        return left - top / bot;
    }
}
