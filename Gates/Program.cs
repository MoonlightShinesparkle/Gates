using Gates.Electronics;
using Gates.GUI;
using Gates.Interactables;
using Gates.Values;
using static System.Console;

namespace Gates{
	public class Program(){
		// Determines if the program is currently running
		public static bool Running = true;
		// Creating an object of vector: it will store the X and Y positions we need

		// In this case it creates a Vector2 with the values we set by default
		// if you left them as empty, they will become 0
		// public int X; -> X = 0;
		public static Vector2 ScreenOffset = Vector2.ZERO;

		// We can also make stuff like this
		// We gave it an argument so it checked for any constructors that takes 2 integrers
		// Since it found one it will run the constructor
		// Our constructor assigns XPos to X and YPos to Y, so X = 1 and Y = 1
		public static Vector2 CursorPos = new Vector2(1, 1);

		// Since the console is being used as a window, we can't just print messages . _.
		public static string DebugMessage = "";
		public static string ExtraDebug = "";
		public static bool ShowDebugMessage = false;

		// Temporary list
		public static List<Object2D> Drawables = new List<Object2D>()
		{
		};

		private static IInteractable? CurrentInteractable;
		private static bool InteractButtonPressed = false;
		private static int InteractDebounce = 0;

		// Program inner workings
		public static void Main()
		{
			// Load custom circuit
			ExampleCircuit.LoadList(Drawables);
			// Main loop
			while (Running)
			{
				// Clear the console
				Clear();
				// Run key logic
				KeyLogic();
				// Draw all objects
				DrawAll();
				// Show the cursor we'll move around
				ShowCursor();
				// Change cursor to the lowest position in console taking in count the debug message
				SetCursorPosition(0, ShowDebugMessage ? WindowHeight - 2 : WindowHeight - 1);
				// Change console colors
				BackgroundColor = ConsoleColor.DarkBlue;
				ForegroundColor = ConsoleColor.White;
				// Print controls to console
				Write("Q - Quit | WASD - Move around | Arrow keys - Move cursor | F - Debug messages | Enter - Interact");
				if (ShowDebugMessage)
				{
					SetCursorPosition(0, WindowHeight - 1);
					Write(DebugMessage + ExtraDebug);
				}
				// Reset the console color
				ResetColor();
				// Reset debug message
				DebugMessage = "";
				ExtraDebug = "";
				// Makes each frame take 10 ms to stop flickering and avoid overpressuring the processor
				Thread.Sleep(10);
				InteractDebounce++;
			}
			// User chose to end the program
			Clear();
			Write("Press enter to end...");
			ReadLine();
		}

		// A function to draw the complete circuit
		// We don't want to have wires overlap the objects so we draw the wires first before the objects
		// of course wire drawing is not available yet
		public static void DrawAll()
		{
			List<Object2D> Buffer = new List<Object2D>();
			foreach (Object2D Drawable in Drawables)
			{
				// We check within our objects if it can be updated, update if that's the case
				if (Drawable is IUpdateable Updateable)
				{
					Updateable.Update();
				}
				Drawable.Offset = ScreenOffset;
				if (Drawable is Wire Connection)
				{
					Connection.Draw();
				}
				else
				{
					if (Drawable.IsCursorOnObject(CursorPos))
					{
						DebugMessage = Drawable.GetType().Name + "#" + Drawable.GetHashCode() + " " + Drawable.ToString();
						if (Drawable is IInteractable Interactable)
						{
							if (InteractButtonPressed && CurrentInteractable == null)
							{
								ExtraDebug += " Pressed";
								Interactable.OnInteraction(CursorPos, InteractionState.Pressed);
								CurrentInteractable = Interactable;
							}
							else if (InteractButtonPressed && CurrentInteractable == Interactable)
							{
								ExtraDebug += " Pressing";
								Interactable.OnInteraction(CursorPos, InteractionState.Pressing);
							}
							else if (!InteractButtonPressed && CurrentInteractable == Interactable)
							{
								ExtraDebug += " Released";
								Interactable.OnInteraction(CursorPos, InteractionState.Released);
								CurrentInteractable = null;
							}
						}
					}
					Buffer.Add(Drawable);
				}
			}
			foreach (Object2D Obj in Buffer)
			{
				Obj.Draw();
			}
		}

		// Simple function to show a "cursor"
		public static void ShowCursor()
		{
			SetCursorPosition(CursorPos.X, CursorPos.Y);
			// This looks like something that should be made by default, right?
			if (DebugMessage == "")
			{
				DebugMessage = CursorPos.ToString();
				DebugMessage += $" | Dims: ({WindowWidth},{WindowHeight})";
			}
			ForegroundColor = ConsoleColor.White;
			Write("•");
			ResetColor();
		}

		// Handles key logic: what happens when a key is pressed
		public static void KeyLogic()
		{
			if (KeyAvailable)
			{
				InteractDebounce = 0;
				ConsoleKey Key = ReadKey(true).Key;
				InteractButtonPressed = Key == ConsoleKey.Enter;
				switch (Key)
				{
					// Quit program
					case ConsoleKey.Q:
						{
							Running = false;
							break;
						}
					// Change console offset, value that will be needed on the future ... why is it working if i
					// haven't implemented offset yet?
					case ConsoleKey.W:
						{
							// Console positions work upside down, but it feels awkward to take the console's directions
							ScreenOffset.Y += 1;
							break;
						}
					case ConsoleKey.A:
						{
							ScreenOffset.X += 1;
							break;
						}
					case ConsoleKey.S:
						{
							ScreenOffset.Y -= 1;
							break;
						}
					case ConsoleKey.D:
						{
							ScreenOffset.X -= 1;
							break;
						}
					// Change cursor offset within the screen
					// if we leave it as -= x, it will break once we exit the screen dimentions, lets do something to 
					// fix such messes
					case ConsoleKey.UpArrow:
						{
							// Console positions work upside down
							// we can do it in 2 ways (that i can think of)
							// This is the way one would do it with a regular if
							/* 
								CursorPos.Y -= 1;
								if (CursorPos.Y < 0){
									CursorPos.Y = 0;
								}
							*/
							// This is a different way that employs a single line
							// in this instance we ask if substracting 1 to Y leads to any number less than 0
							// if that's the case then make it become 0, else it will just turn it into the calculation
							CursorPos.Y = CursorPos.Y - 1 < 0 ? 0 : CursorPos.Y - 1;
							break;
						}
					case ConsoleKey.LeftArrow:
						{
							CursorPos.X = CursorPos.X - 1 < 0 ? 0 : CursorPos.X - 1;
							break;
						}
					case ConsoleKey.RightArrow:
						{
							// Now adding becomes a bit more problematic as their ends vary on the console's window
							// We need to employ width and height
							// we just do the same but instead of making it 0 we turn it into the width, also we employ >
							CursorPos.X = CursorPos.X + 1 > WindowWidth ? WindowWidth : CursorPos.X + 1;
							break;
						}
					case ConsoleKey.DownArrow:
						{
							// We employ window Height
							// .... oh my fluffiness, do not use += in this case-
							CursorPos.Y = CursorPos.Y + 1 > WindowHeight ? WindowHeight : CursorPos.Y + 1;
							break;
						}
					case ConsoleKey.F:
						{
							// Toggles the value
							ShowDebugMessage = !ShowDebugMessage;
							break;
						}
				}
			}
			else
			{
				if (InteractDebounce >= 10)
				{
					InteractButtonPressed = false;
				}
			}
		}
	}
}