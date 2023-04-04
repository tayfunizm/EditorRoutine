using System;
using UnityEditor;

namespace Nullframes.IR.IE.Utils {
    public sealed class IERoutine {
        private readonly Func<bool> waitUntil;
        private readonly float delay;
        
        private double nextTime;
        private event Action call;

        public IERoutine(Action call, float delay, Func<bool> waitUntil) {
            this.call += call;
            this.delay = delay;
            this.waitUntil = waitUntil;
            this.nextTime = EditorApplication.timeSinceStartup + delay;

            this.call += () => { EditorApplication.update -= Update; };
        }

        public void Update() {
            if (!(EditorApplication.timeSinceStartup > nextTime)) return;
            if (waitUntil == null) {
                Execute();
                nextTime = EditorApplication.timeSinceStartup + delay;
                return;
            }

            if (waitUntil.Invoke()) {
                Execute();
            }
        }

        private void Execute() {
            call?.Invoke();
        }

        public void Dispose() {
            EditorApplication.update -= Update;
        }
    }

    public static class EditorRoutine {
        public static IERoutine StartRoutine(float delay, Action call) {
            IERoutine routine = new IERoutine(call, delay, null);

            EditorApplication.update += routine.Update;
            return routine;
        }

        public static IERoutine StartRoutine(Func<bool> waitUntil, Action call) {
            IERoutine routine = new IERoutine(call, 0f, waitUntil);

            EditorApplication.update += routine.Update;
            return routine;
        }
        
        public static IERoutine StartRoutine(float delay, Func<bool> waitUntil, Action call) {
            IERoutine routine = new IERoutine(call, delay, waitUntil);

            EditorApplication.update += routine.Update;
            return routine;
        }

        public static void StopRoutine(IERoutine routine) {
            routine.Dispose();
        }
    }
}