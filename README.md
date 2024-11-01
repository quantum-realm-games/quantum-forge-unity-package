# Unity Package Features
## QuantumForge
Wrapper around the quantum-forge c-api. Provides low level functionality, like predicated versions of all "Actions" to generate entanglement.

## BasisValues
A ScriptableObject that defines the set of values that a QuantumProperty can exist in a superposition of. These can be created via the Assets/Create/Quantum menu.

## QuantumProperty
A MonoBehaviour that can be added to a game object to interface with the underlying Quantum Forge library. Exposes a BasisValues field in the editor, to define the values the QuantumProperty can exist in a superposition of.

## QuantumActions
These are a set of MonoBehaviours that can be added to a scene, and can be used to trigger operations on QuantumProperty(s).

They expose an optional list of QuantumPropertys to target with an operation, and support an apply() function that can be triggered by simple Unity event. They also expose an apply(QuantumProperty[]) function that will apply the operation to a supplied set of QuantumPropertys. This can be used in conjunction with the QuantumPropertyTrigger to trigger actions on QuantumProperty's at run time.

### Hadamard
Takes something from classical state to equal superposition of all basis values.

### Cycle
Cycles through the basis values. 0 -> 1 -> 2... -> 0

### PhaseRotate
Rotate a specific basis value by a specified amount.

### PhaseAll
Rotate all basis values by an amount proportional to the value.

### NCycle
Entangle two QuantumPropertys. (Multi-dimensional version of CNot)

### Measure
Measure one or more QuantumPropertys to probabilistically collapse to a single basis state in the superposition.

## Trackers
Convenience components to expose information about an underlying state to the editor / game.

### ProbabilityTracker
MonoBehaviour that keeps an updated probability distribution for the basis values of a QuantumProperty, or track the joint probability distribution of multiple QuantumProperty's. Can be toggled to continuously update once per frame, or update on demand via the UpdateProbabilities() function. 

Not continuous updating calls the native API once per frame, and recalculates probabilities on every call. This can be extremely wasteful, when the underlying state is slow to change.

### EntanglementTracker
MonoBehaviour that keeps an updated list of entanglement, as measured by quantum mutual information, between each QuantumProperty and the combined state of the other properties in the list.

This can be used to show the player when different QuantumPropertys are correlated in ways that are beyond classical.

### CorrelationTracker
MonoBehaviour that keeps an updated matrix of Pearson's correlations calculated by the joint probability distribution of all permutations of all basis values for a set of QuantumPropertys.

This can be used to get an idea of how measurement outcomes will be correlated when different QuantumProperty's are entangled.

### PhaseTracker
MonoBehaviour that keeps an updated matrix of the relative phases between all permutations of the basis values for a set of QuantumPropertys.

This can be used to give the player an idea of what interference might occur.

### ReduceDensityMatrixTracker
MonoBehaviour that keeps an updated reduced density matrix for a set of QuantumPropertys. Probably not super useful as a game development/design tool, but it was free, and other trackers are based on its functionality.
