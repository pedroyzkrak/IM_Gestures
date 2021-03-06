﻿using System.Windows;
using Microsoft.Kinect;
using System.ComponentModel;
using Microsoft.Kinect.VisualGestureBuilder;
using System;
using System.Timers;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Generic;

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
        private string gestureText = null;

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
            this.StatusText = this.kinect.IsAvailable ? "A Kinect está ativa." : "A Kinect está desativada!";
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
        public string GestureText
        {
            get
            {
                return this.gestureText;
            }
            set
            {
                if (this.gestureText != value)
                {
                    this.gestureText = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("GestureText"));
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
            this.StatusText = this.kinect.IsAvailable ? "A Kinect está ativa." : "A Kinect está desativada!";
            this.GestureText = "Nenhum gesto detetado.";
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
            if (time1 <= 950)
            {
                return true;
            }

            return false;
        }

        private bool AtLeastOneDetected(IReadOnlyDictionary<Gesture, DiscreteGestureResult> d)
        {
            foreach (DiscreteGestureResult g in d.Values)
            {
                if (g.Detected)
                {
                    return true;
                }
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
                        if (!AtLeastOneDetected(discreteResults))
                        {
                            if (stopwatch.IsRunning)
                            {
                                if (!notXSecondsPassed(stopwatch.Elapsed.Milliseconds))
                                {
                                    current_gesture = "";
                                    stopwatch.Stop();
                                }
                            }
                            else
                            {
                                current_gesture = "";
                            }
                            this.GestureText = "Nenhum gesto detetado";
                        }
                        else
                        {
                            foreach (Gesture g in vgbFrameSource.Gestures)
                            {
                                if (g.GestureType == GestureType.Discrete)
                                {
                                    DiscreteGestureResult result = null;
                                    discreteResults.TryGetValue(g, out result);

                                    if (result != null && result.Confidence > 0.4)
                                    {
                                        switch (g.Name)
                                        {
                                            case "ir_direita_Right":
                                                if (stopwatch.IsRunning && current_gesture != g.Name)
                                                {
                                                    if (!notXSecondsPassed(stopwatch.Elapsed.Milliseconds)) //está o timer ativo e já passaram os segundos permitidos
                                                    {
                                                        Console.WriteLine("Vai pra direita");
                                                        _gm.GestureRecognized("MOVER\", \"DIREITA", result.Confidence);
                                                        stopwatch.Stop();
                                                        current_gesture = g.Name;
                                                        this.GestureText = "Ir para a direita, Confiança: " + result.Confidence;
                                                    }
                                                }
                                                else if (!stopwatch.IsRunning && current_gesture != g.Name) // o stopwatch não está ativo
                                                {
                                                    Console.WriteLine("Vai pra direita");
                                                    _gm.GestureRecognized("MOVER\", \"DIREITA", result.Confidence);
                                                    current_gesture = g.Name;
                                                    this.GestureText = "Ir para a direita, Confiança: " + result.Confidence;
                                                }
                                                break;
                                            case "ir_esquerda_Left":
                                                if (stopwatch.IsRunning && current_gesture != g.Name)
                                                {
                                                    if (!notXSecondsPassed(stopwatch.Elapsed.Milliseconds)) //está o timer ativo e já passaram os segundos permitidos
                                                    {
                                                        Console.WriteLine("Vai pra esquerda");
                                                        _gm.GestureRecognized("MOVER\", \"ESQUERDA", result.Confidence);
                                                        stopwatch.Stop();
                                                        current_gesture = g.Name;
                                                        this.GestureText = "Ir para a esquerda, Confiança: " + result.Confidence;
                                                    }
                                                }
                                                else if (!stopwatch.IsRunning && current_gesture != g.Name) // o stopwatch não está ativo
                                                {
                                                    Console.WriteLine("Vai pra esquerda");
                                                    _gm.GestureRecognized("MOVER\", \"ESQUERDA", result.Confidence);
                                                    current_gesture = g.Name;
                                                    this.GestureText = "Ir para a esquerda, Confiança: " + result.Confidence;
                                                }
                                                break;

                                            case "ir_cima":
                                                if (stopwatch.IsRunning && current_gesture != g.Name)
                                                {
                                                    if (!notXSecondsPassed(stopwatch.Elapsed.Milliseconds)) //está o timer ativo e já passaram os segundos permitidos
                                                    {
                                                        Console.WriteLine("Vai pra cima");
                                                        _gm.GestureRecognized("MOVER\", \"CIMA", result.Confidence);
                                                        stopwatch.Stop();
                                                        current_gesture = g.Name;
                                                        this.GestureText = "Ir para cima, Confiança: " + result.Confidence;
                                                    }
                                                }
                                                else if (!stopwatch.IsRunning && current_gesture != g.Name) // o stopwatch não está ativo
                                                {
                                                    Console.WriteLine("Vai pra cima");
                                                    _gm.GestureRecognized("MOVER\", \"CIMA", result.Confidence);
                                                    current_gesture = g.Name;
                                                    this.GestureText = "Ir para cima, Confiança: " + result.Confidence;
                                                }
                                                break;
                                            case "ir_baixo_double":
                                                if (stopwatch.IsRunning && current_gesture != g.Name)
                                                {
                                                    Console.WriteLine(current_gesture);
                                                    if (!notXSecondsPassed(stopwatch.Elapsed.Milliseconds)) //está o timer ativo e já passaram os segundos permitidos
                                                    {
                                                        Console.WriteLine("1Vai pra baixo");
                                                        _gm.GestureRecognized("MOVER\", \"BAIXO", result.Confidence);
                                                        stopwatch.Stop();
                                                        current_gesture = g.Name;
                                                        this.GestureText = "Ir para baixo, Confiança: " + result.Confidence;
                                                    }
                                                }
                                                else if (!stopwatch.IsRunning && current_gesture != g.Name) // o stopwatch não está ativo
                                                {
                                                    Console.WriteLine("2Vai pra baixo, " + current_gesture);
                                                    _gm.GestureRecognized("MOVER\", \"BAIXO", result.Confidence);
                                                    current_gesture = g.Name;
                                                    this.GestureText = "Ir para baixo, Confiança: " + result.Confidence;
                                                }
                                                break;
                                            case "mapa_aberto":
                                                if (stopwatch.IsRunning && current_gesture == "mapa_fechado")
                                                {
                                                    if (notXSecondsPassed(stopwatch.Elapsed.Milliseconds))
                                                    {

                                                        Console.WriteLine("Abrir mapa");
                                                        _gm.GestureRecognized("ABRIR\", \"MAPA", result.Confidence);
                                                        stopwatch.Stop();
                                                        current_gesture = g.Name;
                                                        this.GestureText = "Abrir mapa, Confiança: " + result.Confidence;
                                                    }
                                                    else
                                                    {
                                                        stopwatch.Stop();
                                                        current_gesture = g.Name;
                                                    }
                                                }
                                                else if (!stopwatch.IsRunning)
                                                {
                                                    if (current_gesture != g.Name)
                                                    {
                                                        stopwatch.Start();
                                                        current_gesture = g.Name;
                                                    }
                                                }
                                                break;
                                            case "mapa_fechado":
                                                if (stopwatch.IsRunning && current_gesture == "mapa_aberto")
                                                {
                                                    if (notXSecondsPassed(stopwatch.Elapsed.Milliseconds))
                                                    {

                                                        Console.WriteLine("fechar mapa");
                                                        _gm.GestureRecognized("FECHAR\", \"MAPA", result.Confidence);
                                                        stopwatch.Stop();
                                                        current_gesture = g.Name;
                                                        this.GestureText = "Fechar mapa, Confiança: " + result.Confidence;
                                                    }
                                                    else
                                                    {
                                                        stopwatch.Stop();
                                                        current_gesture = g.Name;
                                                    }
                                                }
                                                else if (!stopwatch.IsRunning)
                                                {
                                                    if (current_gesture != g.Name)
                                                    {
                                                        stopwatch.Start();
                                                        current_gesture = g.Name;
                                                    }
                                                }
                                                break;
                                            case "atacar_direita_body_Right":
                                                if (stopwatch.IsRunning && current_gesture != g.Name)
                                                {
                                                    if (!notXSecondsPassed(stopwatch.Elapsed.Milliseconds)) //está o timer ativo e já passaram os segundos permitidos
                                                    {
                                                        Console.WriteLine("Ataca");
                                                        _gm.GestureRecognized("ATACAR", result.Confidence);
                                                        stopwatch.Stop();
                                                        current_gesture = g.Name;
                                                        this.GestureText = "Atacar, Confiança: " + result.Confidence;
                                                    }
                                                }
                                                else if (!stopwatch.IsRunning && current_gesture != g.Name) // o stopwatch não está ativo
                                                {
                                                    Console.WriteLine("Ataca");
                                                    _gm.GestureRecognized("ATACAR", result.Confidence);
                                                    current_gesture = g.Name;
                                                    this.GestureText = "Atacar, Confiança: " + result.Confidence;
                                                }
                                                break;
                                            case "atacar_esquerda_body_Left":
                                                if (stopwatch.IsRunning && current_gesture != g.Name)
                                                {
                                                    if (!notXSecondsPassed(stopwatch.Elapsed.Milliseconds)) //está o timer ativo e já passaram os segundos permitidos
                                                    {
                                                        Console.WriteLine("Ataca");
                                                        _gm.GestureRecognized("ATACAR", result.Confidence);
                                                        stopwatch.Stop();
                                                        current_gesture = g.Name;
                                                        this.GestureText = "Atacar, Confiança: " + result.Confidence;
                                                    }
                                                }
                                                else if (!stopwatch.IsRunning && current_gesture != g.Name) // o stopwatch não está ativo
                                                {
                                                    Console.WriteLine("Ataca");
                                                    _gm.GestureRecognized("ATACAR", result.Confidence);
                                                    current_gesture = g.Name;
                                                    this.GestureText = "Atacar, Confiança: " + result.Confidence;
                                                }
                                                break;
                                        }
                                    }
                                }
                            }
                        }

                    }
                }
            }
        }
    }
}
