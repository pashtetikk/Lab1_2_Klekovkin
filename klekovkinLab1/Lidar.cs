using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace klekovkinLab1
{
    internal class Lidar
    {
        float maxRayLengh = 10000;
        float startAnlge = 0;
        float stopAngle = MathF.PI;
        int numOfRays = 180;
        float lidarRad = 10;
        public const float Sense = 300;
        
        List<Ray> rays = new List<Ray>();

        public Lidar(float maxRayLengh, float startAnlge, float stopAngle, int numOfRays, float lidarRad)
        {
            this.maxRayLengh = maxRayLengh;
            this.startAnlge = startAnlge;
            this.stopAngle = stopAngle;
            this.numOfRays = numOfRays;
            this.lidarRad = lidarRad;
            
            for (int i = 0; i <= numOfRays; i++)
            {
                rays.Add(new Ray(lidarRad, startAnlge+(stopAngle - startAnlge)*i/numOfRays, maxRayLengh));
            }
            
        }

        public (PointF startP, PointF endP)[] getRaysPs()
        {
            (PointF, PointF)[] raysP = new (PointF, PointF)[numOfRays];
            for(int i = 0; i<numOfRays; i++)
            {
                raysP[i].Item1 = rays[i].StartP;
                raysP[i].Item2 = rays[i].EndP;  
            }            
            return raysP;
        }

        public void setRayLengh(int id, float lengh)
        {
            if(id > 0 && id < rays.Count) 
            {
                rays[id].Lengh = lengh;
            }
        }
        public List<Ray> Rays
        {
            get
            {
                return rays;
            }
        }

    }
}
