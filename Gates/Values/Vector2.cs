namespace Gates.Values{
	public class Vector2{
		// Stores an X and Y position
		public int X = 0;
		public int Y = 0;

		// when i said modifying it would cause chaos and havoc, i really meant it
		// now it should workies
		public static Vector2 ZERO => new Vector2();
		public static Vector2 ONE => new Vector2(1,1);

		// Example property
		// To get the true magnitude you'd do (Vec1-Vec2).Magnitude
		public double Magnitude{
			get {return Math.Sqrt(Math.Pow(X,2)+Math.Pow(Y,2));}
		}

		// Constructors
		// - Always public
		// - Named as the class
		// - It will run anything it has inside, including functions
		// - One usually sets values here
		// - You can give them as many parameters as you want
		// - A parameterless constructor is considered a default constructor
		// - They do NOT return a value, they are ran once the object is created
		public Vector2(){
		}

		// Example of a non-default contructor
		public Vector2(int XPos, int YPos){
			X = XPos;
			Y = YPos;
		}

		// One would usually add methods here, lets add some to make modifying it easily
		public void SetValue(int X, int Y){
			// See how X and Y are the same name as our attributes?
			// in these cases to refer to the attribute we use "this"
			this.X = X;
			this.Y = Y;
			// "this object's X value becomes the X value we have as a parameter"
		}

		// All objects come with a ToString by default
		// ToString would be useless for us so we just override it in order to make it useful for us
		// in this case it returns (x, y) where x becomes X's value and y becomes Y's value
		// Examples: (1, 1) (2, 1) (4, 5)
		public override string ToString()
		{
			return $"({X}, {Y})";
		}

		// Operator functions, completely optional but very useful when managing mathematical expressions such as
		// vectors, fractions, etc. 

		// Example function that allows to create an addition!
		// No one teaches these, but they are very useful
		public static Vector2 operator +(Vector2 Left, Vector2 Right){
			return new Vector2(Left.X+Right.X,Left.Y+Right.Y);
		}

		// Declare them as:
		// public static [Classname] operator [operator](Thing in left, Thing in right){}
		// they return one of the [Classname] with the operation performed
		// in this case we subtract both X and Y values with each other
		public static Vector2 operator -(Vector2 Left, Vector2 Right){
			return new Vector2(Left.X-Right.X,Left.Y-Right.Y);
		}

		// Doesn't have to forcefully be a Vector2 in both of the sides, you can do stuff like this
		public static Vector2 operator *(int Left, Vector2 Right){
			return new Vector2(Right.X*Left, Right.Y*Left);
		}

		// Yes, we just make it call its int Left version by performing said operation inside the function
		// after all int*Vector2 is defined, so it saves us repeating that code
		public static Vector2 operator *(Vector2 Left, int Right){
			return Right*Left;
		}


	}
}