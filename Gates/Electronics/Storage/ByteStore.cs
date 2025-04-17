using Gates.Values;

namespace Gates.Electronics.Storage{
	public class ByteStorage : BasicElectronic, IUpdateable{
		public Pin[] DataPins = new Pin[8];
		public bool[] Stored = new bool[8];
		public Pin Write;
		public Pin[] OutputPins = new Pin[8];

		private string[] Graphics = new string[]{
			"▇▇▇▇",
			"▇▇▇▇",
			"▇▇▇▇",
			"▇▇▇▇",
			"▇▇▇▇",
			"▇▇▇▇",
			"▇▇▇▇",
			"▇▇▇▇",
			"▇▇▇▇"
		};

		public void BasicSetup(){
			Size = new Vector2(4,9);
			for (int i=0; i<DataPins.Length; i++){
				DataPins[i] = new Pin(this,-1,i);
				OutputPins[i] = new Pin(this,3,i);
			}
		}

		public ByteStorage() : base(){
			Write = new Pin(this,-1,8);
			Write.OnStateChanged += UpdateOut;
			BasicSetup();
		}

		public ByteStorage(int XCord, int YCord) : base(XCord,YCord){
			Write = new Pin(this,-1,8);
			Write.OnStateChanged += UpdateOut;
			BasicSetup();
		}

		public void Update(){
			if (Powered){
				for (int i=0; i<DataPins.Length; i++){
					Stored[i] = DataPins[i].Powered;
				}
			}
			for (int i=0; i<OutputPins.Length; i++){
				OutputPins[i].Powered = Stored[i];
			}
		}

		public void UpdateOut(Pin Sender, EventArgs E){
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
			string Added = "";
			foreach(bool State in Stored){
				Added += State? "1":"0";
			}
			return base.ToString() + "Stored: ["+Added+"]";
		}
	}
}