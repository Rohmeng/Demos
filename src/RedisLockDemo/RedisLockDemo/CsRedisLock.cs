using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CSRedis;

namespace RedisLockDemo
{
    public class CsRedisLock
    {
        private static readonly int _lock_timeout = 40;
        private static readonly string _lock_key = "lock.foo";
        public static void Test()
        {
            var rds = new CSRedisClient("127.0.0.1:6379,password=123456,defaultDatabase=13,poolsize=50,ssl=false");
            RedisHelper.Initialization(rds);

            Parallel.For(0, 13, x =>
            {
                if (GetLock(_lock_key))
                {
                    Console.WriteLine($"person:{x},线程ID:{Thread.CurrentThread.ManagedThreadId},获得锁 woking");

                    if (DateTimeOffset.Now.ToUnixTimeMilliseconds() < RedisHelper.Get<long>(_lock_key))
                    {
                        //释放锁
                        RedisHelper.Del(_lock_key);
                    }
                }
                else
                {
                    Console.WriteLine($"person:{x},线程ID:{Thread.CurrentThread.ManagedThreadId},获取锁异常");
                }
            });
            Console.WriteLine();
        }

        private static bool GetLock(string key)
        {
            bool getLocked = false;
            try
            {
                while (!getLocked)
                {
                    var now = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                    var lock_time = now + _lock_timeout + 1;
                    getLocked = RedisHelper.SetNx(key, lock_time);
                    //判断是否获取锁，
                    if (getLocked || now > RedisHelper.Get<long>(key) && now > RedisHelper.GetSet<long>(key, lock_time))
                    {
                        getLocked = true;
                    }
                    else
                    {
                        Thread.Sleep(30);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return getLocked;
        }
    }
}
