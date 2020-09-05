using System;
using System.Drawing;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;

namespace Haceau.Bilibili
{
    public class User
    {
        protected readonly HttpClient client;

        /// <summary>
        /// 用户uid
        /// </summary>
        public long Uid { get; set; }

        /// <summary>
        /// 头像保存路径
        /// </summary>
        public string AvatarSource
        {
            get;
            set;
        }

        /// <summary>
        /// 个人空间url
        /// </summary>
        public string PersonalSpaceUrl
        {
            get => $"https://space.bilibili.com/{Uid}";
        }

        /// <summary>
        /// 个人空间uri
        /// </summary>
        public Uri PersonalSpaceUri
        {
            get => new Uri(PersonalSpaceUrl);
        }

        /// <summary>
        /// 统计
        /// </summary>
        public UserStat Stat
        {
            get
            {
                string json = client.GetStringAsync($"https://api.bilibili.com/x/relation/stat?vmid={Uid}&jsonp=jsonp").Result;
                return JsonConvert.DeserializeObject<UserStat>(json);
            }
        }

        /// <summary>
        /// 数据
        /// </summary>
        public UserInfo Info
        {
            get
            {
                string json = client.GetStringAsync($"https://api.bilibili.com/x/space/acc/info?mid={Uid}&jsonp=jsonp").Result;
                UserInfo info = JsonConvert.DeserializeObject<UserInfo>(json);
                return info;
            }
        }

        /// <summary>
        /// 粉丝数
        /// </summary>
        public int FollowerNumber
        {
            get
            {
                UserStat stat = Stat;
                if (stat.code != 0)
                {
                    throw new Exception(stat.message);
                }
                return stat.data.follower;
            }
        }

        /// <summary>
        /// 关注数
        /// </summary>
        public int FollowingNumber
        {
            get
            {
                UserStat stat = Stat;
                if (stat.code != 0)
                {
                    throw new Exception(stat.message);
                }
                return stat.data.following;
            }
        }

        /// <summary>
        /// 拉黑数
        /// </summary>
        public int BlackNumber
        {
            get
            {
                UserStat stat = Stat;
                if (stat.code != 0)
                {
                    throw new Exception(stat.message);
                }
                return stat.data.black;
            }
        }

        /// <summary>
        /// 等级
        /// </summary>
        public int Level
        {
            get
            {
                UserInfo info = Info;
                if (info.code != 0)
                {
                    throw new Exception(info.message);
                }
                return info.data.level;
            }
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get
            {
                UserInfo info = Info;
                if (info.code != 0)
                {
                    throw new Exception(info.message);
                }
                return info.data.name;
            }
        }

        /// <summary>
        /// 性别
        /// </summary>
        public string Sex
        {
            get
            {
                UserInfo info = Info;
                if (info.code != 0)
                {
                    throw new Exception(info.message);
                }
                return info.data.sex;
            }
        }

        /// <summary>
        /// 简介
        /// </summary>
        public string Introduction
        {
            get
            {
                UserInfo info = Info;
                if (info.code != 0)
                {
                    throw new Exception(info.message);
                }
                return info.data.sign;
            }
        }

        /// <summary>
        /// 成就勋章
        /// </summary>
        public UserInfo.Data.Nameplate Nameplate
        {
            get
            {
                UserInfo info = Info;
                if (info.code != 0)
                {
                    throw new Exception(info.message);
                }
                return info.data.nameplate;
            }
        }

        /// <summary>
        /// 拥有粉丝勋章
        /// </summary>
        public bool HaveFansBadge
        {
            get
            {
                UserInfo info = Info;
                if (info.code != 0)
                {
                    throw new Exception(info.message);
                }
                return info.data.fans_badge;
            }
        }

        /// <summary>
        /// 获取头像
        /// </summary>
        /// <returns>image</returns>
        public Image Avatar
        {
            get
            {
                if (AvatarSource == null)
                {
                    throw new ArgumentNullException("AvatarSource 不可为 null 。");
                }
                DownloadAvatar();
                Image image = Image.FromFile(AvatarSource);
                return image;
            }
        }

        /// <summary>
        /// 下载头像
        /// </summary>
        public void DownloadAvatar()
        {
            if (AvatarSource == null)
            {
                throw new ArgumentNullException("AvatarSource 不能为 null 。");
            }
            UserInfo info = Info;
            if (info.code != 0)
            {
                throw new Exception(info.message);
            }
            string imageUrl = info.data.face;
            byte[] image = client.GetByteArrayAsync(imageUrl).Result;
            string src = AvatarSource;
            string exten = Path.GetExtension(imageUrl);
            src = Tools.SetExtension(src, exten);
            Tools.Save(src, image);
        }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public User(HttpClient client) =>
            this.client = client;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="uid">UID</param>
        public User(long uid, HttpClient client)
        {
            Uid = uid;
            this.client = client;
        }
    }
}
