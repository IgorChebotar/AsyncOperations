using SimpleMan.Utilities;
using System;
using System.Threading.Tasks;
using UnityEngine;


namespace SimpleMan.AsyncOperations
{
    public static class SafeAsync
    {
        /// <summary>
        /// Waits while time is up. Safe for using in playmode (returns 'Canceled by sytem' result on 
        /// switching to editor mode)
        /// </summary>
        /// <param name="seconds">Delay in seconds</param>
        /// <param name="cancelCondition">Use this parameter if you need to stop waiting</param>
        /// <param name="onComplete">Invokes when process is complete</param>
        /// <param name="onCanceled">Invokes when process stopped by [cancel condition]</param>
        /// <param name="skipFrames">Delay in frames between checking. Use for heavy tasks or for physics checking</param>
        /// <returns></returns>
        public static async Task<EAsyncOperationResult> Delay(float seconds, Func<bool> cancelCondition, Action onComplete, Action onCanceled, byte skipFrames = 0)
        {
            float endTime = Time.time + seconds;

            while (Time.time < endTime)
            {
                EAsyncOperationResult skipFramesResult = await SkipFrames(skipFrames);
                if (skipFramesResult == EAsyncOperationResult.CanceledBySystem)
                    return EAsyncOperationResult.CanceledBySystem;

                if (cancelCondition.Exist() && cancelCondition())
                {
                    onCanceled?.Invoke();
                    return EAsyncOperationResult.Canceled;
                }

                await Task.Yield();
            }

            onComplete?.Invoke();
            return EAsyncOperationResult.Completed;
        }

        /// <summary>
        /// Waits while time is up. Safe for using in playmode (returns 'Canceled by sytem' result on 
        /// switching to editor mode)
        /// </summary>
        /// <param name="seconds">Delay in seconds</param>
        /// <param name="cancelCondition">Use this parameter if you need to stop waiting</param>
        /// <param name="skipFrames">Delay in frames between checking. Use for heavy tasks or for physics checking</param>
        /// <returns></returns>
        public static async Task<EAsyncOperationResult> Delay(float seconds, Func<bool> cancelCondition, byte skipFrames = 0)
        {
            return await Delay(seconds, cancelCondition, null, null, skipFrames);
        }

        /// <summary>
        /// Waits while time is up. Safe for using in playmode (returns 'Canceled by sytem' result on 
        /// switching to editor mode)
        /// </summary>
        /// <param name="seconds">Delay in seconds</param>
        /// <param name="skipFrames">Delay in frames between checking. Use for heavy tasks or for physics checking</param>
        /// <returns></returns>
        public static async Task<EAsyncOperationResult> Delay(float seconds, byte skipFrames = 0)
        {
            return await Delay(seconds, null, null, null, skipFrames);
        }

        /// <summary>
        /// Waits while time is up. Safe for using in playmode (returns 'Canceled by sytem' result on 
        /// switching to editor mode)
        /// </summary>
        /// <param name="seconds">Delay in seconds</param>
        /// <param name="cancelCondition"></param>
        /// <param name="onComplete">Invokes when process is complete</param>
        /// <param name="onCanceled">Invokes when process stopped by [cancel condition]</param>
        /// <param name="skipFrames">Delay in frames between checking. Use for heavy tasks or for physics checking</param>
        /// <returns></returns>
        public static async Task<EAsyncOperationResult> DelayRealtime(float seconds, Func<bool> cancelCondition, Action onComplete, Action onCanceled, byte skipFrames = 0)
        {
            float endTime = Time.unscaledTime + seconds;

            while (Time.unscaledTime < endTime)
            {
                EAsyncOperationResult skipFramesResult = await SkipFrames(skipFrames);
                if (skipFramesResult == EAsyncOperationResult.CanceledBySystem)
                    return EAsyncOperationResult.CanceledBySystem;

                if (cancelCondition.Exist() && cancelCondition())
                {
                    onCanceled?.Invoke();
                    return EAsyncOperationResult.Canceled;
                }

                await Task.Yield();
            }

            onComplete?.Invoke();
            return EAsyncOperationResult.Completed;
        }

        /// <summary>
        /// Waits while time is up. Safe for using in playmode (returns 'Canceled by sytem' result on 
        /// switching to editor mode)
        /// </summary>
        /// <param name="seconds">Delay in seconds</param>
        /// <param name="cancelCondition">Use this parameter if you need to stop waiting</param>
        /// <param name="skipFrames">Delay in frames between checking. Use for heavy tasks or for physics checking</param>
        /// <returns></returns>
        public static async Task<EAsyncOperationResult> DelayRealtime(float seconds, Func<bool> cancelCondition, byte skipFrames = 0)
        {
            return await DelayRealtime(seconds, cancelCondition, null, null, skipFrames);
        }

        /// <summary>
        /// Waits while time is up. Safe for using in playmode (returns 'Canceled by sytem' result on 
        /// switching to editor mode)
        /// </summary>
        /// <param name="seconds">Delay in seconds</param>
        /// <param name="skipFrames">Delay in frames between checking. Use for heavy tasks or for physics checking</param>
        /// <returns></returns>
        public static async Task<EAsyncOperationResult> DelayRealtime(float seconds, byte skipFrames = 0)
        {
            return await DelayRealtime(seconds, null, null, null, skipFrames);
        }

        /// <summary>
        /// Waits for the specified number of frames
        /// </summary>
        /// <param name="frames">Frames to skip</param>
        /// <param name="cancelCondition">Use this parameter if you need to stop waiting</param>
        /// <param name="onComplete">Invokes when process is complete</param>
        /// <param name="onCanceled">Invokes when process stopped by [cancel condition]</param>
        /// <returns></returns>
        public static async Task<EAsyncOperationResult> SkipFrames(byte frames, Func<bool> cancelCondition, Action onComplete, Action onCanceled)
        {
            byte skippedFrames = 0;
            while (skippedFrames < frames)
            {
                if (ShouldBeCanceledBySystem())
                {
                    return EAsyncOperationResult.CanceledBySystem;
                }

                if (ShouldFreeze())
                {
                    continue;
                }

                skippedFrames++;

                if (cancelCondition.Exist() && cancelCondition())
                {
                    onCanceled?.Invoke();
                    return EAsyncOperationResult.Canceled;
                }

                await Task.Yield();
            }

            onComplete?.Invoke();
            return EAsyncOperationResult.Completed;
        }

        /// <summary>
        /// Waits for the specified number of frames
        /// </summary>
        /// <param name="frames">Frames to skip</param>
        /// <param name="cancelCondition">Use this parameter if you need to stop waiting</param>
        /// <returns></returns>
        public static async Task<EAsyncOperationResult> SkipFrames(byte frames, Func<bool> cancelCondition)
        {
            return await SkipFrames(frames, cancelCondition, null, null);
        }

        /// <summary>
        /// Waits for the specified number of frames
        /// </summary>
        /// <param name="frames">Frames to skip</param>
        /// <returns></returns>
        public static async Task<EAsyncOperationResult> SkipFrames(byte frames)
        {
            return await SkipFrames(frames, null, null, null);
        }

        /// <summary>
        /// Waits until [complete condition] return true
        /// </summary>
        /// <param name="completeCondition"></param>
        /// <param name="cancelCondition">Use this parameter if you need to stop waiting</param>
        /// <param name="onComplete">Invokes when process is complete</param>
        /// <param name="onCanceled">Invokes when process stopped by [cancel condition]</param>
        /// <param name="skipFrames">Frames to skip</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static async Task<EAsyncOperationResult> WaitUntil(Func<bool> completeCondition, Func<bool> cancelCondition, Action onComplete, Action onCanceled, byte skipFrames = 0)
        {
            if (completeCondition.NotExist())
            {
                throw new ArgumentNullException(nameof(completeCondition));
            }

            while (!completeCondition.Invoke())
            {
                EAsyncOperationResult skipFramesResult = await SkipFrames(skipFrames);
                if (skipFramesResult == EAsyncOperationResult.CanceledBySystem)
                    return EAsyncOperationResult.CanceledBySystem;

                if (cancelCondition.Exist() && cancelCondition())
                {
                    onCanceled?.Invoke();
                    return EAsyncOperationResult.Canceled;
                }

                await Task.Yield();
            }

            onComplete?.Invoke();
            return EAsyncOperationResult.Completed;
        }

        /// <summary>
        /// Waits until [complete condition] return true
        /// </summary>
        /// <param name="completeCondition"></param>
        /// <param name="cancelCondition">Use this parameter if you need to stop waiting</param>
        /// <param name="skipFrames">Frames to skip</param>
        /// <returns></returns>
        public static async Task<EAsyncOperationResult> WaitUntil(Func<bool> completeCondition, Func<bool> cancelCondition, byte skipFrames = 0)
        {
            return await WaitUntil(completeCondition, cancelCondition, null, null, skipFrames);
        }

        /// <summary>
        /// Waits until [complete condition] return true
        /// </summary>
        /// <param name="completeCondition"></param>
        /// <param name="skipFrames">Frames to skip</param>
        /// <returns></returns>
        public static async Task<EAsyncOperationResult> WaitUntil(Func<bool> completeCondition, byte skipFrames = 0)
        {
            return await WaitUntil(completeCondition, null, null, null, skipFrames);
        }

        /// <summary>
        /// Calls [on tick] delegate each [skip frames] frames until the [complete condition] returning false
        /// </summary>
        /// <param name="completeCondition"></param>
        /// <param name="cancelCondition">Use this parameter if you need to stop waiting</param>
        /// <param name="onTick">Called every tick (after each [skip frames] frames)</param>
        /// <param name="onComplete">Invokes when process is complete</param>
        /// <param name="onCanceled">Invokes when process stopped by [cancel condition]</param>
        /// <param name="skipFrames">Frames to skip</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static async Task<EAsyncOperationResult> RepeatUntil(Func<bool> completeCondition, Func<bool> cancelCondition, Action onTick, Action onComplete, Action onCanceled, byte skipFrames = 0)
        {
            if (completeCondition.NotExist())
            {
                throw new ArgumentNullException(nameof(completeCondition));
            }

            if (onTick.NotExist())
            {
                throw new ArgumentNullException(nameof(onTick));
            }

            while (!completeCondition.Invoke())
            {
                EAsyncOperationResult skipFramesResult = await SkipFrames(skipFrames);
                if (skipFramesResult == EAsyncOperationResult.CanceledBySystem)
                    return EAsyncOperationResult.CanceledBySystem;

                if (cancelCondition.Exist() && cancelCondition())
                {
                    onCanceled?.Invoke();
                    return EAsyncOperationResult.Canceled;
                }

                onTick?.Invoke();
                await Task.Yield();
            }

            onComplete?.Invoke();
            return EAsyncOperationResult.Completed;
        }

        /// <summary>
        /// Calls [on tick] delegate each [skip frames] frames until the [complete condition] returning false
        /// </summary>
        /// <param name="completeCondition"></param>
        /// <param name="cancelCondition">Use this parameter if you need to stop waiting</param>
        /// <param name="onTick"></param>
        /// <param name="skipFrames">Frames to skip</param>
        /// <returns></returns>
        public static async Task<EAsyncOperationResult> RepeatUntil(Func<bool> completeCondition, Func<bool> cancelCondition, Action onTick, byte skipFrames = 0)
        {
            return await RepeatUntil(completeCondition, cancelCondition, onTick, null, null, skipFrames);
        }

        /// <summary>
        /// Calls [on tick] delegate each [skip frames] frames until the [complete condition] returning false
        /// </summary>
        /// <param name="completeCondition"></param>
        /// <param name="onTick">Called every tick (after each [skip frames] frames)</param>
        /// <param name="skipFrames">Frames to skip</param>
        /// <returns></returns>
        public static async Task<EAsyncOperationResult> RepeatUntil(Func<bool> completeCondition, Action onTick, byte skipFrames = 0)
        {
            return await RepeatUntil(completeCondition, null, onTick, null, null, skipFrames);
        }

        private static bool ShouldFreeze()
        {
            return Application.isFocused;
        }

        private static bool ShouldBeCanceledBySystem()
        {
            return !Application.isPlaying;
        }
    }
}
