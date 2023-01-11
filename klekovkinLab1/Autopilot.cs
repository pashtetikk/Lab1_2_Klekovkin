using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace klekovkinLab1
{
    internal class Autopilot
    {
        const float lidarSense = 300;
        float robotRad = 0;
        Vector2 tgtPt = new Vector2();
        List<Vector2> wayPts = new List<Vector2>();
        
        List<List<Ray>> obses = new List<List<Ray>>();

        public Autopilot(float robotRad)
        {
            this.robotRad = robotRad;   
        }

        public void DetectObses(List<Ray> lidarRays)
        {
            obses.Clear();
            for (int r = 0; r < lidarRays.Count; r++)
            {
                if (lidarRays[r].Lengh < Lidar.Sense)
                {
                    List<Ray> obstacle = new List<Ray>();
                    obstacle.Add((Ray)lidarRays[r].Clone());
                    int lastRayId = r;
                    for (int i = r + 1; i < lidarRays.Count; i++)
                    {
                        if ((lidarRays[i].EndP.ToVector2() - obstacle.Last<Ray>().EndP.ToVector2()).Length() < 2.5 * robotRad)
                        {
                            obstacle.Add((Ray)lidarRays[i].Clone());
                            lastRayId = i;
                        }
                        /*
                        if(i - lastRayId > 10)
                        {
                            r = i;
                            break;
                        }
                        */
                    }
                    r = lastRayId;
                    obses.Add(obstacle);
                }
            }
        }

        public void CalcPath(Robot robot)
        {
            
        }

        public SpeedVect CalcTgtSpeed(Robot robot)
        {
            SpeedVect speed = new SpeedVect();

            if (wayPts.Count > 0)
            {
                if ((robot.Center.ToVector2() - wayPts.Last<Vector2>()).Length() < 10)
                {
                    wayPts.RemoveAt(wayPts.Count - 1);
                }
            }

            if (wayPts.Count == 0)
            {
                return new SpeedVect(0, 0);
            }
            else
            {
                //float angleToPt;
                //angleToPt = (float)(Math.Atan2(wayPts[0].Y - robot.Center.Y, wayPts[0].X - robot.Center.X) - robot.Alpha);
                float minDistObst = 10000;
                float minAngObst = 0;
                float escCoef = 0;
                float escAngle = 0;
                foreach (List<Ray> obstacle in Obses)
                {
                    foreach (Ray ray in obstacle)
                    {
                        if (ray.Lengh < minDistObst)
                        {
                            minDistObst = ray.Lengh;
                            minAngObst = ray.Alpha;
                        }
                    }
                }

                escCoef = (float)Math.Exp(-(minDistObst - 15) / 20);
                if (minAngObst >= 0)
                {
                    escAngle = (float)((minAngObst - Math.PI / 2));
                }
                if (minAngObst < 0)
                {
                    escAngle = (float)((minAngObst + Math.PI / 2));
                }

                //float tgtAng = getMinAngle(robot, wayPts.Last<Vector2>());
                float tgtAng = getMinAngle(robot, wayPts.Last<Vector2>()) * (1 - escCoef) + escAngle * escCoef;
                if (tgtAng > MathF.PI / 90)
                {
                    speed.AngSpeed = Robot.basicAngSpeed;
                }
                else if (tgtAng < -Math.PI / 90)
                {
                    speed.AngSpeed = -Robot.basicAngSpeed;
                }
                else
                {
                    speed.AngSpeed = 0;
                }

                if (MathF.Abs(tgtAng) < MathF.PI / 8)
                {
                    speed.LinSpeed = Robot.basicLinSpeed;
                }
                else
                {
                    speed.LinSpeed = 0;
                }
            }

            return speed;
        }

        public void SetTgtPt(Vector2 point)
        {
            if(wayPts.Count == 0)
            {
                wayPts.Add(point);
            }
            else
            {
                wayPts[0] = point;
            }
            tgtPt = point;
        }

        float getMinAngle(Robot robot, Vector2 point)
        {
            float output;
            output = (float)(Math.Atan2(point.Y - robot.Center.Y, point.X - robot.Center.X) - robot.Alpha);
            if (output > Math.PI) { output -= (float)(2 * Math.PI); }
            if (output < -Math.PI) { output += (float)(2 * Math.PI); }
            return output;
        }

        public List<List<Ray>> Obses
        {
            get => obses;
        }

        bool IsTgtAvalible(Robot robot, Vector2 point)
        {
            return false;
        }
    }
}
