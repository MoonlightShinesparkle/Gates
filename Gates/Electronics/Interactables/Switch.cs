using Gates.Electronics;
using Gates.Values;

namespace Gates.Interactables
{
	public class Switch : BasicElectronic, IInteractable, IUpdateable
	{
		public Pin Input;
		public Pin Output;
		private Pin? TrueOutput;
		private Pin? TrueInput;
		private bool Switched = false;

		public Switch() : base()
		{
			Input = new Pin(this);
			Output = new Pin(this);
		}

		public Switch(int PosX, int PosY) : base(PosX,PosY){
			Input = new Pin(this);
			Output = new Pin(this);
		}

		// Seems too complicated, it is causing problems
		public void OnInteraction(Vector2 CursorPos, InteractionState State)
		{
			// When just pressed check for the actual output
			if (State == InteractionState.Pressed){
				if (TrueInput != null && TrueOutput != null){
					Switched = false;
					Powered = false;
					TrueOutput.Powered = false;
					TrueInput = null;
					TrueOutput = null;
					return;
				}
				Switched = true;
				if (Input.Powered && Output.Powered){
					TrueOutput = null;
					TrueInput = null;
				} else if (Input.Powered){
					TrueOutput = Output;
					TrueInput = Input;
				} else if (Output.Powered){
					TrueOutput = Input;
					TrueInput = Output;
				}
				if (TrueOutput != null && TrueInput != null){
					Powered = TrueInput.Powered;
					TrueOutput.Powered = Powered;
				}
			}
		}

		public void Update(){
			if (TrueOutput != null && TrueInput != null){
				Powered = TrueInput.Powered;
				TrueOutput.Powered = Powered;
			} else if (Switched){
				if (Input.Powered){
					TrueOutput = Output;
					TrueInput = Input;
				} else if (Output.Powered){
					TrueOutput = Input;
					TrueInput = Output;
				}
			}
		}

		public override void Draw()
		{
			if (CanBeDrawn())
			{
				Console.SetCursorPosition(AccountedPosition.X, AccountedPosition.Y);
				Console.BackgroundColor = ConsoleColor.DarkGray;
				Console.ForegroundColor = ConsoleColor.Black;
				if (Switched){
					Console.Write(Powered? "▤" : "□");
				} else {
					Console.Write("▣");
				}
				Console.ResetColor();
			}
		}

		public override string ToString()
		{
			return base.ToString() + (Switched? " Switched on":" Switched off");
		}
	}
}