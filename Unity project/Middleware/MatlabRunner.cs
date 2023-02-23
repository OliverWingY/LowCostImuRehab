using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Middleware
{
    public class MatlabRunner :IDisposable
    {
        private MLApp.MLApp Matlab;
        private string initialiseName = "InitialiseMatlabWorkspace";
        public MatlabRunner() 
        {
            //string MatlabScriptsFolder = Path.Combine(Directory.GetCurrentDirectory(), "MatlabScripts");
            Matlab = new MLApp.MLApp();
            //Matlab.Execute($"cd '{MatlabScriptsFolder}'");
            //InitialiseWorkspace();
        }

        private bool InitialiseWorkspace()
        {
            Matlab.Feval(initialiseName, 1, out object Successful);
            return (bool)((object[])Successful)[0];
        }

        //For every matlab script you want a different function that will look something like this:
        public double RunExampleFunction(double input1, double input2)
        {            
            var numberOfInputs = 2;
            //call this function, this inputs are 1. name of the function, 2. the number of inputs, 3. "out object output" this will be the output of the function as an object, then the inputs one by one.
            Matlab.Feval("ExampleFunction", numberOfInputs, out object output, input1, input2);
            //cast the output to what you want it to be before returning it (put what it should be in brackets).
            return (double)output;
        }

        public double[] ImuAngleToGlobalAngle(double[] imuAngles) 
        {
            Matlab.Feval("ImuAngleToGlobalAngle", 1, out object output, imuAngles);
            var result = (double[,])((object[])output)[0];
            var globalAngles = new double[6];
            for (int i = 0; i < 6; i++)
            {
                globalAngles[i] = result[0,i];
            }
            return globalAngles;
        }

        public void Dispose()
        {
            Matlab.Quit();
        }
    }
}
