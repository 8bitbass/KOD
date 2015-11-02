using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
    public float distanceAway = 7f;
    public float distanceUp = 1f;
    private Transform followXForm;

    public float freeCamActivate = 0.2f;
    public float distanceAwayMultiplier = 1f;
    public float distanceUpMultiplier = 1f;

    public float minDistanceAway = .5f;
    public float maxDistanceAway = 2f;
    public float minDistanceUp = .5f;
    public float maxDistanceUp = 2f;

    private float rightStickHorizontal;
    private float rightStickVertical;

    private Vector3 targetPosition;
    private Vector3 lookDir;


    private Vector3 velocityCamSmooth = Vector3.zero;
    public float camSmoothDampTime = 0.1f;

    private enum State
    {
        FREE,
        LOCKED,
        NORMAL
    }

    private State cameraMode;
    // Use this for initialization
    void Start()
    {
        followXForm = GameObject.FindGameObjectWithTag("Player").transform;
        cameraMode = State.NORMAL;
    }

    // Update is called once per frame

    void FixedUpdate()
    {
        rightStickHorizontal = Input.GetAxis("RightHorizontal");
        rightStickVertical = Input.GetAxis("RightVertical");

        Vector2 rightStick = new Vector2(rightStickHorizontal, rightStickVertical);

        Vector3 characterOffset = followXForm.position + new Vector3(0f, distanceUp, 0f);

        lookDir = characterOffset - this.transform.position;
        lookDir.y = 0;
        lookDir.Normalize();

        if (rightStick.magnitude >= freeCamActivate)
        {
            cameraMode = State.FREE;
        }

        //targetPosition = followXForm.position + followXForm.up * distanceUp - followXForm.forward * distanceAway;
        switch (cameraMode)
        {
            case State.NORMAL:
                targetPosition = characterOffset + followXForm.up * distanceUp - lookDir * distanceAway;
                break;
            case State.FREE:
                distanceUpMultiplier -= rightStickVertical / 10;
                distanceAwayMultiplier -= rightStickVertical / 10;
                
                distanceAwayMultiplier = Mathf.Clamp(distanceAwayMultiplier, minDistanceAway, maxDistanceAway);
                distanceUpMultiplier = Mathf.Clamp(distanceUpMultiplier, minDistanceUp, maxDistanceUp);

                transform.RotateAround(followXForm.position, Vector3.up, -rightStickHorizontal * 100 * Time.deltaTime);

                targetPosition = characterOffset + followXForm.up * (distanceUp * distanceUpMultiplier) - lookDir * (distanceAway * distanceAwayMultiplier);
                //targetPosition
                break;
            case State.LOCKED:
                break;
        }

        CompensateForWalls(characterOffset, ref targetPosition);

        //camera smoothing
        SmoothPosition(this.transform.position, targetPosition);

        //make sure camera is looking the right way (this is dodgy)
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
