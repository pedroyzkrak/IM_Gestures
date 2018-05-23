using System.Windows;
using Microsoft.Kinect;
using System.ComponentModel;
using Microsoft.Kinect.VisualGestureBuilder;
using System;
using System.Timers;
using System.Threading.Tasks;
using System.Diagnostics;

namespace gestureModality
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private string current_gesture = "";
        private Stopwatch stopwatch = new Stopwatch();
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

        private bool notXSecondsPassed(int time1)
        {
            if (time1 <= 800)
            {
                Console.WriteLine("Tempo: "+time1);
                return true;
            }

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
                                    //Console.WriteLine("Gesto:  " + g.Name + "   Confiança: " + result.Confidence);
                                    switch(g.Name)
                                    {
                                        case "ir_direita_Right":
                                            if (stopwatch.IsRunning && current_gesture!=g.Name)
                                            {
                                                if (!notXSecondsPassed(stopwatch.Elapsed.Milliseconds)) //está o timer ativo e já passaram os segundos permitidos
                                                {
                                                    Console.WriteLine("Vai pra direita");
                                                    stopwatch.Stop();
                                                    current_gesture = g.Name;
                                                }
                                            }
                                           else if(!stopwatch.IsRunning && current_gesture!=g.Name) // o stopwatch não está ativo
                                            {
                                                Console.WriteLine("Vai pra direita");
                                                current_gesture = g.Name;
                                            }
                                            break;
                                        case "ir_esquerda_Left":
                                            if (stopwatch.IsRunning && current_gesture != g.Name)
                                            {
                                                if (!notXSecondsPassed(stopwatch.Elapsed.Milliseconds)) //está o timer ativo e já passaram os segundos permitidos
                                                {
                                                    Console.WriteLine("Vai pra esquerda");
                                                    stopwatch.Stop();
                                                    current_gesture = g.Name;
                                                }
                                            }
                                            else if (!stopwatch.IsRunning && current_gesture != g.Name) // o stopwatch não está ativo
                                            {
                                                Console.WriteLine("Vai pra esquerda");
                                                current_gesture = g.Name;
                                            }
                                            break;

                                        case "ir_cima":
                                            if (stopwatch.IsRunning && current_gesture != g.Name)
                                            {
                                                if (!notXSecondsPassed(stopwatch.Elapsed.Milliseconds)) //está o timer ativo e já passaram os segundos permitidos
                                                {
                                                    Console.WriteLine("Vai pra cima");
                                                    stopwatch.Stop();
                                                    current_gesture = g.Name;
                                                }
                                            }
                                            else if (!stopwatch.IsRunning && current_gesture != g.Name) // o stopwatch não está ativo
                                            {
                                                Console.WriteLine("Vai pra cima");
                                                current_gesture = g.Name;
                                            }
                                            break;
                                        case "ir_baixo_double":
                                            if (stopwatch.IsRunning && current_gesture != g.Name)
                                            {
                                                if (!notXSecondsPassed(stopwatch.Elapsed.Milliseconds)) //está o timer ativo e já passaram os segundos permitidos
                                                {
                                                    Console.WriteLine("Vai pra baixo");
                                                    stopwatch.Stop();
                                                    current_gesture = g.Name;
                                                }
                                            }
                                            else if (!stopwatch.IsRunning && current_gesture != g.Name) // o stopwatch não está ativo
                                            {
                                                Console.WriteLine("Vai pra baixo");
                                                current_gesture = g.Name;
                                            }
                                            break;
                                        case "mapa_aberto":
                                            if (current_gesture == "mapa_fechado" && notXSecondsPassed(stopwatch.Elapsed.Milliseconds))
                                            {
                                                Console.WriteLine("abrir mapa");
                                                stopwatch.Stop();
                                            }
                                            else if (!notXSecondsPassed(stopwatch.Elapsed.Milliseconds) || !stopwatch.IsRunning)
                                            {
                                                stopwatch.Start();
                                                current_gesture = g.Name;
                                            }
                                            break;
                                        case "mapa_fechado":
                                            if (current_gesture == "mapa_aberto" && notXSecondsPassed(stopwatch.Elapsed.Milliseconds) && stopwatch.IsRunning)
                                            {
                                                Console.WriteLine("fechar mapa");
                                                stopwatch.Stop();


                                            }
                                            else if (!notXSecondsPassed(stopwatch.Elapsed.Milliseconds) || !stopwatch.IsRunning)
                                            {
                                                stopwatch.Start();
                                                current_gesture = g.Name;
                                            }
                                            break;
                                        case "atacar_direita_body_Right":
                                            if (stopwatch.IsRunning && current_gesture != g.Name)
                                            {
                                                if (!notXSecondsPassed(stopwatch.Elapsed.Milliseconds)) //está o timer ativo e já passaram os segundos permitidos
                                                {
                                                    Console.WriteLine("Ataca");
                                                    stopwatch.Stop();
                                                    current_gesture = g.Name;
                                                }
                                            }
                                            else if (!stopwatch.IsRunning && current_gesture != g.Name) // o stopwatch não está ativo
                                            {
                                                Console.WriteLine("Ataca");
                                                current_gesture = g.Name;
                                            }
                                            break;
                                        case "atacar_esquerda_body_Left":
                                            if (stopwatch.IsRunning && current_gesture != g.Name)
                                            {
                                                if (!notXSecondsPassed(stopwatch.Elapsed.Milliseconds)) //está o timer ativo e já passaram os segundos permitidos
                                                {
                                                    Console.WriteLine("Ataca");
                                                    stopwatch.Stop();
                                                    current_gesture = g.Name;
                                                }
                                            }
                                            else if (!stopwatch.IsRunning && current_gesture != g.Name) // o stopwatch não está ativo
                                            {
                                                Console.WriteLine("Ataca");
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
                        if(stopwatch.IsRunning)
                        {
                            if(!notXSecondsPassed(stopwatch.Elapsed.Milliseconds))
                            {
                                current_gesture = "";
                            }
                        }
                        else
                        {
                            current_gesture = "";
                        }
                        
                    }
                }
            }
        }

    }
}