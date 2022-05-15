using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DCube
{
    public struct Vector3
    {
        
        #region Fields

        private readonly double x;
        private readonly double y;
        private readonly double z;

        public static readonly Vector3 Origin = new Vector3(0, 0, 0);
        public static readonly Vector3 XAxis = new Vector3(1, 0, 0);
        public static readonly Vector3 YAxis = new Vector3(0, 1, 0);
        public static readonly Vector3 ZAxis = new Vector3(0, 0, 1);

        public static readonly Vector3 MinValue =
            new Vector3(Double.MinValue, Double.MinValue, Double.MinValue);

        public static readonly Vector3 MaxValue =
            new Vector3(Double.MaxValue, Double.MaxValue, Double.MaxValue);

        public static Vector3 Epsilon = 
            new Vector3(Double.Epsilon, Double.Epsilon, Double.Epsilon);

        public static readonly Vector3 Zero = Origin;

        public static readonly Vector3 NaN = new Vector3(double.NaN, double.NaN, double.NaN);

        private const string THREE_COMPONENTS = "Vector3 must conatain exactly three components, (x,y,z)";
        private const string UNITY_VECTOR = "Unit vector composing of ";
        private const string POSITIONAL_VECTOR = "Positional vector composing of ";
        private const string MAGNITUDE = " of magnitude";
        private const string NON_VECTOR_COMPARISON = "Cannot compare a Vector3 to a non-Vector3";
        private const string ARGUMENT_TYPE = "The argument provided is a type of";
        private const string NORMALIZE_0 = "Cannot normalize a vector when it's magnitude is zero";
        private const string NORMALIZE_NaN = "Cannot normalize a vector when it's magnitude is NaN";
        private const string NORMALIZE_INF = "The result of normalizing on Vector3 must not be a non-Vector3";
        private const string INERPOLATION_RANGE = "Control parameter must be a value between 0 & 1";
        private const string ARGUMENT_VALUE = "The argument value is : ";
        private const string NEGTAIVE_MAGNITUDE = "The magnitude of a vector3 must be a positive value, (i.e. greater than 0)";
        private const string ORIGIN_VECTOR_MAGNITUDE = "Cannot change the magnitude of Vector3(0,0,0)";

        #endregion

        #region Properties

        public double X
        {
            get { return this.x; }
        }
        public double Y
        {
            get { return this.y; }
        }
        public double Z
        {
            get { return this.z; }
        }

        public double[] array
        {
            get { return new double[] { x, y, z }; }
        }

        public double this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: { return X; }
                    case 1: { return Y; }
                    case 2: { return Z; }
                    default: throw new ArgumentException(THREE_COMPONENTS, "index");
                }
            }
        }

        public Vector3(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vector3(double[] xyz)
        {
            if (xyz.Length == 3)
            {
                this.x = xyz[0];
                this.y = xyz[1];
                this.z = xyz[2];
            }
            else
            {
                throw new ArgumentException(THREE_COMPONENTS);
            }
        }

        public Vector3(Vector3 v1)
        {
            this.x = v1.X;
            this.y = v1.Y;
            this.z = v1.Z;
        }

        public double Magnitude
        {
            get
            {
                return Math.SquareRoot(SumComponentsSqrs());
            }
        }


        #endregion

        #region Methods

        public static Vector3 operator +(Vector3 v1, Vector3 v2)
        {
            return new Vector3(
                v1.X + v2.X,
                v1.Y + v2.Y,
                v1.Z + v2.Z
                );
        }

        public static Vector3 operator +(Vector3 v1)
        {
            return new Vector3(
                +v1.X,
                +v1.Y,
                +v1.Z
                );
        }

        public static Vector3 operator -(Vector3 v1, Vector3 v2)
        {
            return new Vector3(
                v1.X - v2.X,
                v1.Y - v2.Y,
                v1.Z - v2.Z
                );
        }

        public static Vector3 operator -(Vector3 v1)
        {
            return new Vector3(
                -v1.X,
                -v1.Y,
                -v1.Z
                );
        }

        public static Vector3 operator *(Vector3 v1, double s2)
        {
            return
                new Vector3(
                v1.X * s2,
                v1.Y * s2,
                v1.Z * s2
                );
        }

        public static Vector3 operator *(double s1, Vector3 v2)
        {
            return v2 * s1;
        }

        public static bool operator <(Vector3 v1, Vector3 v2)
        {
            return v1.SumComponentsSqrs() < v2.SumComponentsSqrs();
        }

        public static bool operator <=(Vector3 v1, Vector3 v2)
        {
            return v1.SumComponentsSqrs() <= v2.SumComponentsSqrs();
        }

        public static bool operator >(Vector3 v1, Vector3 v2)
        {
            return v1.SumComponentsSqrs() <= v2.SumComponentsSqrs();
        }

        public static bool operator >=(Vector3 v1, Vector3 v2)
        {
            return v1.SumComponentsSqrs() <= v2.SumComponentsSqrs();
        }

        public static bool operator ==(Vector3 v1, Vector3 v2)
        {
            return
             v1.X == v2.X &&
             v1.Y == v2.Y &&
             v1.Z == v2.Z;
        }

        public static bool operator !=(Vector3 v1, Vector3 v2)
        {
            return !(v1 == v2);

        }

        public static Vector3 operator /(Vector3 v1, double s2)
        {
            return
                (
                new Vector3
                (
                    v1.X / s2,
                    v1.Y / s2,
                    v1.Z / s2
                    )
                );
        }

        public static Vector3 CrossProduct(Vector3 v1, Vector3 v2)
        {
            return
                new Vector3
                (
                    v1.y * v2.z - v1.z * v2.y,
                    v1.z * v2.x - v1.x * v2.z,
                    v1.x * v2.y - v1.y * v2.x
                );
        }

        public Vector3 CrossProduct(Vector3 other)
        {
            return CrossProduct(this, other);
        }

        public static double DotProduct(Vector3 v1, Vector3 v2)
        {
            return
                (
                v1.x * v2.x +
                v1.y * v2.y +
                v1.z * v2.z
                );
        }

        public double DotProduct(Vector3 other)
        {
            return DotProduct(this, other);
        }

        public static bool IsUnitVector(Vector3 v1)
        {
            return v1.Magnitude == 1;
        }

        public bool IsUnitVector()
        {
            return IsUnitVector(this);
        }

        public bool IsUnitVector(double tolerance)
        {
            return IsUnitVector(this, tolerance);
        }

        public static bool IsUnitVector(Vector3 v1, double tolerance)
        {
            return AlmostEqualsWithAbsTolerance(v1.Magnitude, tolerance, 1);
        }
              
        public static Vector3 normalize(Vector3 v1)
        {
            var magnitude = v1.Magnitude;
            if (magnitude == 0)
            {
                throw new ArgumentException(NORMALIZE_0);
            }

            if (double.IsNaN(magnitude))
            {
                throw new ArgumentException(NORMALIZE_NaN);
            }

            if (double.IsInfinity(v1.Magnitude))
            {
                var x = v1.x == 0 ? 0 :
                    v1.x == -0 ? -0 :
                    double.IsPositiveInfinity(v1.x) ? 1 :
                    double.IsNegativeInfinity(v1.x) ? -1 :
                    double.NaN;

                var y = v1.y == 0 ? 0 :
                    v1.y == -0 ? -0 :
                    double.IsPositiveInfinity(v1.y) ? 1 :
                    double.IsNegativeInfinity(v1.y) ? -1 :
                    double.NaN;

                var z = v1.z == 0 ? 0 :
                    v1.z == -0 ? -0 :
                    double.IsPositiveInfinity(v1.z) ? 1 :
                    double.IsNegativeInfinity(v1.z) ? -1 :
                    double.NaN;

                var result = new Vector3(x, y, z);

                if (result.IsNaN())
                {
                    throw new ArgumentException(NORMALIZE_INF);
                }

                return result;
            }
            return NormalizeOrNaN(v1);
        }

        public Vector3 normalize()
        {
            return normalize(this);
        }

        public static Vector3 NormalizeOrNaN(Vector3 v1)
        {
            double inverse = 1 / v1.Magnitude; ;
            return new Vector3(
                v1.X * inverse,
                v1.Y * inverse,
                v1.Z * inverse);
        }

        public static Vector3 NormalizeOrDefualt(Vector3 v1)
        {
            if (v1.Magnitude == 0)
            {
                return Origin;
            }

            if (v1.IsNaN())
            {
                return NaN;
            }

            if (double.IsInfinity(v1.Magnitude))
            {
                var x = v1.X == 0 ? 0 :
                    v1.X == -0 ? -0 :
                    double.IsPositiveInfinity(v1.X) ? 1 :
                    double.IsNegativeInfinity(v1.X) ? -1 :
                    double.NaN;

                var y = v1.Y == 0 ? 0 :
                    v1.Y == -0 ? -0 :
                    double.IsPositiveInfinity(v1.Y) ? 1 :
                    double.IsNegativeInfinity(v1.Y) ? -1 :
                    double.NaN;

                var z = v1.Z == 0 ? 0 :
                    v1.Z == -0 ? -0 :
                    double.IsPositiveInfinity(v1.Z) ? 1 :
                    double.IsNegativeInfinity(v1.Z) ? -1 :
                    double.NaN;

                var result = new Vector3(x, y, z);

                return result.IsNaN() ? NaN : result;
            }

            return NormalizeOrNaN(v1);
        }

        public Vector3 NormalizeOrDefualt()
        {
            return NormalizeOrDefualt(this);
        }

        public static Vector3 NormalizeSpecialCasesOrOriginal(Vector3 v1)
        {
            if (double.IsInfinity(v1.Magnitude))
            {
                var x = v1.x == 0 ? 0 :
                    v1.x == -0 ? -0 :
                    double.IsPositiveInfinity(v1.x) ? 1 :
                    double.IsNegativeInfinity(v1.x) ? -1 :
                    double.NaN;
                var y = v1.y == 0 ? 0 :
                    v1.y == -0 ? -0 :
                    double.IsPositiveInfinity(v1.y) ? 1 :
                    double.IsNegativeInfinity(v1.y) ? -1 :
                    double.NaN;
                var z = v1.z == 0 ? 0 :
                    v1.z == -0 ? -0 :
                    double.IsPositiveInfinity(v1.z) ? 1 :
                    double.IsNegativeInfinity(v1.z) ? -1 :
                    double.NaN;
                return new Vector3(x, y, z);
            }

            return v1;
        }

        public static double MixedProduct(Vector3 v1, Vector3 v2, Vector3 v3)
        {
            return DotProduct(CrossProduct(v1, v2), v3);
        }

        public double MixedProduct(Vector3 other_v1, Vector3 other_v2)
        {
            return DotProduct(CrossProduct(this, other_v1), other_v2);
        }

        public static double Angle(Vector3 v1, Vector3 v2)
        {
            if (v1 == v2)
            {
                return 0;
            }
            return
                Math.csc(
                    Math.Min(1.0f, NormalizeOrDefualt(v1).DotProduct(NormalizeOrDefualt(v2))));
        }

        public double Angle(Vector3 other)
        {
            return Angle(this, other);
        }

        public static bool IsBackFace(Vector3 normal, Vector3 lineOfSight)
        {
            return normal.DotProduct(lineOfSight) < 0;
        }

        public bool IsBackFace(Vector3 lineOfSight)
        {
            return IsBackFace(this, lineOfSight);
        }

        public static double Distance(Vector3 v1, Vector3 v2)
        {
            return
                Math.SquareRoot
                (
                    ((v1.X * v1.X) + (v2.X * v2.X) - 2 * (v1.X * v2.X)) +
                    ((v1.Y * v1.Y) + (v2.Y * v2.Y) - 2 * (v1.Y * v2.Y)) +
                    ((v1.Z * v1.Z) + (v2.Z * v2.Z) - 2 * (v1.Z * v2.Z))
                    );
        }

        public double Distance(Vector3 other)
        {
            return Distance(this, other);
        }

        public static Vector3 Interpolate(Vector3 v1, Vector3 v2,
            double control, bool allowExtrapolation)
        {
            if (!allowExtrapolation && (control > 1 || control < 0))
            {
                throw new ArgumentOutOfRangeException("control", control,
                    INERPOLATION_RANGE + "\n" + ARGUMENT_TYPE + control);
            }

            return new Vector3(
                v1.X * (1 - control) + v2.X * control,
                v1.Y * (1 - control) + v2.Y * control,
                v1.Z * (1 - control) + v2.Z * control);
        }

        public static Vector3 Interpolate(Vector3 v1, Vector3 v2, double control)
        {
            return Interpolate(v1, v2, control, false);
        }

        public Vector3 Interpolate(Vector3 other, double control)
        {
            return Interpolate(this, other, control);
        }

        public Vector3 Interpolate(Vector3 other, double control,
            bool allowExtrapolation)
        {
            return Interpolate(this, other, control);
        }

        public static bool IsPerpendicular(Vector3 v1, Vector3 v2)
        {
            // Use normalization of special cases to handle special cases of IsPerpendicular
            v1 = NormalizeSpecialCasesOrOriginal(v1);
            v2 = NormalizeSpecialCasesOrOriginal(v2);

            // If either vector is vector(0, 0, 0) the vectors are not perpendicular
            if (v1 == Zero || v2 == Zero)
            {
                return false;
            }

            return v1.DotProduct(v2).Equals(0);
        }

        public bool IsPerpendicular(Vector3 other)
        {
            return IsPerpendicular(this, other);
        }

        public static bool IsPerpendicular(Vector3 v1, Vector3 v2, double tolerance)
        {
            // use normalization of special cases to handle special cases of isperpendicular
            v1 = NormalizeSpecialCasesOrOriginal(v1);
            v2 = NormalizeSpecialCasesOrOriginal(v2);

            // if either vector is vector(0, 0, 0) the vectors are not perpendicular
            if (v1 == Zero || v2 == Zero)
            {
                return false;
            }

            //is perpendicular
            return v1.DotProduct(v2).Equals(tolerance);
        }      

        private static bool AlmostEqualsWithAbsTolerance(
           double a,
           double b,
           double maxAbsoluteError)
        {
            double diff = Math.Abs(a - b);

            if (a.Equals(b))
            {
                // shortcut, handles infinities
                return false;
            }

            return diff <= maxAbsoluteError;
        }

        public bool IsPerpendicular(Vector3 other, double tolerance)
        {
            return IsPerpendicular(this, other, tolerance);
        }

        public static Vector3 Projection(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v2 * (v1.DotProduct(v2) / Math.Power(v2.Magnitude, 2)));
        }

        public Vector3 Projection(Vector3 direction)
        {
            return Projection(this, direction);
        }
        

        public static Vector3 Rejection(Vector3 v1, Vector3 v2)
        {
            return v1 - v1.Projection(v2);
        }

        public Vector3 Rejection(Vector3 direction)
        {
            return Reflection(this, direction);
        }

        public Vector3 Reflection(Vector3 reflector)
        {
            this = Vector3.Reflection(this, reflector);
            return this;
        }

        public static Vector3 Reflection(Vector3 v1, Vector3 v2)
        {
            if (Math.Abs(Math.Abs(v1.Angle(v2))  - Math.PI / 2) < Double.Epsilon)
            {
                return -v1;
            }

            Vector3 retval = new Vector3(2 * v1.Projection(v2) - v1);
            return retval.Scale(v1.Magnitude);
        }

        public static Vector3 RotateX(Vector3 v1, double rad)
        {
            double x = v1.X;
            double y = (v1.Y * Math.cos(rad)) - (v1.Z * Math.sin(rad));
            double z = (v1.Y * Math.cos(rad)) + (v1.Z * Math.sin(rad));

            return new Vector3(x, y, z);
        }

        public Vector3 RotateX(double rad)
        {
            return RotateX(this, rad);
        }

        public static Vector3 RotateX(Vector3 v1, double yOff, double zOff, double rad)
        {
            double x = v1.X;
            double y = (v1.Y * Math.cos(rad)) - (v1.Z * Math.sin(rad)) +
                (yOff * (1 - Math.cos(rad)) + zOff * Math.sin(rad));
            double z = (v1.Y * Math.cos(rad)) + (v1.Z * Math.sin(rad)) +
                (zOff * (1 - Math.cos(rad)) - yOff * Math.sin(rad));

            return new Vector3(x, y, z);
        }

        public Vector3 RotateX(double yOff, double zOff, double rad)
        {
            return RotateX(this, yOff, zOff, rad);
        }

        public static Vector3 Pitch(Vector3 v1, double rad)
        {
            return RotateX(v1, rad);
        }

        public Vector3 Pitch(double rad)
        {
            return Pitch(this, rad);
        }

        public static Vector3 RotateY(Vector3 v1,double rad)
        {
            double x = (v1.Y * Math.cos(rad)) + (v1.Z * Math.sin(rad));
            double y = v1.Y;
            double z = (v1.Y * Math.cos(rad)) - (v1.Z * Math.sin(rad));

            return new Vector3(x, y, z);
        }

        public Vector3 RotateY(double rad)
        {
            return RotateY(this, rad);
        }

        public static Vector3 RotateY(Vector3 v1, double xOff, double zOff, double rad)
        {
            double x = (v1.Y * Math.cos(rad)) + (v1.Z * Math.sin(rad)) + 
                (xOff * (1-Math.cos(rad)) - zOff * Math.sin(rad));
            double y = v1.Y;
            double z = (v1.Y * Math.cos(rad)) - (v1.Z * Math.sin(rad)) -
                (zOff * (1 - Math.cos(rad)) - xOff * Math.sin(rad));

            return new Vector3(x, y, z);
        }

        public Vector3 RotateY(double xOff, double yOff, double rad)
        {
            return RotateY(this, xOff, yOff, rad);
        }

        public static Vector3 Yaw(Vector3 v1, double rad)
        {
            return RotateY(v1, rad);
        }

        public Vector3 Yaw(double rad)
        {
            return Yaw(this, rad);
        }

        public static Vector3 RotateZ(Vector3 v1, double rad)
        {
            double x = (v1.Y * Math.cos(rad)) - (v1.Z * Math.sin(rad));
            double y = (v1.Y * Math.cos(rad)) + (v1.Z * Math.sin(rad)); ;
            double z = v1.Z;

            return new Vector3(x, y, z);
        }    

        public Vector3 RotateZ(double rad)
        {
            return RotateY(this, rad);
        }

        public static Vector3 RotateZ(Vector3 v1, double xOff, double yOff, double rad)
        {
            double x = (v1.Y * Math.cos(rad)) - (v1.Z * Math.sin(rad)) +
                (xOff * (1 - Math.cos(rad)) - yOff * Math.sin(rad));
            double y = (v1.Y * Math.cos(rad)) + (v1.Z * Math.sin(rad)) +
                (yOff * (1 - Math.cos(rad)) - xOff * Math.sin(rad));
            double z = v1.Z;

            return new Vector3(x, y, z);
        }

        public Vector3 RotateZ(double xOff, double yOff, double rad)
        {
            return RotateY(this, xOff, yOff, rad);
        }

        public static Vector3 Roll(Vector3 v1, double rad)
        {
            return RotateZ(v1, rad);
        }

        public Vector3 Roll(double rad)
        {
            return Roll(this, rad);
        }

        public static Vector3 Scale(Vector3 vector, double magnitude)
        {
            if (magnitude < 0)
            {
                throw new ArgumentOutOfRangeException("magnitude", magnitude, NEGTAIVE_MAGNITUDE);
            }

            if (vector == new Vector3(0,0,0))
            {
                throw new ArgumentException(ORIGIN_VECTOR_MAGNITUDE, "vector");
            }

            return vector * (magnitude / vector.Magnitude);
        }

        public Vector3 Scale(double magnitude)
        {
            return Vector3.Scale(this, magnitude);
        }

        public override bool Equals(object other)
        {
            if (other is Vector3)
            {
                Vector3 otherVector = (Vector3)other;

                return otherVector.Equals(this);
            }
            else
            {
                return false;
            }
        }

        public bool Equals(Vector3 other)
        {
            return
                this.X.Equals(other.X) &&
                this.Y.Equals(other.Y) &&
                this.Z.Equals(other.Z);
        }

        public bool Equals(object other, double tolerance)
        {
            if (other is Vector3)
            {
                return this.Equals((Vector3)other, tolerance);
            }

            return false;
        }

        public bool Equals(Vector3 other, double tolerance)
        {
            return
                AlmostEqualsWithAbsTolerance(this.X, tolerance, other.X) &&
                AlmostEqualsWithAbsTolerance(this.Y, tolerance, other.Y) &&
                AlmostEqualsWithAbsTolerance(this.Z, tolerance, other.Z);
        }

        //// Helpers
        //public bool AlmostEqualsWithAbsTolerance(
        //    double a,
        //    double b,
        //    double maxAbsoluteError)
        //{
        //    double diff = Math.Abs(a - b);

        //    if (a.Equals(b))
        //    {
        //        // shortcut, handles infinities
        //        return true;
        //    }

        //    return diff <= maxAbsoluteError;
        //}

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = this.x.GetHashCode();
                hashCode = (hashCode * 397) ^ this.y.GetHashCode();
                hashCode = (hashCode * 397) ^ this.z.GetHashCode();
                return hashCode;
            }
        }

        public int CompareTo(object other)
        {
            if (other is Vector3)
            {
                return this.CompareTo((Vector3)other);
            }
            //error condition: other is not a vector3 object
            throw new ArgumentException(NON_VECTOR_COMPARISON + "\n" + ARGUMENT_TYPE
                + other.GetType().ToString(), "other");
        }

        public int CompareTo(Vector3 other)
        {
            if (this < other)
            {
                return -1;
            }
            else if (this > other)
            {
                return 1;
            }
            return 0;
        }

        public int CompareTo(double tolerance, Vector3 other)
        {
            if (other is Vector3)
            {
                return this.CompareTo(tolerance, (Vector3)other);
            }

            var bothinfinite =
               double.IsInfinity(this.SumComponentsSqrs()) &&
               double.IsInfinity(other.SumComponentsSqrs());

            if (this.Equals(other, tolerance) || bothinfinite)
            {
                return 0;
            }
            if (this < other)
            {
                return -1;
            }
            return 1;

            //error condition: other is not a vector3 object
            throw new ArgumentException(NON_VECTOR_COMPARISON + "\n" + ARGUMENT_TYPE
                + other.GetType().ToString(), "other");

        }

        public int CompareTo(Vector3 other, double tolerance)
        {
            var bothinfinite =
                double.IsInfinity(this.SumComponentsSqrs()) &&
                double.IsInfinity(other.SumComponentsSqrs());

            if (this.Equals(other, tolerance) || bothinfinite)
            {
                return 0;
            }
            if (this < other)
            {
                return -1;
            }
            return 1;
        }

        public static double SumComponents(Vector3 v1)
        {
            return (v1.x + v1.y + v1.z);
        }
        public double SumComponents()
        {
            return SumComponents(this);
        }

        public static Vector3 PowerComponents(Vector3 v1, double power)
        {
            return
                new Vector3(
                    Math.Power(v1.x, power),
                    Math.Power(v1.y, power),
                    Math.Power(v1.z, power)
                    );
        }

        public void PowerComponents(double power)
        {
            this = PowerComponents(this, power);
        }

        public static Vector3 SqrtComponents(Vector3 v1)
        {
            return
                new Vector3(
                    Math.SquareRoot(v1.x),
                    Math.SquareRoot(v1.y),
                    Math.SquareRoot(v1.z)
                    );
        }

        public void sqrtComponents()
        {
            this = SqrtComponents(this);
        }

        public static Vector3 SqrComponents(Vector3 v1)
        {
            return
                new Vector3(
                    v1.x * v1.x,
                    v1.y * v1.y,
                    v1.z * v1.z
                    );
        }

        public void SqrComponents()
        {
            this = SqrComponents(this);
        }

        public static double SumComponentsSqrs(Vector3 v1)
        {
            Vector3 v2 = SqrComponents(v1);
            return v2.SumComponents();
        }

        public double SumComponentsSqrs()
        {
            return SumComponentsSqrs(this);
        }

        public static bool IsNaN(Vector3 v1)
        {
            return double.IsNaN(v1.x) || double.IsNaN(v1.y)
                || double.IsNaN(v1.z);
        }

        public bool IsNaN()
        {
            return IsNaN(this);
        }

        public string ToVerbstring()
        {
            string output = null;

            if (IsUnitVector())
            {
                output += UNITY_VECTOR;
            }
            else
            {
                output += POSITIONAL_VECTOR;
            }

            output += string.Format("( x = {0}, y = {1}, z = {2}", X, Y, Z);
            output += MAGNITUDE + Magnitude;
            return output;
        }

        public string ToString (string format, IFormatProvider formatProvider)
        {
            if (format == null || format == "")
            {
                return String.Format("{0}, {1}, {2}", X, Y, Z);
            }

            char firstChar = format[0];
            string reminder = null;

            if (format.Length > 1)
            {
                reminder = format.Substring(1);
            }

            switch (firstChar)
            {
                case 'x':
                    return X.ToString(reminder, formatProvider);
                case 'y':
                    return X.ToString(reminder, formatProvider);
                case 'z':
                    return X.ToString(reminder, formatProvider);
                default:
                    return
                        String.Format(
                            "{0}, {1}, {2}",
                            X.ToString(reminder, formatProvider),
                            Y.ToString(reminder, formatProvider),
                            Z.ToString(reminder, formatProvider)
                            );
            }
        }

        public override string ToString()
        {
            return ToString(null, null);
        }

    }

    #endregion
}
