using Gates.Values;

namespace Gates.GUI{
	// We use abstract when we know we have a function with x name and y arguments but we can't say what it
	// does at this point
	// We can NOT create a Object2D since it is abstract, it has no sense to create one after all
	// Imagine having a displayable object but no way to well- have it show, it would break everything
	// Since we want to create Object2Ds that show, we will use inheritance
	public abstract class Object2D{
		// We created a field in Vector in order to have an empty (0,0) vector
		// Useful for some values
		public Vector2 Position = Vector2.ZERO;
		public Vector2 Offset = Vector2.ZERO;
		protected Vector2 Size = Vector2.ONE;
		public Vector2 ObjectSize => Size;

		public Vector2 AccountedPosition {
			get {return Position+Offset;}
		}

		public Object2D(){}

		public Object2D(int XPos, int YPos){
			Position.X = XPos;
			Position.Y = YPos;
		}

		// Example of a function we know should exist but we can't exactly say what it does
		// after all.. at most we'd be able to draw a color or something but that's not what we want
		public abstract void Draw();

		public bool CanBeDrawn(){
			Vector2 Sum = AccountedPosition + Size - Vector2.ONE;
			return (Sum.X >= 0 && Sum.Y >= 0) && (Sum.X <= Console.WindowWidth && Sum.Y <= Console.WindowHeight);
		}

		public int GetTrimQuantityX(){
			Vector2 Sum = AccountedPosition + Size;
			if (Sum.X < Size.X){
				return Size.X - Sum.X;
			}
			return 0;
		}

		public string TrimTextX(string Txt){
			int Q = GetTrimQuantityX();
			if (Q == 0){
				return Txt;
			}
			return Txt.Remove(0,Q);
		}

		public bool IsCursorOnObject(Vector2 CursorPos){
			if (CanBeDrawn()){
				Program.ExtraDebug += "";
				return CursorPos.Y >= AccountedPosition.Y 
				&& CursorPos.Y <= AccountedPosition.Y+Size.Y-1
				&& CursorPos.X >= AccountedPosition.X 
				&& CursorPos.X <= AccountedPosition.X+Size.X-1;
			} else {
				return false;
			}
		}

		// Draws complex shapes through grids
		public void DrawGrid(string[] Grid){
			Vector2 Origin = AccountedPosition;
			// Y coordinate manager
			for (int Y = 0; Y < Grid.Length; Y++){
				// Only draw X coordinates if Y coordinates are viable
				if (Origin.Y+Y >=0 && Origin.Y+Y <= Console.WindowHeight){
					char[] Split = Grid[Y].ToCharArray();
					for (int X = 0; X < Split.Length; X++){
						// Only draw if both X and Y coordinates are viable
						if (Origin.X+X >= 0 && Origin.X+X <= Console.WindowWidth){
							Console.SetCursorPosition(Origin.X+X,Origin.Y+Y);
							Console.Write(Split[X]);
						}
					}
				}
			}
		}

		public static Dictionary<ConsoleColor,ConsoleColor> ActiveColors = new Dictionary<ConsoleColor, ConsoleColor>(){
			{ConsoleColor.Black,ConsoleColor.White},
			{ConsoleColor.DarkGray,ConsoleColor.Gray},
			{ConsoleColor.DarkCyan,ConsoleColor.Cyan},
			{ConsoleColor.DarkGreen,ConsoleColor.Green},
			{ConsoleColor.DarkMagenta,ConsoleColor.Magenta},
			{ConsoleColor.DarkRed,ConsoleColor.Red},
			{ConsoleColor.DarkYellow,ConsoleColor.Yellow}
		};
		public static Dictionary<ConsoleColor,ConsoleColor> InactiveColors = new Dictionary<ConsoleColor, ConsoleColor>(){
			{ConsoleColor.White,ConsoleColor.Black},
			{ConsoleColor.Gray,ConsoleColor.DarkGray},
			{ConsoleColor.Cyan,ConsoleColor.DarkCyan},
			{ConsoleColor.Green,ConsoleColor.DarkGreen},
			{ConsoleColor.Magenta,ConsoleColor.DarkMagenta},
			{ConsoleColor.Red,ConsoleColor.DarkRed},
			{ConsoleColor.Yellow,ConsoleColor.DarkYellow}
		};
		public static bool IsActiveColor(ConsoleColor Color){
			return !ActiveColors.ContainsKey(Color);
		}

		public static ConsoleColor GetCounterpart(ConsoleColor Color){
			if (IsActiveColor(Color)){
				return InactiveColors[Color];
			} else {
				return ActiveColors[Color];
			}
		}

		public override string ToString()
		{
			return $"({AccountedPosition.X}, {AccountedPosition.Y})";
		}
	}
}