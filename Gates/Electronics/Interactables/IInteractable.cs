using Gates.Values;

namespace Gates.Interactables{
	interface IInteractable{
		void OnInteraction(Vector2 CursorPos, InteractionState State);
	}
}