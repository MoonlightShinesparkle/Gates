using Gates.GUI;

namespace Gates.Electronics{
	public abstract class BasicElectronic : Object2D{
		// So for our electronic we want to inherit 2DObject, so we use :
		// Lets see.. for it to work we need to pretty much have a input pin and an output pin for every electronic
		// Since we want the powered value to be used only by the electronic then we make it protected
		// we don't want outsiders just changing powered to true . _.
		protected bool Powered = false;

		public BasicElectronic(){}
		public BasicElectronic(int XPos, int YPos) : base(XPos,YPos){}

		public bool IsPowered(){
			return Powered;
		}
	}
}