using Gates.Values;

namespace Gates.Electronics.Gates{
	public class NORGate : BiGate{
		private static string[] Graphics = new string[]{
			"▜▙╗",
			"▟▛╝"
		};

		public NORGate() : base(){
			Size = new Vector2(3,2);
			InputA.OnStateChanged += Update;
			InputB.OnStateChanged += Update;
		}

		public NORGate(int XPos, int YPos) : base(XPos,YPos){
			Size = new Vector2(3,2);
			InputA.OnStateChanged += Update;
			InputB.OnStateChanged += Update;
		}

		public override void Update(Pin Updated, PinEventArgs E){
			Powered = !(InputA.Powered || InputB.Powered);
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