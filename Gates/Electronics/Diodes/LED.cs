using Gates.Values;

namespace Gates.Electronics.Diodes{
	public class LED : Diode{
		// We want to control the color in a way that the active color is the bright color
		public ConsoleColor Color {
			get {return RegularColor;}
			set {
				if (IsActiveColor(value)){
					RegularColor = value;
					Counterpart = GetCounterpart(value);
				} else {
					RegularColor = GetCounterpart(value);
					Counterpart = value;
				}
			}
		}
		private ConsoleColor RegularColor = ConsoleColor.Red;
		private ConsoleColor Counterpart = ConsoleColor.DarkRed;

		public LED() : base(){
			Size = Vector2.ONE;
		}

		public LED(int XPos, int YPos) : base(XPos,YPos){
			Size = Vector2.ONE;
		}

		public LED(int XPos, int YPos, ConsoleColor Color) : base(XPos,YPos){
			Size = Vector2.ONE;
			this.Color = Color;
		}

		public override void Draw()
		{
			if (CanBeDrawn()){
				if (Powered){
					Console.BackgroundColor = RegularColor;
				} else {
					Console.BackgroundColor = Counterpart;
				}
				Console.SetCursorPosition(AccountedPosition.X+GetTrimQuantityX(),AccountedPosition.Y);
				Console.Write(TrimTextX(" "));
				Console.ResetColor();
			}
		}
	}
}