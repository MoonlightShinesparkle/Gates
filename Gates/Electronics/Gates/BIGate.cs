namespace Gates.Electronics.Gates{
	public abstract class BiGate : BasicElectronic{
		public readonly Pin InputA;
		public readonly Pin InputB;
		public readonly Pin Output;

		public BiGate() : base()
		{
			InputA = new Pin(this, -1, 0);
			InputB = new Pin(this, -1, 1);
			Output = new Pin(this);
		}

		public BiGate(int XPos, int YPos) : base(XPos, YPos)
		{
			InputA = new Pin(this, -1, 0);
			InputB = new Pin(this, -1, 1);
			Output = new Pin(this);
		}

		public abstract void Update(Pin Updated, PinEventArgs E);

		public override string ToString()
		{
			return base.ToString() + "Value: ["+(Powered?"1":"0")+"]";
		}
	}
}