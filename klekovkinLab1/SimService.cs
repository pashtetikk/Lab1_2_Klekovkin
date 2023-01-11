using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace klekovkinLab1
{
    internal class SimService
    {
        Random rand = new Random();
        Field field;
        int robotMCtrlSpeed = 3;
        bool manualControl = true;
        
        public int RobotMCtrlSpeed { get => robotMCtrlSpeed; }
        public SimService(int fieldWidth, int fieldHeigth)
        {
            field = new Field(fieldWidth, fieldHeigth);
        }

        public void ModelTick()     //Здесь вызывать все хэндлеры
        {
            field.handler();
        }

        public void setRobotSpeed(SpeedVect _robotSpeed)
        {
            for (int i = 0; i < field.Robots.Count; i++)
            {
                if (field.Robots[i].ManualControl)
                {
                    field.SetRobotsTgtSpd(i, _robotSpeed);
                }
            }
        }

        public void ChangeControlMode(int id, bool manualControl)
        {
            if(id < field.Robots.Count && id >= 0)
            {
                field.Robots[id].ManualControl = manualControl;
            }
        }

        public List<Obstacle> GetObses()
        {
            return field.Obses;
        }

        public List<Robot> GetRobots()
        {
            return field.Robots;
        }

        public void GenRandObses()
        {
            field.ClearObses();
            int quanity = rand.Next(6, 20);
            field.GenRandObses(quanity);
    
        }

        public void CleanObses()
        {
            field.ClearObses();
        }

        public void AddObstacle(PointF center, float rad)
        {
            field.AddObstacle(center, rad);
        }

        public void AddTgtPoint(PointF point)
        {
            field.SetRobotsTgtPt(0, point);
        }

        public PointF GetRobotTgtPt(int id)
        {
            return field.GetRobotsTgtPt(id);
        }

        public Matrix3x2 GetRoboToBaseM(int id)
        {
            if ((id < field.Robots.Count) && (id >= 0))
            {
                return new Matrix3x2((float)Math.Cos(field.Robots[id].Alpha), (float)Math.Sin(field.Robots[id].Alpha),
                                            -(float)Math.Sin(field.Robots[id].Alpha), (float)Math.Cos(field.Robots[id].Alpha),
                                             field.Robots[id].Center.X, field.Robots[id].Center.Y);
            }
            return new Matrix3x2();
        }

        public Matrix3x2 GetLidarTobaseM(int offsetX, int offsetY)
        {
            return new Matrix3x2(MathF.Cos(MathF.PI/2), MathF.Sin(MathF.PI / 2),
                                            -MathF.Sin(MathF.PI / 2), MathF.Cos(MathF.PI / 2),
                                             offsetX, offsetY);

        }

    }
}
