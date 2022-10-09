public class Singleton<T> where T : class, new()
{
    public static T Inst
    {
        get
        {
            lock (locks)
            {
                if (inst == null)
                {
                    inst = new T();
                }

                return inst;
            }
        }
    }
    private static T inst;
    private static object locks = new object();
}
