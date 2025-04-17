using Gates.Electronics;
using Gates.Electronics.Diodes;
using Gates.Electronics.Gates;
using Gates.Values;

namespace Gates{
	public class FullAdder : BasicElectronic {
		// Testing a composite electronic (Electronics within an electronic)
		public Pin InputA;
		public Pin InputB;
		public Pin CarryIn;
		public Pin Sum;
		public Pin CarryOut;

		// Splitters
		private Splitter InputASplit = new Splitter();
		private Splitter InputBSplit = new Splitter();
		private Splitter CarrySplit = new Splitter();
		private Splitter XORSplit = new Splitter();

		// Gates
		private ANDGate FirstAND = new ANDGate();
		private XORGate FirstXOR = new XORGate();
		private ANDGate SecondAND = new ANDGate();
		private XORGate SecondXOR = new XORGate();
		private ORGate FirstOR = new ORGate();

		// Wires
		private Wire[] Wires = new Wire[0];

		private static string[] Graphics = new string[]{
			"██",
			"██",
			"██"
		};

		private void InternalSetup() {
			Wires = new Wire[]{
				// Inputs
				new Wire(InputA, InputASplit.Input),
				new Wire(InputB, InputBSplit.Input),
				new Wire(CarryIn, CarrySplit.Input),

				// Input to AND
				new Wire(InputASplit.OutputB, FirstAND.InputA),
				new Wire(InputBSplit.OutputB, FirstAND.InputB),

				// Input to XOR & XOR to split
				new Wire(InputASplit.OutputA, FirstXOR.InputA),
				new Wire(InputBSplit.OutputA, FirstXOR.InputB),
				new Wire(FirstXOR.Output, XORSplit.Input),

				// Input to second XOR
				new Wire(XORSplit.OutputA, SecondXOR.InputA),
				new Wire(CarrySplit.OutputA, SecondXOR.InputB),

				// Input to second AND
				new Wire(XORSplit.OutputB, SecondAND.InputA),
				new Wire(CarrySplit.OutputB, SecondAND.InputB),

				// Input to OR
				new Wire(FirstAND.Output, FirstOR.InputB),
				new Wire(SecondAND.Output, FirstOR.InputA),

				// Outputs
				new Wire(SecondXOR.Output, Sum),
				new Wire(FirstOR.Output, CarryOut)
			};
		}

		public FullAdder() : base() {
			Size = new Vector2(2, 3);
			InputA = new Pin(this, -1, 0);
			InputB = new Pin(this, -1, 1);
			CarryIn = new Pin(this, -1, 2);
			Sum = new Pin(this, 2, 0);
			CarryOut = new Pin(this, 2, 2);
			InternalSetup();
		}

		public FullAdder(int XPos, int YPos) : base(XPos, YPos) {
			Size = new Vector2(2, 3);
			InputA = new Pin(this, -1, 0);
			InputB = new Pin(this, -1, 1);
			CarryIn = new Pin(this, -1, 2);
			Sum = new Pin(this, 2, 0);
			CarryOut = new Pin(this, 2, 2);
			InternalSetup();
		}

		public override void Draw() {
			Console.ForegroundColor = ConsoleColor.DarkRed;
			DrawGrid(Graphics);
			Console.ResetColor();
		}

		private string Bitify(bool In) {
			return In ? "1" : "0";
		}

		public override string ToString() {
			return base.ToString() + $"Gate status: AND[{Bitify(FirstAND.IsPowered())},{Bitify(SecondAND.IsPowered())}] XOR[{Bitify(FirstXOR.IsPowered())},{Bitify(SecondXOR.IsPowered())}] OR[{Bitify(FirstOR.IsPowered())}] Splitters [{Bitify(InputASplit.IsPowered())},{Bitify(InputBSplit.IsPowered())},{Bitify(CarrySplit.IsPowered())},{Bitify(XORSplit.IsPowered())}] Pins [{Bitify(InputA.Powered)},{Bitify(InputB.Powered)},{Bitify(CarryIn.Powered)}]";
		}
	}
}