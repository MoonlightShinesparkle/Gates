using Gates.GUI;
using Gates.Values;

namespace Gates.Electronics.Batteries{
	public class RandoBat : BasicElectronic, IUpdateable{
		public readonly Pin Output;
		private static readonly Random RNG = new Random();
		int Frame = 0;

		public RandoBat() : base(){
			Size = new Vector2(3,1);
			Output = new Pin(this,3,0);
		}

		public RandoBat(int PosX, int PosY) : base(PosX,PosY){
			Size = new Vector2(3,1);
			Output = new Pin(this,3,0);
		}

		// We follow IUpdateable
		// Raw update is too fast
		public void Update(){
			// Only change battery's powered value if frame has been increased 10 times
			if (Frame >= 10){
				Powered = RNG.Next(0,2) == 0;
				// Reset frame back to 0 after updating
				Frame = 0;
			}
			// Update output to mirror battery's powered output
			Output.Powered = Powered;
			// Increase frame by 1 each update loop
			Frame++;
		}

		public override void Draw()
		{
			if (CanBeDrawn()){
				Console.BackgroundColor = ConsoleColor.DarkRed;
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.SetCursorPosition(AccountedPosition.X+GetTrimQuantityX(),AccountedPosition.Y);
				Console.Write(TrimTextX("RNG"));
				Console.ResetColor();
			}
		}

		public override string ToString()
		{
			return base.ToString() + " Value: " + (Powered? "[1]" : "[0]");
		}
	}
}