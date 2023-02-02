using SimpleMan.Utilities;
using System;
using System.Collections;
using UnityEngine;


namespace SimpleMan.AsyncOperations
{
    public static class Coroutines
    {
        /// <summary>
        /// Call [onComplete] after [framesToSkip] frames
        /// </summary>
        /// <param name="skipFrames">Frames number beteen ticks</param>
        /// <param name="onComplete"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IEnumerator SkipFramesProcess(byte skipFrames, Action onComplete)
        {
            while (skipFrames > 0)
            {
                skipFrames--;
                yield return null;
            }

            onComplete?.Invoke();
        }

        /// <summary>
        /// Invoke [onComplete] after delay
        /// </summary>
        /// <param name="time"></param>
        /// <param name="onComplete"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IEnumerator DelayProcess(float time, Action onComplete)
        {
            yield return new WaitForSeconds(time);
            onComplete?.Invoke();
        }

        /// <summary>
        /// Invoke [onComplete] after delay
        /// </summary>
        /// <param name="time"></param>
        /// <param name="onComplete"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IEnumerator DelayProcessRealtime(float time, Action onComplete)
        {
            yield return new WaitForSecondsRealtime(time);
            onComplete?.Invoke();
        }

        /// <summary>
        /// Wait while [condition] return false and call [onComplete] when it return true
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="onComplete"></param>
        /// <param name="skipFrames">Frames number beteen ticks</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IEnumerator WaitUntilProcess(Func<bool> condition, Action onComplete, byte skipFrames = 0)
        {
            if (condition.NotExist())
            {
                throw new ArgumentNullException(nameof(condition));
            }

            byte framesSkipped = 0;
            while (!condition())
            {
                while (framesSkipped > 0)
                {
                    framesSkipped--;
                    yield return null;
                }
                framesSkipped = skipFrames;
            }

            onComplete?.Invoke();
        }

        /// <summary>
        /// Wait while [condition] return false and call [onComplete] when it return true
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="onComplete"></param>
        /// <param name="tickDilation">Time between ticks</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IEnumerator WaitUntilProcess(Func<bool> condition, Action onComplete, float tickDilation = 0)
        {
            if(tickDilation < 0)
            {
                throw new ArgumentException(nameof(tickDilation), 
                    "Tick dilation must be positive");
            }

            if (condition.NotExist())
            {
                throw new ArgumentNullException(nameof(condition));
            }

            yield return new WaitForSeconds(tickDilation);
            onComplete?.Invoke();
        }

        /// <summary>
        /// Wait while [condition] return false and call [onComplete] when it return true
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="onComplete"></param>
        /// <param name="tickDilation">Time between ticks</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IEnumerator WaitUntilProcessRealtime(Func<bool> condition, Action onComplete, float tickDilation = 0)
        {
            if (tickDilation < 0)
            {
                throw new ArgumentException(nameof(tickDilation),
                    "Tick dilation must be positive");
            }

            if (condition.NotExist())
            {
                throw new ArgumentNullException(nameof(condition));
            }

            yield return new WaitForSecondsRealtime(tickDilation);
            onComplete?.Invoke();
        }

        /// <summary>
        /// Call action each [skip frames] frames while condition is false
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="onTick"></param>
        /// <param name="onComplete"></param>
        /// <param name="skipFrames">Frames number beteen ticks</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IEnumerator RepeatUntilProcess(Func<bool> condition, Action onTick, Action onComplete, byte skipFrames = 0)
        {
            if (condition.NotExist())
            {
                throw new ArgumentNullException(nameof(condition));
            }

            byte framesSkipped = 0;
            while (!condition())
            {
                while (framesSkipped > 0)
                {
                    framesSkipped--;
                    yield return null;
                }
                framesSkipped = skipFrames;
                onTick?.Invoke();
            }

            onComplete?.Invoke();
        }

        /// <summary>
        /// Call action each [skip frames] frames while condition is false
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="onTick"></param>
        /// <param name="onComplete"></param>
        /// <param name="tickDilation">Time between ticks</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IEnumerator RepeatUntilProcess(Func<bool> condition, Action onTick, Action onComplete, float tickDilation = 0)
        {
            if (tickDilation < 0)
            {
                throw new ArgumentException(nameof(tickDilation),
                    "Tick dilation must be positive");
            }

            if (condition.NotExist())
            {
                throw new ArgumentNullException(nameof(condition));
            }

            while (!condition())
            {
                onTick?.Invoke();
                yield return new WaitForSeconds(tickDilation);
            }

            onComplete?.Invoke();
        }

        /// <summary>
        /// Call action each [skip frames] frames while condition is false
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="onTick"></param>
        /// <param name="onComplete"></param>
        /// <param name="tickDilation">Time between ticks</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IEnumerator RepeatUntilProcessRealtime(Func<bool> condition, Action onTick, Action onComplete, float tickDilation = 0)
        {
            if (tickDilation < 0)
            {
                throw new ArgumentException(nameof(tickDilation),
                    "Tick dilation must be positive");
            }

            if (condition.NotExist())
            {
                throw new ArgumentNullException(nameof(condition));
            }

            while (!condition())
            {
                onTick?.Invoke();
                yield return new WaitForSecondsRealtime(tickDilation);
            }

            onComplete?.Invoke();
        }
    }
}
