// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// PROJECT INFORMATION
// Project:         Lab02 - Missile Command  
// Date:            Oct.21.2020
// Author:          Austin Klevgaard
// Instructor:      Shane Kelemen   
// Submission Code: 1201_2300_L02
//
// Description:     Recreation of the classic game Missile Commando, except with way worse graphics, and way less functionality.

// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// FILE INFORMATION
// Class:           CMPE 2300 A01  
// Last Edit:       Oct.21.2020
// Description:     Missile class file - contains all the information to make a missile class. Information includes the size of the missile
//                  how fast it is moving, and it's angle and current path length towards its target. Both friendly and enemy missiles 
//                  can be generated. Included Methods will move and show the missile in a canvas. An equals override is used to determine
//                  if friendly missiles have overlapped and destroyed an enemy missile.
//
// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using GDIDrawer;

namespace KlevgaardA_Lab02_Missiles
{
    class Missile
    {
        private static CDrawer _canvas;                 //CDrawer canvas to be shared by all Missile classes
        private static int _explosionRadius;            //static value for the maximum radius the missile will have after explosion
        private static int _enemyInitialRadius = 5;     //static int for initial enemy missile radius Makes edge detection work better, especially in predicates that require a static member
        private static int _friendlyInitialRadius = 10;
        private static Random _rng;                     //random number generator

        private Point _origin;                          //source of the missile
        private double _angle;                          //angle the missile moves at
        private double _pathLength;                     //current path length of the missile
        private double _heightDestination;              //the height that the missile must reach to explode
        private int _radius = 5;                            //starting radius of the missiles
        private int _alphaVal = 255;                    //starting opacity of the missile. Initialized to 255.
        private int _speed;                             //current speed of the missile (how much it moves each frame)

        public double Angle                             //public property to access the current angle of the missile
        {
            get { return _angle; }
            set { _angle = value; }
        }
        public bool FriendlyMissile                     //public automatic property that returns alliance information for the missile
        {   
            get;
            private set;
        }

        public static CDrawer Canvas                    //public property to set the missile canvas
        {
            set { _canvas = value; }
        }

        public static int Explosion                     //public property to adjust the size of the explosion
        {
            set { _explosionRadius = value; }
        }
        
        public bool FinishedExplosion                   //public property for if the missile has finished exploding
        {
            get { return _alphaVal == 0; }
        }
        public bool TargetReached                       //public property to tell if a missile has reached it's destination
        {
            get
            {
                if (FriendlyMissile == false) return Where().Y + _radius >= _canvas.ScaledHeight && _radius == _enemyInitialRadius;
                else return Where().Y + _radius >= _canvas.ScaledHeight && _radius == _friendlyInitialRadius;
            }
        }

        /// <summary>
        /// Static constructor for Missile class. Initializes the random number generator, sets initial explosion size 
        /// and assigns a canvas to the game.
        /// </summary>
        static Missile()
        {
            _rng = new Random();
            Explosion = 50;
            Canvas = null;
            
        }
        /// <summary>
        /// Default constructor creates enemy Missiles. These missiles start at some location along the top of the screen
        /// and are targetted to the bottom of the canvas at a random angle
        /// </summary>
        public Missile()
        {
            //sets the start point of the missile. The reason why I have the origin of enemy missiles set from 5 -> width - 5
            //is due to how enemy missile generation interacts with my "wall bounce" enhancement. Basically, if wall bounce is 
            //enabled then whenever a ball is created  within 5 units (1/2 radius) of a wall boundary, it considers itself
            //"past the wall" as it should, however it continuously "bounces" back and forth quickly, drops to 
            //the bottom boundary, and kills all the player lives in 5 cycles. As afix, I've just made sure that enemy missiles
            //won't be generated in the slim areas that lead to that bug.
            _origin = new Point(_rng.Next(6,_canvas.ScaledWidth - 6),0);    

            _angle = _rng.NextDouble() * (5 * Math.PI / 4 - 3 * Math.PI / 4) + 3 * Math.PI / 4; //rng(0-1) * (upperlimit - lowerlimit) + min value
            _pathLength = 5;            //sets the initial path length of the missile
            _heightDestination = _canvas.ScaledHeight - 1;  //set sthe target of the missile
            _speed = 5;                 //initial speed
            FriendlyMissile = false;    //makes it an enemy missile
        }
        /// <summary>
        /// Constructor Overload for a new enemy missile after it bounces off a wall. Requires that wall bounce be enabled in the main form
        /// Works by passing in the point of a missile touching the wall, and replacing it with a new missile at the same location
        /// but has an inverted angle direction.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="angle"></param>
        public Missile(Point start, double angle)
        {
            _origin = start;
            _angle = angle * -1;
            _pathLength = 5;
            _heightDestination = _canvas.ScaledHeight - 1;
            _speed = 5;
            FriendlyMissile = false;
        }

        /// <summary>
        /// Missile consturctor for friendly missiles. Accepts a point from a location that the user clicked in the canvas.
        /// A friendly missile is then created along the center of the bottom of the canvas, and directed towards the clicked "target".
        /// Friendly missiles move faster and are larger than enemy missiles. This Method is also used in the Shock and Awe Enhancement
        /// to send out a barrage of friendly missiles
        /// </summary>
        /// <param name="target"></param>
        public Missile(Point target)
        {
            _origin = new Point((_canvas.ScaledWidth - 1) / 2, _canvas.ScaledHeight - 1);
            _angle = Math.Atan( -1.0 * (target.X - _origin.X) / (target.Y - _origin.Y));
            _heightDestination = target.Y;
            _radius = 10;
            _speed = 20;
            FriendlyMissile = true;
        }

        /// <summary>
        /// helper method to determine where a missile is in the canvas based on it's origin, angle, and pathlength
        /// returns a Point data type that contains its X and Y position in the canvas
        /// </summary>
        /// <returns></returns>
        private Point Where()
        {
            int currentX = (int)(Math.Sin(_angle) * _pathLength) + _origin.X;
            int currentY = (int)(-1 * Math.Cos(_angle) * _pathLength) + _origin.Y;

            return new Point(currentX, currentY);
        }
        /// <summary>
        /// static public overload for Where that accepts a Missile as an argument. This allows Where to be called on any missile
        /// in the main form, which is useful for the wall bounce detection enhancement
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        static public  Point Where(Missile arg)
        {
            int currentX = (int)(Math.Sin(arg._angle) * arg._pathLength) + arg._origin.X;
            int currentY = (int)(-1 * Math.Cos(arg._angle) * arg._pathLength) + arg._origin.Y;

            return new Point(currentX, currentY);
        }

        /// <summary>
        /// Method to move the Missiles in the canvas. Everytime Move is called the missile is moved along the angle of it's pathlength
        /// by a factor of its speed. If it's destination has been reached then the missile is exploded, and everytime move is called
        /// It will increase the step of the missile explosion until it fades out.
        /// </summary>
        public void Move()
        {
            //If missile is an enemy keep moving it until it reaches the bottom of the screen
            if (FriendlyMissile ==  false)
            {
                if (Where().Y < _heightDestination -_radius)
                {
                    _pathLength += _speed;
                }
                //if target is reached then explode the missile
                else
                {
                    ExplodeMissile();
                }
                
            }
            //if missile is friendly then determin where it is
            else
            {
                //if the current location is less than the target, Move the missile according to it's speed. If the target is reached then 
                //explode it
                Point currentLocation = Where();
                if (currentLocation.Y > _heightDestination)
                {
                    _pathLength += _speed;
                }
                else
                {
                    ExplodeMissile();
                }
            }
        }

        /// <summary>
        /// Helper method that moves an exploded missile along an explosion step
        /// </summary>
        private void ExplodeMissile()
        {
            _speed = 0;
            if (_radius < _explosionRadius || _alphaVal > 0)
            {
                if (_radius < _explosionRadius) _radius += 5;
                if (_alphaVal > 0) { _alphaVal -= 10; }
                if (_alphaVal < 0) { _alphaVal = 0; }
            }
        }
        /// <summary>
        /// Public method that is used to draw a missile in the canvas. This will draw both friendly and enemy missiles,
        /// and will change opacity of the missile according to it's current alpha value
        /// </summary>
        public void Show()
        {
            //probably want to change the color so that it uses alhpaVal to fade out, not just disappear
            if (FriendlyMissile == false)
            {
                _canvas.AddCenteredEllipse(Where(), _radius * 2, _radius * 2, Color.FromArgb(_alphaVal, 255, 0, 0));
                _canvas.AddLine(_origin, _pathLength, _angle, Color.FromArgb(_alphaVal,255,0,0), 1);
            }
            if (FriendlyMissile == true)
            {
                _canvas.AddCenteredEllipse(Where(), _radius * 2, _radius * 2, Color.FromArgb(_alphaVal,0,255,0));
                _canvas.AddLine(_origin, _pathLength, _angle, Color.FromArgb(_alphaVal, 0, 255, 0), 1);
            }
        }
        /// <summary>
        /// Helper Method which determines if two missiles currently touch or overlap each other's areas. 
        /// Overlap determined by Pythagoras theorum
        /// </summary>
        /// <param name="firstMissile">first Missile argument</param>
        /// <param name="secondMissile">second missile to compare to the first</param>
        /// <returns></returns>
        private double GetMissileDifference(Missile firstMissile, Missile secondMissile)
        {
            Point locMis1 = Where(firstMissile);
            Point locMis2 = Where(secondMissile);

            return Math.Sqrt(Math.Pow((locMis2.Y - locMis1.Y), 2) + Math.Pow((locMis2.X - locMis1.X), 2));

        }
        /// <summary>
        /// public override for equals. Missiles are considered equal if their radius overlap within the canvas.
        /// However, if the missile is an enemy and it has reached it's target it will never be equal to anything,
        /// because it has already began to explode and the damage is done.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            //if obj passed is not ball method is invalid
            if (!(obj is Missile arg)) return false;

            //if the missile is a foe and has reached the bottom then it can't be equal to anything, it is exploding
            if (this.FriendlyMissile == false && Where().Y + _enemyInitialRadius >= _canvas.ScaledHeight - 1)
            {
                return false;
            }
            else
            {
                if (GetMissileDifference(this, arg) < (_radius + arg._radius))                   
                {
                    return true;
                }
                else return false;
            }
        }
        /// <summary>
        /// Override requied for override Equals
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            //return base.GetHashCode();
            return 1;
        }

        //******************************************************************************************
        //Predicates
        //******************************************************************************************
        /// <summary>
        /// Returns true if the missile argument is touching either left or right side of the canvas
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static bool IsPastHorizontal(Missile arg)
        {
            return arg.Where().X - _enemyInitialRadius <= 0 || arg.Where().X + _enemyInitialRadius >= _canvas.ScaledWidth - 1;
        }

        /// <summary>
        /// returns true if the missile arugment is past the edge of the canvas.
        /// I didn't use this predicate in my program however
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static bool IsPastVertical(Missile arg)
        {
            return arg.Where().Y < 0 || arg.Where().Y > _canvas.ScaledHeight - 1;
        }
        
        /// <summary>
        /// Predicate to determine if a missile argument has finished exploding and has faded away
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static bool doneExplosion(Missile arg)
        {
            return arg._alphaVal == 0;
        }
        
    }
}
