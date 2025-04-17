using Gates.GUI;
using Gates.Values;

namespace Gates.Electronics.Batteries{
	public class Bat : BasicElectronic, IUpdateable{
		public readonly Pin Output;

		public Bat() : base(){
			Size = new Vector2(3,1);
			Output = new Pin(this,3,0);
		}

		public Bat(int PosX, int PosY) : base(PosX,PosY){
			Size = new Vector2(3,1);
			Output = new Pin(this,3,0);
		}

		public override void Draw()
		{
			if (CanBeDrawn()){
				Console.BackgroundColor = ConsoleColor.DarkRed;
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.SetCursorPosition(AccountedPosition.X+GetTrimQuantityX(),AccountedPosition.Y);
				Console.Write(TrimTextX("Bat"));
				Console.ResetColor();
			}
		}
		
		public virtual void Update(){
			Powered = true;
			Output.Powered = true;
		}

		public override string ToString()
		{
			return base.ToString() + " Value: " + (Powered? "[1]" : "[0]");
		}
	}
}