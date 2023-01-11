using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static klekovkinLab1.FieldObj;

namespace klekovkinLab1
{
    class Obstacle:FieldObj
    {
        public Obstacle() { }
        public Obstacle(PointF _center, float _alpha, Shapes _shape, float _rad)
            : base(_center, _alpha, _shape, _rad, false)
        {

        }

        public Obstacle(float _x, float _y, float _alpha, Shapes _shape, float _rad)
            : base(_x, _y, _alpha, _shape, _rad, false)
        {

        }

        public Obstacle(Obstacle obj)
        {
            this.Center = obj.Center;
            this.speedVect = obj.speedVect;
            this.alpha = obj.alpha;
            this.shape = obj.shape;
            this.movable = obj.movable;
            this.rad = obj.rad;
        }
    }
}
