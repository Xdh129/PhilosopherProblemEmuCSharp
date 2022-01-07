using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PhilosopherProblemEmu
{
    internal class CustomThread
    {
        private volatile bool isCurrentThreadPaused = false; //是否要暂停线程
        private volatile bool isCurrentThreadInterrupted = false; //是否要中断进程
        //private Object threadLock = new Object(); //线程锁
        public volatile bool isCurrentThreadSignaledCanceled = false; //是否收到过线程停止信号

        ManualResetEvent manualResetEvent = new ManualResetEvent(false);
        public static CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        public CancellationToken cancellationToken = cancellationTokenSource.Token;

        public Task task;

        public void SetPaused()
        {
            manualResetEvent.Reset(); //不允许取得锁，继续等待
        }

        public void PauseThread()
        {
            isCurrentThreadPaused = true; //发出暂停线程信号
        }

        public void InterruptThread()
        {
            isCurrentThreadInterrupted = true; //发出中断进程信号
        }

        public void StartThread()
        {
            task.Start();
        }

        public void StopThread()
        {
            cancellationTokenSource.Cancel();
            isCurrentThreadSignaledCanceled = true;
        }

        public void ResumeThread()
        {
            isCurrentThreadPaused = false;
            manualResetEvent.Set(); //通知等待线程可以取得锁
        }

        public void ThreadSleep(int sleepTime)
        { //输入需要睡眠的总时间，单位ms
            bool shouldSleeping = true; //是否继续睡眠
            int sleepTimes = sleepTime / 50; //使用单次睡眠时间计算睡眠总次数
            int sleepCount = 0; //睡眠次数
            while (!isCurrentThreadInterrupted && shouldSleeping)
            { //如果未收到线程中断信号且睡眠未结束
                while (isCurrentThreadPaused)
                {
                    SetPaused(); //使线程暂停运行，进入等待状态
                    Thread.Sleep(500); //睡眠500毫秒避免死循环高CPU占用
                }
                if (sleepCount < sleepTimes)
                {
                    try
                    {
                        Thread.Sleep(50); //单次睡眠时间，值越小可以更快醒来
                        ++sleepCount;
                    }
                    catch (ThreadInterruptedException e)
                    {
                        //e.printStackTrace();
                        break;
                    }
                }
                else
                {
                    sleepCount = 0;
                    shouldSleeping = false; //退出睡眠状态
                }
            }
            isCurrentThreadInterrupted = false;
        }

        public void ReNewCancellationSourceToken()
        {
            cancellationTokenSource = new CancellationTokenSource();
            cancellationToken = cancellationTokenSource.Token;
        }

        public static class ThreadLocalRandom
        // https://devblogs.microsoft.com/pfxteam/getting-random-numbers-in-a-thread-safe-way/
        {
            private static System.Security.Cryptography.RNGCryptoServiceProvider _global = new System.Security.Cryptography.RNGCryptoServiceProvider();
            [ThreadStatic]
            private static Random _local;

            public static int Next(int min, int max)
            {
                Random inst = _local;
                if (inst == null)
                {
                    byte[] buffer = new byte[4];
                    _global.GetBytes(buffer);
                    _local = inst = new Random(BitConverter.ToInt32(buffer, 0));
                }
                return inst.Next(min, max);
            }
        }

    }
}