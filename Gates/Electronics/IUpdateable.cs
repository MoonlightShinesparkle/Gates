namespace Gates.Electronics{
	// Interfaces allow you to "stick to a standard" to say at least
	// they kinda tell you what you have to add in order for it to work
	// it's kinda like an abstract class, but not really, after all you can implement an interface but not extend it
	// they also don't really require A and B to be related for both to be part of the interface, as long as they
	// want to be part of the interface they can be
	// for it to register a requirememnt you just need to write the function's return value and its name, also its
	// arguments, but that's about it, while they can have functions themselves it is rather specific to the interface
	// instead of the object itself, in other words: you won't be able to run a function defined in an interface
	// until you cast the object into the interface... also- casting is when you turn something into something else
	// like for example (int) 10f <- turn float 10 into int 10
	// we did this in cases where we used if (x is type y) {}
	interface IUpdateable{
		void Update();
	}
}