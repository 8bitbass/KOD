using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
    //OnAnimatorMove might be required later on.
    public float speed = 0.0f;
    public float directionSpeed = 1.5f;
    public float rotationDegreesPerSecond = 120f;
    private float horizontal = 0.0f;
    private float vertical = 0.0f;
    private float r = 0.0f;

    public CameraControl gamecam;

    private float speeder = 0.0f;
    private float direction = 0.0f;

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        vertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        r = new Vector2(horizontal, vertical).sqrMagnitude;

        StickToWorldSpace(this.transform, gamecam.transform, ref direction, ref speeder);

        //Vector3 newPosition = transform.position;
        //newPosition.z += vertical;
        //transform.localPosition = newPosition;

        transform.localPosition += transform.forward * r;

        //transform.Rotate(0, horizontal * 3, 0);


    }

    void FixedUpdate()
    {
        if ((direction >= 0 && horizontal >= 0) || (direction < 0 && horizontal < 0))
        {
            Vector3 rotationAmount = Vector3.Lerp(Vector3.zero, new Vector3(0f, rotationDegreesPerSecond * (horizontal < 0f ? -1f : 1f), 0f), Mathf.Abs(horizontal));
            Quaternion deltaRotation = Quaternion.Euler(rotationAmount * Time.deltaTime);
            this.transform.rotation = (this.transform.rotation * deltaRotation);
        }
    }

    public void StickToWorldSpace(Transform root, Transform camera, ref float directionOut, ref float speedOut)
    {
        Vector3 rootDirection = root.forward;

        Vector3 stickDirection = new Vector3(horizontal, 0, vertical);

        speedOut = stickDirection.sqrMagnitude;

        //get camera rotation
        Vector3 cameraDirection = camera.forward;
        cameraDirection.y = 0.0f;
        Quaternion referentialShift = Quaternion.FromToRotation(Vector3.forward, cameraDirection); // puts cameraDirection in the same vector space as Vector3.forward

        //convert joystick input in worldspace coordinates
        Vector3 moveDirection = referentialShift * stickDirection;
        Vector3 axisSign = Vector3.Cross(moveDirection, rootDirection);

        //debug stuff
        Vector3 debugVector3 = new Vector3(root.position.x, root.position.y + 2f, root.position.z);
        Debug.DrawRay(debugVector3, moveDirection, Color.green);
        Debug.DrawRay(debugVector3, rootDirection, Color.magenta);
        Debug.DrawRay(debugVector3, stickDirection, Color.blue);


        float angleRootToMove = Vector3.Angle(rootDirection, moveDirection) * (axisSign.y >= 0 ? -1f : 1f);

        angleRootToMove /= 180;

        directionOut = angleRootToMove * directionSpeed;
    }
}