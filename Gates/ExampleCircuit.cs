using Gates.Electronics;
using Gates.Electronics.Batteries;
using Gates.Electronics.Diodes;
using Gates.Electronics.Storage;
using Gates.GUI;
using Gates.Interactables;

namespace Gates{
	public class ExampleCircuit{

		public static ByteStorage RandStorage = new ByteStorage(6,1);
		public static SwitchBat RandWrite = new SwitchBat(1,RandStorage.ObjectSize.Y);
		public static Wire RandWire = new Wire(RandWrite.Output,RandStorage.Write);

		public static ByteStorage ManualStorage = new ByteStorage(6,2+RandStorage.ObjectSize.Y);
		public static SwitchBat ManualWrite = new SwitchBat(1,1+2*ManualStorage.ObjectSize.Y);
		public static Wire ManualWire = new Wire(ManualWrite.Output,ManualStorage.Write);

		public static readonly List<Object2D> Objects = new List<Object2D>(){
			RandStorage,RandWrite,RandWire,ManualStorage,ManualWrite,ManualWire
		};

		public ExampleCircuit(){
		}

		public static void LoadList(List<Object2D> OtherList){
			Pin[] RandOuts = new Pin[8];
			for(int i =0; i<8; i++){
				RandoBat Bat = new RandoBat(1,i+1);
				Wire Connector = new Wire(Bat.Output,RandStorage.DataPins[i]);
				LED OutShower = new LED(Bat.Position.X+Bat.ObjectSize.X+1+RandStorage.ObjectSize.X+2,Bat.Position.Y);
				Wire ConnectorB = new Wire(RandStorage.OutputPins[i],OutShower.Input);
				RandOuts[i] = OutShower.Output;
				Objects.Add(Bat);
				Objects.Add(Connector);
				Objects.Add(OutShower);
				Objects.Add(ConnectorB);
			}
			Pin[] ManOuts = new Pin[8];
			for (int i=0; i<8; i++){
				SwitchBat Bat = new SwitchBat(1,i+2+RandStorage.ObjectSize.Y);
				Wire Connector = new Wire(Bat.Output,ManualStorage.DataPins[i]);
				LED OutShower = new LED(Bat.Position.X+Bat.ObjectSize.X+1+ManualStorage.ObjectSize.X+2,Bat.Position.Y);
				Wire ConnectorB = new Wire(ManualStorage.OutputPins[i],OutShower.Input);
				ManOuts[i] = OutShower.Output;
				Objects.Add(Bat);
				Objects.Add(Connector);
				Objects.Add(OutShower);
				Objects.Add(ConnectorB);
			}
			FullAdder[] Adders = new FullAdder[8];
			for (int i = 0; i < 8; i++) {
				FullAdder Adder = new FullAdder(20, 1 + 3 * i);
				Wire FirstIn = new Wire(RandOuts[i], Adder.InputA, ConsoleColor.DarkMagenta);
				Wire SecondIn = new Wire(ManOuts[i], Adder.InputB);
				LED Shower = new LED(24 + Adder.ObjectSize.X, 1 + i);
				Wire Out = new Wire(Adder.CarryOut, Shower.Input);
				Objects.Add(Adder);
				Objects.Add(FirstIn);
				Objects.Add(SecondIn);
				Objects.Add(Shower);
				Objects.Add(Out);
				Adders[i] = Adder;
			}
			for (int i = 0; i < 8; i++) {
				if (i + 1 < Adders.Length) {
					Wire Connector = new Wire(Adders[i].CarryOut, Adders[i + 1].CarryIn);
					Objects.Add(Connector);
				}
			}
			foreach (Object2D Obj in Objects) {
				OtherList.Add(Obj);
			}
		}
	}
}