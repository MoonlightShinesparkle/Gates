# Gates
A console based application for the making of small logic based circuits made in C#, it offers a pseudo-graphic interface
with the capacity of moving around both the viewport and a cursor.

# Interface
The interface allows you to see your current position in the window, checking the window size and check important controls, it optionally shows additional information about
the components placed in the console.

# Circuits
There's no interface to create circuits, instead it shows pre-created circuits written in a class, check the example circuit to see ways you can make these circuits.

# Electronica
ALl electronics depend on the "BasicElectronic" class which extents "Object2D", it allows circuits to hold an update function, have access to modifying whether it's powered or not,
allows external circuits to check if it's powered and contains all Object2D functionality.<br>
Electronics are based on the concept of pins and wires, the electronics themselves being nothing more than logic that controls the pins, gives them positions, allows for them
to have a shape, etc. Extra interfaces allow for the capacity of being updated every frame and being interacted with.
## Drawing
It is recommended to use the grid drawing function within Object2D, it takes in a string array which then is drawn to console.
## Some electronics
### Batteries
Simple outputs, they output a stored charge, true batteries do not have a input pin.
#### True batteries
The simplest of them is the Bat, it always outputs a true Powered to its pin, meanwhile RandoBat's Powered output is random and SwitchBat's Powered depends on user input.
#### Capacitors
They quickly store energy and release it over time, they give out a graphic on its currently charged level.
#### Inductors
They slowly store energy and slowly release it once it reaches its apex, do not react its apex in time and it'll just discharge itself without powering its output, power
it after its apex and you'll lengthen its output time.
### Diodes
Simple one-way electronics, some offer special functionalities.
#### Diode
The simplest one of them, has no special functionality.
#### LED
A diode with the capacity to light up once powered, they will be your main way of showing output.
#### Splitter
A diode with 2 outputs, a single input becomes 2 outputs with it, use it to go around the max of 1 wire per pin.
### Gates
The general objective of the project, gates allow for different kinds of logic to happen.
#### BIGate
Not a placeable electronic but the base of all gates: 2 inputs, one output and the function to see its value.
#### ORGate
Either input will power the output.
#### ANDGate
Both inputs need to be powered to power the output.
#### NOTGate
Breaks the 2 inputs one output rule, after all all it does is output the opposite of the input.
#### NORGate
The polar opposite of the AND: neither input has to be on for it to output power.
#### NANDGate
Outputs power until both inputs are powered.
#### XORGate
Either inputs can be turned on for it to output power, but both can't be turned on for it to output power.
#### XNORGate
Either no pin is turned on or both pins are turned on for it to output power.
### Interactables
Electronics based in the IInteractable interface, they allow for the user to actively interact with your circuit, SwitchBat could be considered part of these.
#### Button
Outputs power when the user presses it, stops outputting when the user stops, it is bidirectional.
#### Switch
Outputs power when switched on, blocks it when switched off, it is bidirectional.
### Storage
Electronics capable of storing data in execution (once execution ends their data is lost).
#### BitStore
Stores a single bit of data, the Write pin overrides the stored value with the Data pin's value.
#### ByteStore
Stores a byte (8 bits) of data, due to its size it works through an array of inputs, an array of stored values and an array of outputs, it is functionally the same as
bit storage but the Write pin manages all 8 Data pins.

# Pins and wires
## Pins
Pins contain an event which runs once its powered state is changed, electronics tend to change/read its property, every pin can have ONE wire attached to it.
## Wires
Wires take in count 2 pins and subscribe to both's powered event, wires are capable of spreading the charge between pins and are the basis of power transmission, electronics
communicate with each other through wires connected to their pins, the algorithm to draw wires is overly simplistic but allows for simple displays of the circuit.
### Splitter
To help against pins only being able to be connected to another pin splitters were put in place, these small electronics offer an input pin which translates to 2 output pins
which mirror the input's power, they are useful for when one wants to connect a single output to many inputs.

# Compound electronics
An electronic which represents a small circuit is considered an compound electronic, while they also extend the basic electronic class they generally do not make use of logic but
instead hold an ecosystem of electronics which can be accessed through its pins. An example of a compound electronic is the FullAdder.

# Switching circuits
To load a circuit simply dump its electronics and wires into the "Drawables" list within program, ExampleCircuit and FullAdderCircuit do so through a LoadList method which takes
as an argument the drawables list and transfers contents from an internal list into the output list, FullAdderCircuit autogenerates part of the cirtuit through some loops to
avoid having to do so by hand.
