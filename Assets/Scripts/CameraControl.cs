using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
    public float distanceAway = 7f;
    public float distanceUp = 1f;
    private Transform followXForm;

    private Vector3 targetPosition;
    private Vector3 lookDir;


    private Vector3 velocityCamSmooth = Vector3.zero;
    public float camSmoothDampTime = 0.1f;


    // Use this for initialization
    void Start()
    {
        followXForm = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame

    void FixedUpdate()
    {
        Vector3 characterOffset = followXForm.position + new Vector3(0f, distanceUp, 0f);


        lookDir = characterOffset - this.transform.position;
        lookDir.y = 0;
        lookDir.Normalize();



        //targetPosition = followXForm.position + followXForm.up * distanceUp - followXForm.forward * distanceAway;
        targetPosition = characterOffset + followXForm.up * distanceUp - lookDir * distanceAway;

        CompensateForWalls(characterOffset, ref targetPosition);

        //camera smoothing
        SmoothPosition(this.transform.position, targetPosition);

        //make sure camera is looking the right way
        transform.LookAt(followXForm);
    }

    private void SmoothPosition(Vector3 fromPos, Vector3 toPos)
    {
        this.transform.position = Vector3.SmoothDamp(fromPos, toPos, ref velocityCamSmooth, camSmoothDampTime);
    }

    private void CompensateForWalls(Vector3 fromObject, ref Vector3 toTarget)
    {
        RaycastHit wallHit = new RaycastHit();
        if (Physics.Linecast(fromObject, toTarget, out wallHit))
        {
            toTarget = new Vector3(wallHit.point.x, toTarget.y, wallHit.point.z);
        }
    }
}
