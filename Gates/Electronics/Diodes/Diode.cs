using Gates.Values;

namespace Gates.Electronics.Diodes{
	public class Diode : BasicElectronic{
		public readonly Pin Input;
		public readonly Pin Output;

		public Diode() : base(){
			Size = new Vector2(2,1);
			Input = new Pin(this);
			Output = new Pin(this);
			Input.OnStateChanged += Update;
		}

		// So.. since "this" is not available outside we must throw it into the constructor
		// Make sure to create pin before subsribint to the event, else it may break . _.
		public Diode(int XPos, int YPos) : base(XPos,YPos){
			Size = new Vector2(2,1);
			Input = new Pin(this);
			Output = new Pin(this);
			Input.OnStateChanged += Update;
		}

		public void Update(Pin pin, PinEventArgs E){
			Powered = Input.Powered;
			Output.Powered = Input.Powered;
		}

		public override void Draw()
		{
			if (CanBeDrawn()){
				Console.SetCursorPosition(AccountedPosition.X,AccountedPosition.Y);
				Console.BackgroundColor = ConsoleColor.DarkGray;
				Console.ForegroundColor = ConsoleColor.Black;
				Console.Write("▶▎");
				Console.ResetColor();
			}
		}

		public override string ToString()
		{
			return base.ToString() + (Powered? " Powered" : " Inactive");
		}
	}
}