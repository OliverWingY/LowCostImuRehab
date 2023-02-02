using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltArm : MonoBehaviour
{
    public KeyCode TiltBycepUp;
    public KeyCode TiltBycepDown;
    private Vector3 upVector = new Vector3(1,0,0);
    private Vector3 downVector = new Vector3(-1, 0,0);
    public KeyCode TiltBycepClockwise;
    public KeyCode TiltBycepAntiClockwise;
    private Vector3 clockwiseVector = new Vector3(0, 0, 1);
    private Vector3 anitclockwiseVector = new Vector3(0, 0, -1);
    public KeyCod  e TwistBycepClockwise;
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
        if (Input.GetKey(TiltBycepUp))
            GetComponent<Transform>().Rotate(upVector);
        if (Input.GetKey(TiltBycepDown))
            GetComponent<Transform>().Rotate(downVector);
        if (Input.GetKey(TiltBycepClockwise))
            GetComponent<Transform>().Rotate(clockwiseVector);
        if (Input.GetKey(TiltBycepAntiClockwise))
            GetComponent<Transform>().Rotate(anitclockwiseVector);
        if (Input.GetKey(TwistBycepClockwise))
            GetComponent<Transform>().Rotate(twistClockwiseVector);
        if (Input.GetKey(TwistBycepAntiClockwise))
            GetComponent<Transform>().Rotate(twistAnitClockwiseVector);
        if (Input.GetKey(Reset))
            GetComponent<Transform>().rotation = new Quaternion() { eulerAngles = new Vector3(-90, 0, 90) };

    }
}
