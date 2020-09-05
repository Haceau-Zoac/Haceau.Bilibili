namespace Haceau.Bilibili
{
    public class UserStat
    {
        public int code;
        public string message;
        public int ttl;
        public Data data;

        public class Data
        {
            public int mid;
            public int following;
            public int whisper;
            public int black;
            public int follower;
        }
    }
}
