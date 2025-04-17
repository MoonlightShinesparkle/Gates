using Gates.GUI;
using Gates.Interactables;
using Gates.Values;

namespace Gates.Electronics.Batteries{
	public class SwitchBat : Bat, IUpdateable, IInteractable{
		private bool Switched = false;
		public SwitchBat() : base(){
		}

		public SwitchBat(int PosX, int PosY) : base(PosX,PosY){
		}

		public override void Draw()
		{
			if (CanBeDrawn()){
				Console.BackgroundColor = ConsoleColor.DarkRed;
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.SetCursorPosition(AccountedPosition.X+GetTrimQuantityX(),AccountedPosition.Y);
				Console.Write(TrimTextX(Switched?"═▤═":"═□═"));
				Console.ResetColor();
			}
		}

		public void OnInteraction(Vector2 CursorPos, InteractionState State){
			if (State == InteractionState.Pressed){
				Switched = !Switched;
			}
		}

		public override void Update()
		{
			Powered = Switched;
			Output.Powered = Switched;
		}

		public override string ToString()
		{
			return base.ToString();
		}
	}
}