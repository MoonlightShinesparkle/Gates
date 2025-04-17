using Gates.Electronics;
using Gates.Values;

namespace Gates.Interactables
{
	public class Button : BasicElectronic, IInteractable
	{
		public Pin Input;
		public Pin Output;
		private Pin? TrueOutput;
		private Pin? TrueInput;

		public Button() : base()
		{
			Input = new Pin(this);
			Output = new Pin(this);
		}

		public Button(int PosX, int PosY) : base(PosX,PosY){
			Input = new Pin(this);
			Output = new Pin(this);
		}

		public void OnInteraction(Vector2 CursorPos, InteractionState State)
		{
			// When just pressed check for the actual output
			if (State == InteractionState.Pressed){
				if (Input.Powered && Output.Powered){
					TrueOutput = null;
					TrueInput = null;
				} else if (Input.Powered){
					TrueOutput = Output;
					TrueInput = Input;
				} else {
					TrueOutput = Input;
					TrueInput = Output;
				}
				if (TrueOutput != null && TrueInput != null){
					Powered = TrueInput.Powered;
					TrueOutput.Powered = Powered;
				}
			} else if (State == InteractionState.Pressing) {
				// If it's still pressed then just continue sending out a signal
				if (TrueOutput != null && TrueInput != null){
					Powered = TrueInput.Powered;
					TrueOutput.Powered = Powered;
				}
			} else {
				Powered = false;
				if (TrueOutput != null){
					TrueOutput.Powered = false;
				}
				TrueOutput = null;
				TrueInput = null;
			}
		}

		public override void Draw()
		{
			if (CanBeDrawn())
			{
				Console.SetCursorPosition(AccountedPosition.X, AccountedPosition.Y);
				Console.BackgroundColor = ConsoleColor.DarkGray;
				Console.ForegroundColor = ConsoleColor.Black;
				Console.Write("▣");
				Console.ResetColor();
			}
		}

		public override string ToString()
		{
			return base.ToString();
		}
	}
}