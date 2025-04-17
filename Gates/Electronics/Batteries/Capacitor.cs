using Gates.Electronics;

namespace Gates{
	public class Capacitor : BasicElectronic, IUpdateable{
		char[] States = new char[]{
			'▯','▏','▎','▍','▌','▋','▊','▉','█' 
		};
		private int Charge = 0;
		private int FrameCount = 0;
		public Pin Input;
		public Pin Output;

		public Capacitor() : base(){
			Input = new Pin(this);
			Output = new Pin(this);
		}

		public Capacitor(int XPos, int YPos) : base(XPos,YPos){
			Input = new Pin(this);
			Output = new Pin(this);
		}

		public void Update(){
			FrameCount++;
			if (FrameCount >= 5){
				if (!Input.Powered){
					Charge = Math.Clamp(Charge-1,0,8);
				}
				FrameCount = 0;
			}
			if (Input.Powered){
				Charge = 8;
			}
			Output.Powered = Charge > 0;
		}

		public override void Draw()
		{
			if (CanBeDrawn()){
				Console.BackgroundColor = ConsoleColor.DarkGreen;
				Console.ForegroundColor = ConsoleColor.Green;
				Console.SetCursorPosition(AccountedPosition.X,AccountedPosition.Y);
				Console.Write(States[Charge]);
				Console.ResetColor();
			}
		}

		public override string ToString()
		{
			return base.ToString() + " Charge: "+Charge;
		}
	}
}