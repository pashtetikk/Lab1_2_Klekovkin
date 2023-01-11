using klekovkinLab1;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace klekovkinLab1
{
    internal class Physics
    {
        public static void calcPhysics(List<Robot> _robots, List<Obstacle> _obses,
             List<SpeedVect> _robotsTgtSpd)
        {

            List<Robot> robotsNextState = new List<Robot>(_robots.Count);
            foreach (Robot robot in _robots)
            {
                Robot robotCoppy = (Robot)robot.Clone();

                robotsNextState.Add((Robot)robot.Clone());
            }
            calcMovement(robotsNextState, _robotsTgtSpd);

            List<List<int>> collisionResult = calcCollision(robotsNextState, _obses);
            for (int i =0; i< _robots.Count; i++)
            {
                if(!collisionResult[i].Exists(item=>item >=0))
                {
                    _robots[i] = robotsNextState[i];
                }
                else
                {
                    _robots[i].Alpha = robotsNextState[i].Alpha;
                    //тут можно вставить обработку столкновений 
                }                
            }
        }

        public static void calcLidar(List<Robot> robots, List<Obstacle> obses)
        {
            foreach(Robot robot in robots)
            //for(int i = 0; i<robots.Count; i++) 
            {
                //List <Ray> rays = robot.LidarRays;
                foreach(Ray ray in robot.LidarRays)
                //for(int j = 0; j < robot.LidarRays.Count; j++) 
                { 
                    ray.Lengh = 0;
                    for(int k = 0; k<Ray.maxSamples; k++)
                    {
                        float minDistance = 10000;
                        Vector2 test = Vector2.Transform(ray.EndP.ToVector2(), getRoboToBaseM(robot));
                        foreach (Obstacle obstacle in obses)
                        {
                            
                            if (Vector2.Distance(Vector2.Transform(ray.EndP.ToVector2(),getRoboToBaseM(robot)) , obstacle.Center.ToVector2()) - obstacle.Rad < minDistance)
                            {
                                minDistance = Vector2.Distance(Vector2.Transform(ray.EndP.ToVector2(), getRoboToBaseM(robot)), obstacle.Center.ToVector2()) - obstacle.Rad;
                            }
                        }
                        ray.Lengh += minDistance;
                    }                    
                }
            }
        }
        private static List<List<int>> calcCollision(List<Robot> _robots, List<Obstacle> _obses)
        {
            List<List<int>> result = new List<List<int>>();


            for(int i = 0; i<_robots.Count; i++)
            {
                result.Add(new List<int>());
                for (int j = 0; j<_obses.Count; j++)
                {
                    if ((_robots[i].Center.ToVector2() - _obses[j].Center.ToVector2()).Length() < (_robots[i].Rad + _obses[j].Rad))
                    {
                        result[i].Add(j);
                    }
                    else
                    {
                        result[i].Add(-1);
                    }
                }
            }
            return result;
        }

        private static void calcMovement(List<Robot> _robots, List<SpeedVect> _robotsCalcSpd)
        {
            for(int i=0; i < _robots.Count; i++)
            {
                float _alpha = _robots[i].Alpha;
                _alpha += _robotsCalcSpd[i].AngSpeed;                
                if (_alpha > Math.PI) { _alpha -= (float)(2 * Math.PI); }
                if (_alpha < -Math.PI) { _alpha += (float)(2 * Math.PI); }
                _robots[i].Alpha = _alpha;
                PointF _center = _robots[i].Center;
                _center.X += (float)Math.Cos(_alpha) * _robotsCalcSpd[i].LinSpeed;
                _center.Y += (float)Math.Sin(_alpha) * _robotsCalcSpd[i].LinSpeed;
                _robots[i].Center = _center;
            }
        }

        private static Matrix3x2 getRoboToBaseM(Robot robot)
        {
            return new Matrix3x2((float)Math.Cos(robot.Alpha), (float)Math.Sin(robot.Alpha),
                                            -(float)Math.Sin(robot.Alpha), (float)Math.Cos(robot.Alpha),
                                             robot.Center.X, robot.Center.Y);

        }


    }
}

