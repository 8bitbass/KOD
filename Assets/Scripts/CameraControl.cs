using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
    public float distanceAway;
    public float distanceUp;
    public float smooth;
    public Vector3 offset = new Vector3(0f, 1.5f, 0f);
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
    void Update()
    {

    }

    void LateUpdate()
    {
        Vector3 characterOffset = followXForm.position + offset;


        lookDir = characterOffset - this.transform.position;
        lookDir.y = 0;
        lookDir.Normalize();



        //targetPosition = followXForm.position + followXForm.up * distanceUp - followXForm.forward * distanceAway;
        targetPosition = characterOffset + followXForm.up * distanceUp - lookDir * distanceAway;


        //camera smoothing
        //transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smooth);
        smoothPosition(this.transform.position, targetPosition);

        //make sure camera is looking the right way
        transform.LookAt(followXForm);
    }

    private void smoothPosition(Vector3 fromPos, Vector3 toPos)
    {
        this.transform.position = Vector3.SmoothDamp(fromPos, toPos, ref velocityCamSmooth, camSmoothDampTime);
    }
}
