using System.Net;
using System.Reflection.PortableExecutable;
using Gates.GUI;
using Gates.Values;
using static System.Console;

namespace Gates.Electronics{
	public class Wire : Object2D{
		public bool Powered = false;
		private bool AvoidCycle = false;
		protected Pin? ConnectedLeft;
		protected Pin? ConnectedRight;
		protected ConsoleColor WireColor = ConsoleColor.White;

		// Default constructor, since it is extending another class we have to call its constructor
		public Wire() : base(){
		}

		// Yes, we can indeed run functions inside of constructors, i said it runs anything
		public Wire(Pin Left, Pin Right) : base(){
			Connect(Left,Right);
		}

		// They al run the default constructor of 2dObject since we don't need to set the position of the wire
		public Wire(Pin Left, Pin Right,ConsoleColor WireColor) : base(){
			this.WireColor = WireColor;
			Connect(Left,Right);
		}

		// We want a function to connect Left and Right
		public void Connect(Pin Left, Pin Right){
			// In case the wire is connected backwards we still want it to behave normally (left to right) so we rectify it
			Pin Leftmost = Left;
			Pin RightMost = Right;
			if (Left.Owner.Position.X > Right.Owner.Position.X){
				RightMost = Left;
				Leftmost = Right;
			}
			ConnectedLeft = Leftmost;
			Leftmost.ConnectedWire = this;
			Leftmost.OnStateChanged += SpreadCharge;
			ConnectedRight = RightMost;
			RightMost.ConnectedWire = this;
			RightMost.OnStateChanged += SpreadCharge;
		}
		// Disconnects wire from a pin
		public void Disconnect(Pin Disconnected){
			if (Disconnected == ConnectedLeft){
				ConnectedLeft = null;
				Disconnected.ConnectedWire = null;
				Disconnected.OnStateChanged -= SpreadCharge;
			} else if (Disconnected == ConnectedRight){
				ConnectedRight = null;
				Disconnected.ConnectedWire = null;
				Disconnected.OnStateChanged -= SpreadCharge;
			}
		}
		private void SpreadCharge(Pin Obj, PinEventArgs E){
			if (AvoidCycle){
				AvoidCycle = false;
				return;
			}
			Powered = E.IsPowered();
			// Update wires, in case of doing so update variable so we don't start to cyclically call each other with
			// the event, after all the function runs in both pins on any update
			if (Obj == ConnectedLeft && ConnectedRight != null){
				AvoidCycle = true;
				ConnectedRight.Powered = Powered;
			} else if (Obj == ConnectedRight && ConnectedLeft != null){
				AvoidCycle = true;
				ConnectedLeft.Powered = Powered;
			}
		}
		private bool IsValidX(int X){
			return X >= 0 && X <= WindowWidth;
		}
		private bool IsValidY(int Y){
			return Y >= 0 && Y <= WindowHeight;
		}
		public override void Draw()
		{
			if (ConnectedLeft == null || ConnectedRight == null){
				return;
			}
			// Since we employ "return", execution ends once either are null
			BackgroundColor = WireColor;
			Vector2 LeftPos = ConnectedLeft.GetPinPosition();
			Vector2 RightPos = ConnectedRight.GetPinPosition();
			char Line = Powered? '═' : ' ';
			if (LeftPos.Y == RightPos.Y && IsValidY(LeftPos.Y)){
				for (int i = LeftPos.X; i <= RightPos.X; i++){
					if (IsValidX(i)){
						SetCursorPosition(i,LeftPos.Y);
						Write(Line);
					}
				}
			} else {
				int MiddleX = LeftPos.X+((RightPos.X-LeftPos.X)/2);
				if (IsValidY(LeftPos.Y)){
					for (int i = LeftPos.X; i <= MiddleX; i++){
						if (IsValidX(i)){
							SetCursorPosition(i,LeftPos.Y);
							Write(Line);
						}
					}
				}
				if (IsValidY(RightPos.Y)){
					for (int i = MiddleX; i <= RightPos.X; i++){
						if (IsValidX(i)){
							SetCursorPosition(i,RightPos.Y);
							Write(Line);
						}
					}
				}
				// Because it can go up/down and this code does it up, so we have to find the top and the bottom
				int LowestY = Math.Min(LeftPos.Y,RightPos.Y);
				int GreatestY = Math.Max(LeftPos.Y,RightPos.Y);
				char UpLine = Powered? '║' : ' ';
				char IntersectD = Powered? '╝' : ' ';
				char IntersectU = Powered? '╔' : ' ';
				if (LeftPos.Y < RightPos.Y){
					IntersectD = Powered? '╚' : ' ';
					IntersectU = Powered? '╗' : ' ';
				}
				if (IsValidX(MiddleX)){
					for (int i = LowestY; i <= GreatestY; i++){
						if (IsValidY(i)){
							SetCursorPosition(MiddleX,i);
							Write(i == LowestY || i == GreatestY? 
							i == LowestY? IntersectU : IntersectD 
							: UpLine);
						}
					}
				}
			}
			ResetColor();
		}
	}
}