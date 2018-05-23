using System.Windows;
using Microsoft.Kinect;
using System.ComponentModel;
using Microsoft.Kinect.VisualGestureBuilder;
using System;
using System.Timers;
using System.Threading.Tasks;

namespace gestureModality
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private string current_gesture = "";
        private int time = -1;
        private KinectSensor kinect = null;

        private string statusText = null;

        private GestureMod _gm;
        private VisualGestureBuilderDatabase vgbDb;
        private VisualGestureBuilderFrameSource vgbFrameSource;
        private VisualGestureBuilderFrameReader vgbFrameReader;
        private BodyFrameReader bodyFrameReader;

        public MainWindow()
        {
            InitializeComponent();

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
            this.DataContext = this;

            _gm = new GestureMod();
            bodyFrameReader = kinect.BodyFrameSource.OpenReader();
            bodyFrameReader.FrameArrived += bodyFrameArrived;
            bodyFrameReader.IsPaused = false;
            vgbDb = new VisualGestureBuilderDatabase(@"Gestures\IM_Gestures.gbd");
            vgbFrameSource = new VisualGestureBuilderFrameSource(KinectSensor.GetDefault(), 0);
            foreach (var g in vgbDb.AvailableGestures)
            {
                vgbFrameSource.AddGesture(g);
            }
            vgbFrameReader = vgbFrameSource.OpenReader();
            vgbFrameReader.FrameArrived += vgbFrameArrived;
            kinect.Open();
        }

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

        private bool notXSecondsPassed(int time1, int time2)
        {
            if (time2 - time1 <= 1)
                return true;
            return false;
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

                                if (result != null && result.Confidence>0.5)
                                {
                                    Console.WriteLine("Gesto:  " + g.Name + "   Confiança: " + result.Confidence);
                                    switch(g.Name)
                                    {
                                        case "ir_direita_hands_Right":
                                            if (current_gesture == "ir_esquerda_hands_Right" && notXSecondsPassed(time,DateTime.Now.Second) && time != -1)
                                            {
                                                Console.WriteLine("vai pra direita");
                                                time = -1;
                                                
                                            }
                                            else if(!notXSecondsPassed(time, DateTime.Now.Second) || time == -1)
                                            {
                                                time = DateTime.Now.Second;
                                                current_gesture = g.Name;
                                            }
                                            break;
                                        case "ir_esquerda_hands_Right":
                                            if (current_gesture == "ir_direita_hands_Right" && notXSecondsPassed(time, DateTime.Now.Second) && time != -1)
                                            {
                                                Console.WriteLine("vai pra esquerda");
                                                time = -1;

                                            }
                                            else if (!notXSecondsPassed(time, DateTime.Now.Second) || time == -1)
                                            {
                                                time = DateTime.Now.Second;
                                                current_gesture = g.Name;
                                            }
                                            break;

                                        case "ir_cima":
                                            if (current_gesture == "ir_baixo" && notXSecondsPassed(time, DateTime.Now.Second) && time != -1)
                                            {
                                                Console.WriteLine("Ir pra cima");
                                                time = -1;

                                            }
                                            else if (!notXSecondsPassed(time, DateTime.Now.Second) || time == -1)
                                            {
                                                time = DateTime.Now.Second;
                                                current_gesture = g.Name;
                                            }
                                            break;
                                        case "ir_baixo":
                                            if (current_gesture == "ir_cima" && notXSecondsPassed(time, DateTime.Now.Second) && time != -1)
                                            {
                                                Console.WriteLine("Ir pra baixo");
                                                time = -1;

                                            }
                                            else if (!notXSecondsPassed(time, DateTime.Now.Second) || time == -1)
                                            {
                                                time = DateTime.Now.Second;
                                                current_gesture = g.Name;
                                            }
                                            break;
                                        case "mapa_aberto":
                                            if (current_gesture == "mapa_fechado" && notXSecondsPassed(time, DateTime.Now.Second) && time != -1)
                                            {
                                                Console.WriteLine("Abrir mapa");
                                                time = -1;

                                            }
                                            else if (!notXSecondsPassed(time, DateTime.Now.Second) || time == -1 || current_gesture!="mapa_fechado")
                                            {
                                                time = DateTime.Now.Second;
                                                current_gesture = g.Name;
                                            }
                                            break;
                                        case "mapa_fechado":
                                            if (current_gesture == "mapa_aberto" && notXSecondsPassed(time, DateTime.Now.Second) && time!=-1)
                                            {
                                                Console.WriteLine("Fechar Mapa");
                                                time = -1;

                                            }
                                            else if (!notXSecondsPassed(time, DateTime.Now.Second) || time == -1 || current_gesture != "mapa_aberto")
                                            {
                                                time = DateTime.Now.Second;
                                                current_gesture = g.Name;
                                            }
                                            break;
                                    }
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

    }
}