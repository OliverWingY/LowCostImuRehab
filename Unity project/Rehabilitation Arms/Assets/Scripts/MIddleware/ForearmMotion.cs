using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForearmMotion : MonoBehaviour
{
    // Start is called before the first frame update
    internal Startup startup;
    internal BycepMotion bycep;
    private float toDegrees = (float)(180 / 3.14159);
    public float xOffset = 0;
    public float yOffset = 0;
    public float zOffset = 90;
    void Start()
    {
        startup = GameObject.Find("Ground").GetComponent<Startup>();        
    }

    // Update is called once per frame
    void Update()
    {
        var transform = GetComponent<Transform>();

        // this code is the equivalent of this: q_g = (q_imu*M)⦻q_0
        // where q_g are the game quaternions, q_imu is the imu reading, M is a 4d matrix that rearranges the axes, q_0 is the quaternions that describe
        // the calibration position in the game frame of reference (see Oliver Wing Young's final individual report for details), and ⦻ represents quaternion multiplication 
        //Its implemented like this because 
        //1: Getting this working happened 2 months before I understood how it worked
        //2: I cant be bothered to write the code necessary to make q_imu*M possible in one line.
        
        // Seriously take care before attempting to make this make sense or change it.
        //             ______________                               
        //       ,===:'.,            `-._                           
        //            `:.`---.__         `-._                       
        //              `:.     `--.         `.                     
        //                \.        `.         `.                   
        //        (,,(,    \.         `.   ____,-`.,                
        //     (,'     `/   \.   ,--.___`.'                         
        // ,  ,'  ,--.  `,   \.;'         `                         
        //  `{D, {    \  :    \;                                    
        //    V,,'    /  /    //                                    
        //    j;;    /  ,' ,-//.    ,---.      ,                    
        //    \;'   /  ,' /  _  \  /  _  \   ,'/                    
        //          \   `'  / \  `'  / \  `.' /                     
        //           `.___,'   `.__,'   `.__,'  
        transform.eulerAngles = new Vector3((float)startup.Forearm.y * toDegrees, -(float)startup.Forearm.z * toDegrees, -(float)startup.Forearm.x * toDegrees);
        transform.Rotate(xOffset, yOffset, zOffset);
        transform.Rotate(0, startup.yRotation, 0, Space.World);
    }
}
