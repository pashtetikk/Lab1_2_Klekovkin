using System;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using static klekovkinLab1.FieldObj;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace klekovkinLab1
{
    class Field
    {
        Random rand = new Random();
        List<Robot> robots = new List<Robot>();
        List<Obstacle> obses = new List<Obstacle>();

        List<SpeedVect> robotsTgtSpd = new List<SpeedVect>();
        List<PointF> robotsTgtPt = new List<PointF>();
        List<SpeedVect> robotsCalcSpd = new List<SpeedVect>();

        int _width, _height = 0;

        public Field(int width, int height) { 
            _width = width;
            _height = height;

            robots.Add(new Robot(10, 10, 0, 15));
            //obses.Add(new Obstacle(0, 0, 0, Shapes.ROUND, 10));

            if (robots.Count > robotsTgtSpd.Count)
            {
                for (int i = 0; i<= robots.Count - robotsTgtSpd.Count; i++)
                {
                    robotsTgtSpd.Add(new SpeedVect(0, 0));
                    robotsTgtPt.Add(new PointF());
                    robotsCalcSpd.Add(new SpeedVect(0, 0));
                }
                //int itemsToAdd = robots.Count - robotsTgtSpd.Count;
                //robotsTgtSpd.AddRange(new List<SpeedVect>(itemsToAdd));
                //robotsTgtPt.AddRange(new List<PointF>(itemsToAdd));
                //robotsCalcSpd.AddRange(new List<SpeedVect>(itemsToAdd));
            }
        }

        public SpeedVect GetRobotsTgtSpd(int id)
        {
            if(id >= robotsTgtSpd.Count) {
                return null;
            }

            return robotsTgtSpd[id]; 
        }

        public void SetRobotsTgtSpd(int id, SpeedVect _tgtSpd)
        {
            if ((id < robotsTgtSpd.Count)&&(id >=0))
            {
                robotsTgtSpd[id] = _tgtSpd;
            }
        }

        public void SetRobotsTgtSpd(int id, Vector2 _tgtSpd)
        {
            if ((id < robotsTgtSpd.Count) && (id >= 0))
            {
                robotsTgtSpd[id].setFromVec2(_tgtSpd);
            }
        }

        public PointF GetRobotsTgtPt(int id)
        {
            if (id >= robotsTgtSpd.Count)
            {
                return new PointF();
            }

            return robotsTgtPt[id];
        }

        public void SetRobotsTgtPt(int id, PointF _tgtPt)
        {
            if ((id < robotsTgtSpd.Count) && (id >= 0))
            {
                robots[id].setTgtPt(_tgtPt.ToVector2());
                robotsTgtPt[id] = _tgtPt;
                
            }
        }



        public List<Obstacle> Obses
        {
            get { return obses; }
        }

        public List<Robot> Robots
        {
            get { return robots; }
            set { robots = value; }
        }
        
        public void handler()
        {
            //Physics.calcCollision(robots, obses, robotsTgtSpd, robotsCalcSpd);
            //Physics.calcMovement(robots, robotsCalcSpd);

            int i = 0;
            foreach(Robot robot in robots)
            {
                robot.Handler();
                if (!robot.ManualControl)
                {
                    robotsTgtSpd[i] = robot.TgtSpeed;
                }
                i++;
            }


            
            Physics.calcPhysics(robots, obses, robotsTgtSpd);
            Physics.calcLidar(robots, obses);

        }

        public void ClearObses()
        {
            obses.Clear();
        }

        public void GenRandObses(int quanity)
        {
            for(int i = 0; i<quanity; i++)
            {
                int x = rand.Next(0, _width);
                int y = rand.Next(0, _height);
                int rad = rand.Next(1, 50);
                int shape = rand.Next(1, 5);
                obses.Add(new Obstacle(x, y, 0, Shapes.ROUND, rad));
                foreach(Robot robot in robots)
                {
                    if((obses.Last<Obstacle>().Center.ToVector2() - robot.Center.ToVector2()).Length() < robot.Rad + obses.Last<Obstacle>().Rad)
                    {
                        obses.RemoveAt(obses.Count - 1);
                        i--;
                        break;
                    }
                }
                
            }

        }

        public void AddObstacle(PointF center, float rad)
        {
            obses.Add(new Obstacle(center, 0, Shapes.ROUND, rad));
        }


        

    }
}
