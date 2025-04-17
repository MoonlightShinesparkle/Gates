using Gates.GUI;
using Gates.Values;

namespace Gates.Electronics{
	// Since we'll base on events, we need to declare both a delegate (fancy name for a function/method)
	// and also event arguments to feed to the user
	public delegate void PinEventHandler(Pin Sender, PinEventArgs PinEventArgs);

	// An alternate form to write a constructor is through "Primary constructors"
	// You use these once you only need a single constructor for your system
	// To employ it simply write parenthesis after the class name, then you can simply use the values you write
	// inside, be careful though since now you'll have nore available variables, so don't confuse them
	// also you'll only be able to rely on that since constructor
	public class PinEventArgs(bool IsPowered) : EventArgs{
		private readonly bool Powered = IsPowered;
		public bool IsPowered(){
			return Powered;
		}
	}

	public class Pin{
		public Wire? ConnectedWire;
		public Object2D Owner;
		public Vector2 Offset = Vector2.ZERO;
		public event PinEventHandler? OnStateChanged;
		private bool InternalPowered = false;

		public Pin(Object2D Obj){
			Owner = Obj;
		}
		public Pin(Object2D Obj, int XOffset, int YOffset){
			Owner = Obj;
			Offset.X = XOffset;
			Offset.Y = YOffset;
		}

		public bool Powered{
			get{
				// in case the connected wire exists and it is powered, the pin will have to be powered
				// that's in case the pin is a receiving pin instead of a sender pin
				if (ConnectedWire?.Powered == true){
					return true;
				}
				// In case it is not receiving power, return what is being sent by the pin
				return InternalPowered;
			}
			set{
				if (InternalPowered != value){
					InternalPowered = value;
					// How we can call our event
					if (OnStateChanged != null){
						OnStateChanged(this,new PinEventArgs(InternalPowered));
					}
				}
			}
		}

		public Vector2 GetPinPosition(){
			return Owner.AccountedPosition + Offset;
		}

		public bool IsActivelyCharged(){
			return InternalPowered;
		}
	}
}