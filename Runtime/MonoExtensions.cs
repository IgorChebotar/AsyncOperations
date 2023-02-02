using SimpleMan.Utilities;
using System;
using UnityEngine;
using static SimpleMan.AsyncOperations.Coroutines;

namespace SimpleMan.AsyncOperations
{
    public static class MonoExtensions
    {
        /// <summary>
        /// Invoke [onComplete] after delay
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="delay">Time in seconds</param>
        /// <param name="onComplete"></param>
        /// <returns></returns>
        public static Coroutine Delay(this MonoBehaviour owner, float delay, Action onComplete)
        {
            AssertOnwerExist(owner);
            return owner.StartCoroutine(DelayProcess(delay, onComplete));
        }

        /// <summary>
        /// Invoke [onComplete] after delay
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="delay">Time in seconds</param>
        /// <param name="onComplete"></param>
        /// <returns></returns>
        public static Coroutine DelayRealtime(this MonoBehaviour owner, float delay, Action onComplete)
        {
            AssertOnwerExist(owner);
            return owner.StartCoroutine(DelayProcessRealtime(delay, onComplete));
        }

        /// <summary>
        /// Call [onComplete] after [framesToSkip] frames
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="framesToSkip">Frames number beteen ticks</param>
        /// <param name="onComplete"></param>
        /// <returns></returns>
        public static Coroutine SkipFrames(this MonoBehaviour owner, byte framesToSkip, Action onComplete)
        {
            AssertOnwerExist(owner);
            return owner.StartCoroutine(SkipFramesProcess(framesToSkip, onComplete));
        }

        /// <summary>
        /// Wait while [condition] return false and call [onComplete] when it return true
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="condition"></param>
        /// <param name="onComplete"></param>
        /// <param name="skipFrames">Frames number beteen ticks</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Coroutine WaitUntil(this MonoBehaviour owner, Func<bool> condition, Action onComplete, byte skipFrames = 0)
        {
            AssertOnwerExist(owner);
            return owner.StartCoroutine(WaitUntilProcess(condition, onComplete, skipFrames));
        }

        /// <summary>
        /// Wait while [condition] return true and call [onComplete] when it return false
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="condition"></param>
        /// <param name="onComplete"></param>
        /// <param name="skipFrames">Frames number beteen ticks</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Coroutine WaitWhile(this MonoBehaviour owner, Func<bool> condition, Action onComplete, byte skipFrames = 0)
        {
            AssertOnwerExist(owner);
            return owner.StartCoroutine(WaitUntilProcess(() => !condition(), onComplete, skipFrames));
        }

        /// <summary>
        /// Wait while [condition] return true and call [onComplete] when it return false
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="condition"></param>
        /// <param name="onComplete"></param>
        /// <param name="tickDilation">Time between ticks</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Coroutine WaitWhile(this MonoBehaviour owner, Func<bool> condition, Action onComplete, float tickDilation = 0)
        {
            AssertOnwerExist(owner);
            return owner.StartCoroutine(WaitUntilProcess(() => !condition(), onComplete, tickDilation));
        }

        /// <summary>
        /// Call action each [skip frames] frames while condition is false
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="condition"></param>
        /// <param name="onTick"></param>
        /// <param name="onComplete"></param>
        /// <param name="skipFrames">Frames number beteen ticks</param>
        /// <returns></returns>
        public static Coroutine RepeatUntil(this MonoBehaviour owner, Func<bool> condition, Action onTick, Action onComplete, byte skipFrames = 0)
        {
            AssertOnwerExist(owner);
            return owner.StartCoroutine(RepeatUntilProcess(condition, onTick, onComplete, skipFrames));
        }

        /// <summary>
        /// Call action each [skip frames] frames while condition is true
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="condition"></param>
        /// <param name="onTick"></param>
        /// <param name="onComplete"></param>
        /// <param name="skipFrames">Frames number beteen ticks</param>
        /// <returns></returns>
        public static Coroutine RepeatWhile(this MonoBehaviour owner, Func<bool> condition, Action onTick, Action onComplete, byte skipFrames = 0)
        {
            AssertOnwerExist(owner);
            return owner.StartCoroutine(RepeatUntilProcess(() => !condition(), onTick, onComplete, skipFrames));
        }

        /// <summary>
        /// Call action each [skip frames] frames while condition is true
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="condition"></param>
        /// <param name="onTick"></param>
        /// <param name="onComplete"></param>
        /// <param name="tickDilation">Time between ticks</param>
        /// <returns></returns>
        public static Coroutine RepeatWhile(this MonoBehaviour owner, Func<bool> condition, Action onTick, Action onComplete, float tickDilation = 0)
        {
            AssertOnwerExist(owner);
            return owner.StartCoroutine(RepeatUntilProcess(() => !condition(), onTick, onComplete, tickDilation));
        }

        /// <summary>
        /// Call action each [skip frames] frames forever
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="repeatAction"></param>
        /// <param name="skipFrames">Frames number beteen ticks</param>
        /// <returns></returns>
        public static Coroutine RepeatForever(this MonoBehaviour owner, Action repeatAction, byte skipFrames = 0)
        {
            AssertOnwerExist(owner);
            return owner.StartCoroutine(RepeatUntilProcess(() => false, repeatAction, null, skipFrames));
        }

        /// <summary>
        /// Call action each [skip frames] frames forever
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="repeatAction"></param>
        /// <param name="tickDilation">Time between ticks</param>
        /// <returns></returns>
        public static Coroutine RepeatForever(this MonoBehaviour owner, Action repeatAction, float tickDilation = 0)
        {
            AssertOnwerExist(owner);
            return owner.StartCoroutine(RepeatUntilProcess(() => false, repeatAction, null, tickDilation));
        }

        private static void AssertOnwerExist(MonoBehaviour owner)
        {
            if (owner.NotExist())
            {
                throw new ArgumentNullException(nameof(owner), 
                    "A coroutine can be started from the existing monobehavior only. Check your class " +
                    "on 'null' before calling this method");
            }
        }
    }
}
