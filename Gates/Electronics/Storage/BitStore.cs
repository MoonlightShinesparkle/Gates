using Gates.Values;

namespace Gates.Electronics.Storage{
	public class BitStorage : BasicElectronic, IUpdateable{
		public readonly Pin Data;
		public readonly Pin Write;
		public readonly Pin Output;
		private bool State = false;

		private string[] Graphics = new string[]{
			"▇",
			"▇"
		};

		public BitStorage() : base(){
			Size = new Vector2(1,2);
			Data = new Pin(this);
			Write = new Pin(this,0,-1);
			Output = new Pin(this);
			Write.OnStateChanged += UpdateOut;
		}

		public BitStorage(int XCord, int YCord) : base(XCord,YCord){
			Size = new Vector2(1,2);
			Data = new Pin(this);
			Write = new Pin(this,0,-1);
			Output = new Pin(this);
			Write.OnStateChanged += UpdateOut;
		}

		public void Update(){
			if (Powered){
				State = Data.Powered;
			}
			Output.Powered = State;
		}

		public void UpdateOut(Pin Sender, PinEventArgs E){
			Powered = Sender.Powered;
		}

		public override void Draw()
		{
			Console.BackgroundColor = ConsoleColor.Black;
			Console.ForegroundColor = Powered? ConsoleColor.Magenta : ConsoleColor.DarkMagenta;
			DrawGrid(Graphics);
			Console.ResetColor();
		}

		public override string ToString()
		{
			return base.ToString() + " Stored: ["+(State?"1":"0")+"]";
		}
	}
}