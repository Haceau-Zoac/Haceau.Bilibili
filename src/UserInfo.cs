using System;
using System.IO;
using System.Net.Http;

namespace Haceau.Bilibili
{
    public class UserInfo
    {
        public int code;
        public string message;
        public int ttl;
        public Data data;

        public class Data
        {
            public string birthday;
            public int coins;
            public string face;
            public bool fans_badge;
            public bool is_follwed;
            public int jointime;
            public int level;
            public int mid;
            public int moral;
            public string name;
            public Nameplate nameplate;
            public Official official;
            public Pendant pendant;
            public int rank;
            public string sex;
            public string sign;
            public int silence;
            public object sys_notice;
            public object theme;
            public string top_photo;

            public class Nameplate
            {
                /// <summary>
                /// 勋章图标保存路径
                /// </summary>
                public string NameplateSource
                {
                    get;
                    set;
                }
                /// <summary>
                /// 小勋章图标保存路径
                /// </summary>
                public string SmallNameplateSource
                {
                    get;
                    set;
                }

                public string condition;
                public string image;
                public string image_small;
                public string level;
                public string name;
                public int nid;

                /// <summary>
                /// 下载勋章图标
                /// </summary>
                public void DownloadNameplateImage(HttpClient client)
                {
                    if (NameplateSource == null)
                    {
                        throw new ArgumentNullException("NameplateSource 不能为 null 。");
                    }
                    string imageUrl = this.image;
                    if (imageUrl == null)
                    {
                        throw new Exception("该用户没有佩戴成就勋章。");
                    }
                    byte[] image = client.GetByteArrayAsync(imageUrl).Result;
                    string src = NameplateSource;
                    string exten = Path.GetExtension(imageUrl);
                    src = Tools.SetExtension(src, exten);
                    Tools.Save(src, image);
                }
                /// <summary>
                /// 下载小勋章图标
                /// </summary>
                public void DownloadSmallNameplateImage(HttpClient client)
                {
                    if (SmallNameplateSource == null)
                    {
                        throw new ArgumentNullException("NameplateSource 不能为 null 。");
                    }
                    string imageUrl = image_small;
                    if (imageUrl == null)
                    {
                        throw new Exception("该用户没有佩戴成就勋章。");
                    }
                    byte[] image = client.GetByteArrayAsync(imageUrl).Result;
                    string src = NameplateSource;
                    string exten = Path.GetExtension(imageUrl);
                    src = Tools.SetExtension(src, exten);
                    Tools.Save(src, image);
                }
            }
            public class Official
            {
                public int role;
                public string title;
                public string desc;
                public int type;
            }
            public class Pendant
            {
                public int expire;
                public string image;
                public string image_enhance;
                public string name;
                public int pid;
            }
            public class Vip
            {
                public int avatar_subscript;
                public Label label;
                public string nickname_color;
                public int status;
                public int theme_type;
                public int type;

                public class Label
                {
                    public string label_theme;
                    public string path;
                    public string text;
                }
            }
        }
    }
}
