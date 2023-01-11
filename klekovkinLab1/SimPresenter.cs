using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace klekovkinLab1
{
    internal class SimPresenter
    {
        private readonly SimView _view;
        private readonly SimService _service;
        SpeedVect _speed = new SpeedVect(0, 0);
        public SimPresenter()
        {
            
            _view = new SimView();
            _service = new SimService(_view.FieldWidth, _view.FieldHeight);

            


            _view.tick += _service.ModelTick;
            _view.keyUp += KeyUpHandler;
            _view.keyDown += KeyDownHandler;
            _view.genBtnClick += _service.GenRandObses;
            _view.viewTick += RedrawView;
            _view.cleanBtnClick += _service.CleanObses;
            _view.setTgtPoint += AddRobotTgtPt;
            _view.addManObstacle += AddManObstacle;
            _view.changeControlMode += _service.ChangeControlMode;

            //_service.GenRandObses();
        }

        

        public void Run()
        {
            _view.Show();
        }

        void RedrawView()
        {
            List<Obstacle> obses = _service.GetObses();
            for (int i = 0; i< obses.Count; i++)
            {
                _view.DrawCurcle(Vector2.Transform(obses[i].Center.ToVector2(), _view.basisToWindow), (int)obses[i].Rad, new Pen(Color.Black, 2));
            }

            List<Robot> robots = _service.GetRobots();
            for(int i = 0; i< robots.Count; i++)
            {
                _view.DrawCurcle(Vector2.Transform(robots[i].Center.ToVector2(), _view.basisToWindow), (int)robots[i].Rad, new Pen(Color.Blue, 3));
                _view.DrawLine(Vector2.Transform(robots[i].Center.ToVector2(), _view.basisToWindow),
                    Vector2.Transform(new Vector2((int)robots[i].Rad, 0), _service.GetRoboToBaseM(i)*_view.basisToWindow), 
                    new Pen(Color.Blue, 3));
                foreach ((PointF, PointF) ray in robots[i].GetLidarRaysPs())
                {
                    _view.DrawLine(Vector2.Transform(ray.Item1.ToVector2(), _service.GetRoboToBaseM(i) * _view.basisToWindow), 
                        Vector2.Transform(ray.Item2.ToVector2(), _service.GetRoboToBaseM(i) * _view.basisToWindow), 
                        new Pen(Color.Red, 1));
                }                
            }
            if (!_service.GetRobotTgtPt(0).IsEmpty)
            {
                _view.DrawCurcle(Vector2.Transform(_service.GetRobotTgtPt(0).ToVector2(), _view.basisToWindow).toPointF(),
                    3,
                    new Pen(Color.Purple, 3));
            }
            foreach ((PointF, PointF) ray in robots[0].GetLidarRaysPs())
            {
                
                /*
                Vector2 beg = Vector2.Transform(ray.Item1.ToVector2(), _service.GetLidarTobaseM());
                Vector2 end = Vector2.Transform(ray.Item2.ToVector2(), _service.GetLidarTobaseM());
                _view.DrawLidarLine(Vector2.Transform(ray.Item1.ToVector2(), _service.GetLidarTobaseM()* _view.basisToLidarW),
                        Vector2.Transform(ray.Item2.ToVector2(), _service.GetLidarTobaseM()*_view.basisToLidarW),
                        new Pen(Color.Red, 1));
                */
                _view.DrawLidarP(Vector2.Transform(ray.Item2.ToVector2(), _service.GetLidarTobaseM(_view.LidarWidth, _view.LidarHeight/3) * _view.basisToLidarW).toPoint(), 
                    2, 
                    new Pen(Color.Red, 1));
            }

            foreach(List<Ray> obstacle in robots[0].Obses)
            {
                for(int i = 1; i<obstacle.Count; i++)
                {
                    _view.DrawLine(Vector2.Transform(obstacle[i-1].EndP.ToVector2(), _service.GetRoboToBaseM(0) * _view.basisToWindow).toPointF(),
                    Vector2.Transform(obstacle[i].EndP.ToVector2(), _service.GetRoboToBaseM(0) * _view.basisToWindow).toPointF(),
                    new Pen(Color.Green, 3));
                }
            }


        }

        void KeyDownHandler(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode) {
                case Keys.W:
                    _speed.LinSpeed = _service.RobotMCtrlSpeed;
                    break;
                case Keys.S:
                    _speed.LinSpeed = -_service.RobotMCtrlSpeed;
                    break;
                case Keys.A:
                    _speed.AngSpeed = _service.RobotMCtrlSpeed * 0.01f;
                    break;
                case Keys.D:
                    _speed.AngSpeed = -_service.RobotMCtrlSpeed * 0.01f;
                    break;
                default:
                    break;
            }
            _service.setRobotSpeed(_speed);            
        }

        void KeyUpHandler(object sender, KeyEventArgs e)
        {
            //SpeedVect _speed = new SpeedVect(0, 0);
            switch (e.KeyCode)
            {
                case Keys.W:
                    _speed.LinSpeed = 0;
                    break;
                case Keys.S:
                    _speed.LinSpeed = 0;
                    break;
                case Keys.A:
                    _speed.AngSpeed = 0;
                    break;
                case Keys.D:
                    _speed.AngSpeed = 0;
                    break;
                default:
                    break;
            }
            _service.setRobotSpeed(_speed);
        }

        void AddRobotTgtPt(Vector2 point)
        {
            _service.AddTgtPoint(Vector2.Transform(point, _view.basisToWindow).toPointF());
        }

        void AddManObstacle(Vector2 center, float rad)
        {
            _service.AddObstacle(Vector2.Transform(center, _view.basisToWindow).toPointF(), rad);
        }
    }
}
