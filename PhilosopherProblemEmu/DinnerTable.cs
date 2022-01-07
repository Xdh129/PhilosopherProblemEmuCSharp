using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhilosopherProblemEmu
{
    internal class DinnerTable
    {
        private volatile byte currentDinnerStatus = 0; //定义餐桌状态代码
        private int peopleCount; //定义餐桌容纳人数
        
        public List<Philosopher> philosophers = new List<Philosopher>(); //用于存储哲学家对象的表
        public List<Chopstick> chopsticks = new List<Chopstick>(); //用于存储筷子对象的表


        /*
        private enum Singleton{
            INSTANCE;

            private DinnerTable dinnerTableInstance;

            Singleton(){
                dinnerTableInstance=new DinnerTable();
            }
        }

        public static DinnerTable getInstance(){
            return DinnerTable.Singleton.INSTANCE.dinnerTableInstance;
        }
        */


        public void InitDinnerTable(int peopleCount)
        { //初始化餐桌
            this.peopleCount = peopleCount;
            chopsticks.Clear(); //将存储哲学家对象的表还原为空
            philosophers.Clear(); //将存储筷子对象的表还原为空

            for (int k = 0; k < peopleCount; ++k)
            {
                Chopstick newChopstick = new Chopstick(k); //新建筷子对象
                chopsticks.Insert(k, newChopstick); //将新建的筷子对象加入到表中
            }

            Philosopher tempWorkaround = new Philosopher(0, null, null); // temp workaround for an unknown bug, do not remove
            tempWorkaround.ReNewCancellationSourceToken();
            tempWorkaround = null;

            Philosopher firstPhilosopher = new Philosopher(0, chopsticks[0], chopsticks[peopleCount - 1]);
            firstPhilosopher.ReNewCancellationSourceToken();
            philosophers.Insert(0, firstPhilosopher);

            for (int k = 1; k < peopleCount; ++k)
            {
                Philosopher newPhilosopher = new Philosopher(k, chopsticks[k], chopsticks[k - 1]);
                newPhilosopher.ReNewCancellationSourceToken();
                philosophers.Insert(k, newPhilosopher); //将新建的哲学家对象加入到表中
            }

            //currentDinnerStatus=2;
        }

        public void OpenDinnerTable()
        { //开启餐桌
            for (int k = 0; k < peopleCount; ++k)
            {
                philosophers[k].StartThread(); //依次启动各个哲学家对象进程
            }
            currentDinnerStatus = 2; //将当前餐桌状态设置为开始
        }

        public void PauseDinner()
        { //暂停哲学家进餐模拟过程
            for (int k = 0; k < peopleCount; ++k)
            {
                philosophers[k].PauseThread(); //依次暂停各个哲学家对象进程
            }
            currentDinnerStatus = 1; //将当前餐桌状态设置为暂停
        }

        public void ResumeDinner()
        { //继续哲学家进餐模拟过程
            for (int k = 0; k < peopleCount; ++k)
            {
                philosophers[k].ResumeThread(); //依次继续各个哲学家对象进程
            }
            currentDinnerStatus = 2; //将当前餐桌状态设置为开始
        }

        public void CloseDinnerTable()
        { //关闭餐桌，重新定义餐桌人数时调用
            for (int k = 0; k < peopleCount; ++k)
            {
                philosophers[k].StopThread(); //依次强行停止各个哲学家对象进程
            }
            currentDinnerStatus = 0; //将当前餐桌状态设置为停止
        }

        public int GetDinnerPeopleCount()
        {
            return peopleCount; //返回当前餐桌人数
        }

        public byte GetDinnerStatusID()
        {
            return currentDinnerStatus; //返回当前餐桌状态代码
        }

        public void SetPhilosophersSleepTime(int minRandomThinkingTime, int maxRandomThinkingTime, int minRandomEatingTime, int maxRandomEatingTime)
        {
            for (int k = 0; k < peopleCount; ++k)
            {
                philosophers[k].SetRandomSleepTime(minRandomThinkingTime, maxRandomThinkingTime, minRandomEatingTime, maxRandomEatingTime); //依次设置各个哲学家的随机时间范围
            }
        }

        public int GetPhilosophersMinThinkingTime()
        {
            return philosophers[0].GetMinRandomThinkingTime(); //返回哲学家最小思考时间
        }

        public int GetPhilosophersMaxThinkingTime()
        {
            return philosophers[0].GetMaxRandomThinkingTime(); //返回哲学家最大思考时间
        }

        public int GetPhilosophersMinEatingTime()
        {
            return philosophers[0].GetMinRandomEatingTime(); //返回哲学家最小进食时间
        }

        public int GetPhilosophersMaxEatingTime()
        {
            return philosophers[0].GetMaxRandomEatingTime(); //返回哲学家最大进食时间
        }
    }
}
