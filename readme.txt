EditorRoutine.StartRoutine(() => IsComplete, () => { 
  //...
  //wait until IsComplete is true, and run.
});

or

EditorRoutine.StartRoutine(2f, () => { 
  //...
  //wait 2sc, and run.
});

or

EditorRoutine.StartRoutine(2f, () => IsComplete, () => { 
  //...
  //wait 2sc and wait until IsComplete is true, then runs.
});


--------------

var routine = EditorRoutine.StartRoutine(() => IsComplete, Execute);

private void Execute() {
  //...
}

EditorRoutine.StopRoutine(routine); // Stops routine.
