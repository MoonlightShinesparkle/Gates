using Gates.Electronics;
using Gates.Electronics.Batteries;
using Gates.Electronics.Diodes;
using Gates.Electronics.Gates;
using Gates.GUI;

namespace Gates {
	public class FullAdderCircuit {
		// Splitters
		public static Splitter InputASplit = new Splitter(6, 1);
		public static Splitter InputBSplit = new Splitter(6, 4);
		public static Splitter CarrySplit = new Splitter(6, 7);
		public static Splitter XORSplit = new Splitter(16,1);

		// Gates
		public static ANDGate FirstAND = new ANDGate(12, 4);
		public static XORGate FirstXOR = new XORGate(12, 1);
		public static ANDGate SecondAND = new ANDGate(18, 4);
		public static XORGate SecondXOR = new XORGate(18, 1);
		public static ORGate FirstOR = new ORGate(18, 7);

		// IO
		public static LED OutShowSum = new LED(30, 1);
		public static LED OutShowCarry = new LED(30, 7);
		public static SwitchBat InputA = new SwitchBat(1, 1);
		public static SwitchBat InputB = new SwitchBat(1, 4);
		public static SwitchBat CarryIn = new SwitchBat(1, 7);

		// Wiring
		public static Wire InputA_Split = new Wire(InputA.Output, InputASplit.Input, ConsoleColor.Blue);
		public static Wire InputB_Split = new Wire(InputB.Output, InputBSplit.Input, ConsoleColor.Red);
		public static Wire CarryIn_Split = new Wire(CarryIn.Output, CarrySplit.Input, ConsoleColor.Magenta);
		public static Wire A_XOR1 = new Wire(InputASplit.OutputA, FirstXOR.InputA, ConsoleColor.Blue);
		public static Wire B_XOR1 = new Wire(InputBSplit.OutputA, FirstXOR.InputB, ConsoleColor.Red);
		public static Wire A_AND1 = new Wire(InputASplit.OutputB, FirstAND.InputA, ConsoleColor.Blue);
		public static Wire B_AND1 = new Wire(InputBSplit.OutputB, FirstAND.InputB, ConsoleColor.Red);
		public static Wire XOR1_Split = new Wire(FirstXOR.Output, XORSplit.Input);
		public static Wire CarryIn_AND2 = new Wire(CarrySplit.OutputB, SecondAND.InputB,ConsoleColor.Magenta);
		public static Wire CarryIn_XOR2 = new Wire(CarrySplit.OutputA, SecondXOR.InputB,ConsoleColor.Magenta);
		public static Wire XOR1_XOR2 = new Wire(XORSplit.OutputA, SecondXOR.InputA);
		public static Wire XOR1_AND2 = new Wire(XORSplit.OutputB, SecondAND.InputA);
		public static Wire AND1_OR = new Wire(FirstAND.Output, FirstOR.InputB);
		public static Wire AND2_OR = new Wire(SecondAND.Output, FirstOR.InputA);
		public static Wire XOR2_Sum = new Wire(SecondXOR.Output, OutShowSum.Input);
		public static Wire OR_Carry = new Wire(FirstOR.Output, OutShowCarry.Input);

		public static readonly List<Object2D> Objects = new List<Object2D>() {
			InputA,InputASplit,InputB,InputBSplit,CarryIn,CarrySplit,XORSplit,FirstAND,FirstXOR,SecondAND,SecondXOR,FirstOR,
			OutShowSum,OutShowCarry,InputA_Split,InputB_Split,CarryIn_Split,A_XOR1,B_XOR1,A_AND1,B_AND1,XOR1_Split,
			CarryIn_AND2,CarryIn_XOR2,XOR1_XOR2,XOR1_AND2,AND1_OR,AND2_OR,XOR2_Sum,OR_Carry
		};

		public FullAdderCircuit() {
		}

		public static void LoadList(List<Object2D> OtherList) {
			foreach (Object2D Obj in Objects) {
				OtherList.Add(Obj);
			}
		}
	}
}