# Technique for patching enumerator methods to override the original implementation (similar to a prefix patch that skips the original method)
Apply a postfix patch to the target method that returns an IEnumerator and takes in an IEnumerator as its first parameter regardless of whether the target method takes in no parameters or takes in other parameters.

## Notes regarding the technique
The postfix patch will be executed before the original enumerator method is executed.

## Explanation for the technique
When one has a method which returns IEnumerable and uses "yield" statements, under the hood, this compiles to a new class which does the state machine logic for that code, and the method returns a new instance of that class.

Hence, one can apply a postfix patch to the aforementioned method and return a different enumerable. This will create an instance of the aforementioned state machine class, however, none of its logic should be executed as long as one does not try to enumerate it.