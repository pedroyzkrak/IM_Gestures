using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mmisharp;
using Microsoft.Kinect;

namespace gestureModality
{
    public class GestureMod
    {
        private LifeCycleEvents lce;
        private MmiCommunication mmic;

        public GestureMod()
        {
            //init LifeCycleEvents..
            lce = new LifeCycleEvents("GESTURES", "FUSION", "gesture-1", "acoustic", "command"); // LifeCycleEvents(string source, string target, string id, string medium, string mode)
            mmic = new MmiCommunication("localhost", 8000, "User2", "GESTURES"); // MmiCommunication(string IMhost, int portIM, string UserOD, string thisModalityName)

            mmic.Send(lce.NewContextRequest());

        }

        private void GestureRecognized(String gestureName, float confidence)
        {
            //SEND
            // IMPORTANT TO KEEP THE FORMAT {"recognized":["SHAPE","COLOR"]}
            string json = "{ \"recognized\": [";
            json += "\"" + gestureName + "\", ";
            json = json.Substring(0, json.Length - 2);
            json += "] }";
            Console.WriteLine(json);

            var exNot = lce.ExtensionNotification("", "", confidence, json);
            mmic.Send(exNot);
        }

    }
}
