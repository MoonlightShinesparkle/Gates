using Gates.Values;

namespace Gates.Electronics.Gates{
	public class NANDGate : BiGate{
		private static string[] Graphics = new string[]{
			"█▙╗",
			"█▛╝"
		};

		public NANDGate() : base(){
			Size = new Vector2(3,2);
			InputA.OnStateChanged += Update;
			InputB.OnStateChanged += Update;
		}

		public NANDGate(int XPos, int YPos) : base(XPos,YPos){
			Size = new Vector2(3,2);
			InputA.OnStateChanged += Update;
			InputB.OnStateChanged += Update;
		}

		public override void Update(Pin Updated, PinEventArgs E){
			// Either of them active but not both nor either
			Powered = !(InputA.Powered && InputB.Powered);
			Output.Powered = Powered;
		}

		public override void Draw()
		{
			Console.BackgroundColor = ConsoleColor.Black;
			Console.ForegroundColor = ConsoleColor.Blue;
			DrawGrid(Graphics);
			Console.ResetColor();
		}
	}
}