using Gates.Values;

namespace Gates.Electronics.Gates{
	public class XNORGate : BiGate{
		// It really is starting to look like a ship...
		private static string[] Graphics = new string[]{
			"┊▜▙╗",
			"┊▟▛╝"
		};

		public XNORGate() : base(){
			Size = new Vector2(4,2);
			InputA.OnStateChanged += Update;
			InputB.OnStateChanged += Update;
		}

		public XNORGate(int XPos, int YPos) : base(XPos,YPos){
			Size = new Vector2(4,2);
			InputA.OnStateChanged += Update;
			InputB.OnStateChanged += Update;
		}

		public override void Update(Pin Updated, PinEventArgs E){
			Powered = !((InputA.Powered || InputB.Powered) && !(InputA.Powered && InputB.Powered));
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