using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltArm : MonoBehaviour
{
    public KeyCode TiltBycepUp;
    public KeyCode TiltBycepDown;
    private Vector3 upVector = new Vector3(1, 0, 0);
    private Vector3 downVector = new Vector3(-1, 0, 0);
    public KeyCode TiltBycepClockwise;
    public KeyCode TiltBycepAntiClockwise;
    private Vector3 clockwiseVector = new Vector3(0, 0, 1);
    private Vector3 anitclockwiseVector = new Vector3(0, 0, -1);
    public KeyCode TwistBycepClockwise;
    public KeyCode TwistBycepAntiClockwise;
    private Vector3 twistClockwiseVector = new Vector3(0, 1, 0);
    private Vector3 twistAnitClockwiseVector = new Vector3(0, -1, 0);

    public KeyCode Reset;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var transform = GetComponent<Transform>();
        if (Input.GetKey(TiltBycepUp))
            transform.Rotate(upVector);
        if (Input.GetKey(TiltBycepDown))
            transform.Rotate(downVector);
        if (Input.GetKey(TiltBycepClockwise))
            transform.Rotate(clockwiseVector);
        if (Input.GetKey(TiltBycepAntiClockwise))
            transform.Rotate(anitclockwiseVector);
        if (Input.GetKey(TwistBycepClockwise))
            transform.Rotate(twistClockwiseVector);
        if (Input.GetKey(TwistBycepAntiClockwise))
            transform.Rotate(twistAnitClockwiseVector);
        if (Input.GetKey(Reset))
            transform.rotation = new Quaternion() { eulerAngles = new Vector3(-90, 0, 90) };
        var qtn = transform.eulerAngles;
        print($"{this.gameObject.name} Euler angles:  {qtn.x}, {qtn.y}, {qtn.z}");
    }
}
