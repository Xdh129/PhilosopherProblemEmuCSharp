using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PhilosopherProblemEmu
{
    internal class Chopstick
    {
        private readonly int numberID; //筷子序号
        private SemaphoreSlim chopstickMutex = new SemaphoreSlim(1,1); //每一个筷子对象均有独立的互斥信号量

        public Chopstick(int number)
        {
            this.numberID = number;
        }

        public void PickUp()
        {
            /*
            if(!isAvailable()){
                GUI.outputLogToConsole("等待筷子");
            }else {


                try {
                    chopstickMutex.acquire();
                } catch (ThreadInterruptedException e) {

                    GUI.outputLogToConsole("没有取到筷子");
                    e.printStackTrace();
                }
            }

             */

            try
            {
                chopstickMutex.Wait(); //拿起筷子，信号量值减1，相当于执行P操作
            }
            catch (ThreadInterruptedException e)
            {
                //e.printStackTrace();
            }


        }

        public void PutDown()
        {
            chopstickMutex.Release(); //放下筷子，信号量值加1，相当于执行V操作
        }

        public bool IsAvailable()
        {
            return chopstickMutex.CurrentCount > 0; //检查当前筷子对象是否可用
        }

        public int GetAvailablePermits()
        {
            return chopstickMutex.CurrentCount;  //返回筷子对象信号量
        }

        public int GetNumberID()
        {
            return numberID + 1; //返回筷子序号
        }
    }
}
