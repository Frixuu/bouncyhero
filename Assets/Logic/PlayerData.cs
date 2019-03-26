using System;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

namespace Frixu.BouncyHero
{
    [XmlRoot("SaveData")]
    public class PlayerData
    {
        [XmlIgnore] public TimeSpan BestTime = TimeSpan.Zero;
        [XmlElement("BestTime")]
        public long BestTimeString
        {
            get => BestTime.Ticks;
            set => BestTime = TimeSpan.FromTicks(value);
        }
        [XmlIgnore] public static string SavePath;
        [XmlIgnore] public static XmlSerializer SaveDataSerializer;

        static PlayerData()
        {
            SaveDataSerializer = new XmlSerializer(typeof(PlayerData));
            SavePath = Path.Combine(Application.persistentDataPath, "bouncyherosave.xml");
        }

        public async Task<string> Serialize()
        {
            return await Task.Run(() =>
            {
                using (var writer = new StringWriter())
                {
                    using (var xmlWriter = new XmlTextWriter(writer))
                    {
                        SaveDataSerializer.Serialize(xmlWriter, this);
                        return writer.ToString();
                    }
                }
            });
        }

        public static async Task<PlayerData> Deserialize(string s)
        {
            return await Task.Run(() =>
            {
                using (var reader = new StringReader(s))
                {
                    using (var xmlReader = new XmlTextReader(reader))
                    {
                        return SaveDataSerializer.Deserialize(xmlReader) as PlayerData;
                    }
                }  
            });
        }

        public static async Task<PlayerData> Load()
        {
            try
            {
                using (var reader = File.OpenText(SavePath))
                {
                    var content = await reader.ReadToEndAsync();
                    return await Deserialize(content);
                }
            }
            catch (FileNotFoundException)
            {
                var p = new PlayerData();
                await p.Save();
                return p;
            }
            
        }

        public async Task Save()
        {
            using (var writer = new StreamWriter(SavePath, false))
            {
                await writer.WriteAsync(await Serialize());
            }
        }
    }
}
