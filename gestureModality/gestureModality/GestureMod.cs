using System;
using mmisharp;

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

        public void GestureRecognized(String action, float confidence)
        {
            //SEND
            // IMPORTANT TO KEEP THE FORMAT {"recognized":["SHAPE","COLOR"]}
            string json = "{ \"recognized\": [";
            json += "\"" + action + "\", ";
            json = json.Substring(0, json.Length - 2);
            json += "] }";
            Console.WriteLine(json);

            var exNot = lce.ExtensionNotification("", "", confidence, json);
            mmic.Send(exNot);
        }

    }
}
