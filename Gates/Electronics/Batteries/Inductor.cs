using Gates.Electronics;

namespace Gates{
	public class Inductor : BasicElectronic, IUpdateable{
		char[] States = new char[]{
			'▯','▏','▎','▍','▌','▋','▊','▉','█' 
		};
		private int Charge = 0;
		private int FrameCount = 0;
		public Pin Input;
		public Pin Output;
		public bool Emitting = false;

		public Inductor() : base(){
			Input = new Pin(this);
			Output = new Pin(this);
		}

		public Inductor(int XPos, int YPos) : base(XPos,YPos){
			Input = new Pin(this);
			Output = new Pin(this);
		}

		public void Update(){
			FrameCount++;
			if (FrameCount >= 5){
				Charge = Math.Clamp(Charge+(!Input.Powered? -1 : 1),0,8);
				FrameCount = 0;
			}
			if (Charge == 8){
				Emitting = true;
			} else if (Charge == 0) {
				Emitting = false;
			}
			Powered = Emitting;
			Output.Powered = Powered;
		}

		public override void Draw()
		{
			if (CanBeDrawn()){
				Console.BackgroundColor = ConsoleColor.DarkYellow;
				Console.ForegroundColor = ConsoleColor.Red;
				Console.SetCursorPosition(AccountedPosition.X,AccountedPosition.Y);
				Console.Write(States[Charge]);
				Console.ResetColor();
			}
		}

		public override string ToString()
		{
			return base.ToString() + " Charge: "+Charge+(Emitting?" Releasing":" Charging");
		}
	}
}