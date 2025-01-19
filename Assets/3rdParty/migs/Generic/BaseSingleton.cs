namespace migs.Generic
{
    public abstract class BaseSingleton<T> where T : new()
    {
        protected static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new T();

                return _instance;
            }
        }
    }
}