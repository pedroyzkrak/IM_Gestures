﻿using System.Windows;
using Microsoft.Kinect;
using System.ComponentModel;
using Microsoft.Kinect.VisualGestureBuilder;
using System;

namespace gestureModality
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        
        private KinectSensor kinect = null;
        
        private string statusText = null;
        /*
        private GestureMod _gm;
        private VisualGestureBuilderDatabase vgbDb;
        private VisualGestureBuilderFrameSource vgbFrameSource;
        private VisualGestureBuilderFrameReader vgbFrameReader;
        private BodyFrameReader bodyFrameReader; 
        */
        public MainWindow()
        {
            InitializeComponent();
            
            
            //this.DataContext = this;
            /*
            _gm = new GestureMod();
            bodyFrameReader = kinect.BodyFrameSource.OpenReader();
            bodyFrameReader.FrameArrived += bodyFrameArrived;
            vgbDb = new VisualGestureBuilderDatabase(@"..\Gestures\Gesture.gbd");
            vgbFrameSource = new VisualGestureBuilderFrameSource(KinectSensor.GetDefault(), 0);
            foreach (var g in vgbDb.AvailableGestures)
            {
                vgbFrameSource.AddGesture(g);
            }
            vgbFrameReader = vgbFrameSource.OpenReader();
            vgbFrameReader.FrameArrived += vgbFrameArrived; 
            */
            
            
        }
        public event PropertyChangedEventHandler PropertyChanged;
        
        private void Sensor_IsAvailableChanged(object sender, IsAvailableChangedEventArgs e)
        {
            this.StatusText = this.kinect.IsAvailable ? "The Kinect is availabe." : "Kinect is Unavailable!";
        } 
        public string StatusText
        {
            get
            {
                return this.statusText;
            }
            set
            {
                if (this.statusText != value)
                {
                    this.statusText = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("StatusText"));
                    }
                }
            }
        }
        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            if (this.kinect != null)
            {
                this.kinect.Close();
                this.kinect = null;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.kinect = KinectSensor.GetDefault();
            this.kinect.IsAvailableChanged += this.Sensor_IsAvailableChanged;
            this.StatusText = this.kinect.IsAvailable ? "The Kinect is availabe." : "Kinect is Unavailable!";
            kinect.Open();
        }
        /*
private void bodyFrameArrived(object sender, BodyFrameArrivedEventArgs e)
{
   if (!vgbFrameSource.IsTrackingIdValid)
   {
       using (BodyFrame bodyFrame = e.FrameReference.AcquireFrame())
       {
           if (bodyFrame != null)
           {
               Body[] bodies = new Body[6];
               bodyFrame.GetAndRefreshBodyData(bodies);
               Body closestBody = null;
               foreach (Body b in bodies)
               {
                   if (b.IsTracked)
                   {
                       if (closestBody == null)
                       {
                           closestBody = b;
                       }
                       else
                       {
                           Joint newHeadJoint = b.Joints[JointType.Head];
                           Joint oldHeadJoint = closestBody.Joints[JointType.Head];
                           if (newHeadJoint.TrackingState == TrackingState.Tracked &&
                          newHeadJoint.Position.Z < oldHeadJoint.Position.Z)
                           {
                               closestBody = b;
                           }

                       }
                   }
                   if (closestBody != null)
                   {
                       vgbFrameSource.TrackingId = closestBody.TrackingId;
                   }
               }
           }
       }
   }
}

private void vgbFrameArrived(object sender, VisualGestureBuilderFrameArrivedEventArgs e)
{
   using (var vgbFrame = e.FrameReference.AcquireFrame())
   {
       if (vgbFrame != null && vgbFrame.DiscreteGestureResults != null)
       {
           var discreteResults = vgbFrame.DiscreteGestureResults;
           if (discreteResults != null)
           {
               foreach (Gesture g in vgbFrameSource.Gestures)
               {
                   if (g.GestureType == GestureType.Discrete)
                   {
                       DiscreteGestureResult result = null;
                       discreteResults.TryGetValue(g, out result);

                       if (result != null)
                       {
                           Console.WriteLine(g.Name + " " + result.Confidence + "\n");
                       }
                   }
               }
           }
           else
           {
               //cenas ? 
           }
       }
   }
}


*/
    }
}