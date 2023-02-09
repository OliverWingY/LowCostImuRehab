using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UDPTinkering;

//attaching this to the ground and using it as a startup script because I'm a hack who cant be bothered to read the documentation
public class Startup : MonoBehaviour
{
    public PortListener PortListener;
    public static double[] CurrentPosition;
    // Start is called before the first frame update
    void Start()
    {
        CurrentPosition= new double[6];
        PortListener = new PortListener(17628);          
    }

    // Update is called once per frame
    void Update()
    {
        CurrentPosition = PortListener.currentAngles;
    }
}
