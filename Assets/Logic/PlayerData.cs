using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Frixu.BouncyHero
{
    /// <summary> Contains information about the player's progress. </summary>
    [XmlRoot("SaveData")]
    public class PlayerData
    {
        /// <summary> The longest time player survived. </summary>
        [XmlIgnore] public TimeSpan BestTime = TimeSpan.Zero;
        [XmlElement("BestTime")]
        public long BestTimeString
        {
            get => BestTime.Ticks;
            set => BestTime = TimeSpan.FromTicks(value);
        }

        private static readonly XmlSerializer SaveDataSerializer;

        static PlayerData()
        {
            SaveDataSerializer = new XmlSerializer(typeof(PlayerData));
        }

        /// <summary> Convert this data object to string. </summary>
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

        /// <summary> Create new data object from a string. </summary>
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
    }
}
