using Gates.Values;

namespace Gates.Electronics.Diodes{
	public class Splitter : BasicElectronic
	{
		public readonly Pin Input;
		public readonly Pin OutputA;
		public readonly Pin OutputB;

		private static string[] Graphics = new string[]{
			"╦",
			"╚"
		};

		public Splitter() : base() {
			Size = new Vector2(1, 2);
			Input = new Pin(this, -1, 0);
			OutputA = new Pin(this, 1, 0);
			OutputB = new Pin(this, 1, 1);
			Input.OnStateChanged += Update;
		}

		public Splitter(int XPos, int YPos) : base(XPos, YPos) {
			Size = new Vector2(1, 2);
			Input = new Pin(this, -1, 0);
			OutputA = new Pin(this, 1, 0);
			OutputB = new Pin(this, 1, 1);
			Input.OnStateChanged += Update;
		}

		public void Update(Pin Emitter, PinEventArgs E)
		{
			Powered = Emitter.Powered;
			OutputA.Powered = Powered;
			OutputB.Powered = Powered;
		}

		public override void Draw()
		{
			Console.BackgroundColor = ConsoleColor.White;
			Console.ForegroundColor = ConsoleColor.Black;
			DrawGrid(Graphics);
			Console.ResetColor();
		}
	}
}