using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PhilosopherProblemEmu
{
    internal class Philosopher : CustomThread
    {

        private byte statusID = 0; //哲学家状态
        private int numberID; //哲学家序号
        private volatile bool isLeftHanded = false; //哲学家是否为左撇子
        private volatile bool isDisabled = false; //哲学家双手是否被禁用
        private Chopstick leftChopstick, rightChopstick; //哲学家能够使用的筷子对象

        private int minRandomThinkingTime = 3000; //最小思考时间
        private int maxDiffRandomThinkingTime = 8000; //思考时间最大随机范围
        private int minRandomEatingTime = 0; //最小进食时间
        private int maxDiffRandomEatingTime = 8000; //进食时间最大随机范围

        public Philosopher(int number, Chopstick leftChopstick, Chopstick rightChopstick)
        {
            this.numberID = number;
            this.leftChopstick = leftChopstick;
            this.rightChopstick = rightChopstick;

            task = new Task(() =>
            {
                Run();
            }, cancellationToken);
        }

        public void Run()
        { //哲学家线程
            while (!isCurrentThreadSignaledCanceled)
            {
                while (!isDisabled && !isCurrentThreadSignaledCanceled)
                { //判断是否禁用双手,默认全为右撇子
                    if (!isLeftHanded)
                    { //判断是否偏好左手
                        Think(); //思考中
                        rightChopstick.PickUp(); //拿起右手边筷子
                        leftChopstick.PickUp(); //拿起左手边筷子
                        Eat(); //进食中
                        rightChopstick.PutDown(); //放下右手边筷子
                        leftChopstick.PutDown(); //放下左手边筷子
                    }
                    else
                    { //左撇子代码
                        Think(); //思考中
                        leftChopstick.PickUp(); //拿起左手边筷子
                        rightChopstick.PickUp(); //拿起右手边筷子
                        Eat();//进食中
                        leftChopstick.PutDown(); //放下左手边筷子
                        rightChopstick.PutDown(); //放下右手边筷子
                    }
                }
                statusID = 0; //设置为空闲状态
                try
                {
                    Thread.Sleep(500); //若为双手禁用状态，睡眠500毫秒避免死循环高CPU占用
                }
                catch (ThreadInterruptedException e)
                {
                    //e.printStackTrace();
                }
            }
        }

        private void Think()
        {
            if (!isCurrentThreadSignaledCanceled)
            {
                statusID = 1; //设置哲学家状态为思考
                Form1.form1.OutputLogToConsole(numberID + 1 + "号思考中");

                if (maxDiffRandomThinkingTime != 0)
                {
                    //threadSleep(ThreadLocalRandom.current().nextInt(maxDiffRandomThinkingTime) + minRandomThinkingTime); //睡眠随机时间
                    ThreadSleep(ThreadLocalRandom.Next(minRandomThinkingTime, maxDiffRandomThinkingTime)); //睡眠随机时间

                }
                else
                {
                    ThreadSleep(minRandomThinkingTime); //随机范围为0则睡眠最小时间
                }

                statusID = 2; //设置哲学家状态为饥饿

                /*
                try {
                    Thread.sleep(ThreadLocalRandom.current().nextInt(8000)+3000);
                } catch (InterruptedException e) {
                    //e.printStackTrace();
                }
                */
            }
        }

        private void Eat()
        {
            if (!isDisabled && !isCurrentThreadSignaledCanceled)
            {
                statusID = 3; //设置哲学家状态为进食
                Form1.form1.OutputLogToConsole(numberID + 1 + "号进食中,使用" + (rightChopstick.GetNumberID()) + "号和" + (leftChopstick.GetNumberID()) + "号筷子");

                if (maxDiffRandomEatingTime != 0)
                {
                    //threadSleep(ThreadLocalRandom.current().nextInt(maxDiffRandomEatingTime) + minRandomEatingTime); //睡眠随机时间
                    ThreadSleep(ThreadLocalRandom.Next(minRandomEatingTime,maxDiffRandomEatingTime)); //睡眠随机时间
                }
                else
                {
                    ThreadSleep(minRandomEatingTime); //随机范围为0则睡眠最小时间
                }

                /*
                try {
                    Thread.sleep(ThreadLocalRandom.current().nextInt(8000));
                } catch (InterruptedException e) {
                    //e.printStackTrace();
                }
                */

            }
        }

        public int GetStatusID()
        {
            return statusID; //返回哲学家状态
        }

        public bool IsChopstickAvailable()
        {
            return leftChopstick.IsAvailable(); //返回左手筷子是否可用
        }

        public int GetChopstickNumberID()
        {
            return leftChopstick.GetNumberID(); //返回左手筷子序号
        }

        public int GetChopstickPermitsAmount()
        {
            return leftChopstick.GetAvailablePermits(); //返回左手筷子信号量值
        }

        public void ChangeHandPreference(bool isLeftHanded)
        {
            this.isLeftHanded = isLeftHanded; //设置该哲学家为左撇子
        }

        public void ChangeHandStatus(bool isDisabled)
        {
            this.isDisabled = isDisabled; //设置禁用哲学家双手
        }

        /*
        public void setIdle(){
            statusID=0;
        }
        */

        public void SetRandomSleepTime(int setMinRandomThinkingTime, int setMaxDiffRandomThinkingTime, int setMinRandomEatingTime, int setMaxDiffRandomEatingTime)
        {
            minRandomThinkingTime = setMinRandomThinkingTime;
            maxDiffRandomThinkingTime = setMaxDiffRandomThinkingTime;
            minRandomEatingTime = setMinRandomEatingTime;
            maxDiffRandomEatingTime = setMaxDiffRandomEatingTime;
        }

        public int GetMinRandomThinkingTime()
        {
            return minRandomThinkingTime; //返回哲学家最小思考时间
        }

        public int GetMaxRandomThinkingTime()
        {
            return minRandomThinkingTime + maxDiffRandomThinkingTime; //返回哲学家最大思考时间
        }

        public int GetMinRandomEatingTime()
        {
            return minRandomEatingTime; //返回哲学家最小进食时间
        }

        public int GetMaxRandomEatingTime()
        {
            return minRandomEatingTime + maxDiffRandomEatingTime; //返回哲学家最大进食时间
        }
    }
}
