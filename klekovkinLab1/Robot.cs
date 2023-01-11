using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Numerics;

namespace klekovkinLab1
{
    class Robot:FieldObj
    {
        Lidar lidar;
        Autopilot autopilot;
        SpeedVect tgtSpeed = new SpeedVect(0, 0);
        public const float basicLinSpeed = 3;
        public const float basicAngSpeed = basicLinSpeed * 0.01f;
        bool manualCtrl = false;
        public Robot() 
        {
            shape = Shapes.TRIANGLGE;
            autopilot = new Autopilot(this.rad);
        }
        public Robot(PointF _center, float _alpha, float _rad)
            : base(_center, _alpha, Shapes.TRIANGLGE, _rad, true)
        {
            lidar = new Lidar(0, -MathF.PI / 2, MathF.PI/2, 180, _rad);
            autopilot = new Autopilot(this.rad);
        }

        public Robot(float _x, float _y, float _alpha, float _rad)
            : base(_x, _y, _alpha, Shapes.TRIANGLGE, _rad, true)
        {
            lidar = new Lidar(0, -MathF.PI / 2, MathF.PI / 2, 180, _rad);
            autopilot = new Autopilot(this.rad);
        }

        public Robot(Robot obj)
        {
            this.Center = obj.Center;
            this.speedVect = obj.speedVect;
            this.alpha = obj.alpha;
            this.shape = obj.shape;
            this.movable = obj.movable;
            this.rad = obj.rad;
            this.lidar = obj.lidar;
            autopilot = new Autopilot(this.rad);
        }

        public void Handler()
        {
            autopilot.DetectObses(LidarRays);
            autopilot.CalcPath(this);
            tgtSpeed = autopilot.CalcTgtSpeed(this);
        }

        public (PointF startP, PointF endP)[] GetLidarRaysPs()
        {
            return lidar.getRaysPs();
        }

        public Lidar Lidar { get { return lidar; } }
        public List<Ray> LidarRays 
        {
            get => lidar.Rays;
        }

        public List<List<Ray>> Obses
        {
            get => autopilot.Obses;
        }

        public SpeedVect TgtSpeed
        {
            get => tgtSpeed;
        }

        public bool ManualControl
        {
            get => manualCtrl;
            set => manualCtrl = value;
        }

        public void setTgtPt(Vector2 tgtPt)
        {
           autopilot.SetTgtPt(tgtPt);
        }
    }

}
