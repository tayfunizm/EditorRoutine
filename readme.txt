EditorRoutine.StartRoutine(() => IsComplete, () => { 
  //...
});

or

EditorRoutine.StartRoutine(2f, () => { 
  //...
});

or

EditorRoutine.StartRoutine(2f, () => IsComplete, () => { 
  //...
});
