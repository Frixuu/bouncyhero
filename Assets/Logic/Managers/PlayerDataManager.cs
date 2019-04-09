using System.IO;
using System.Threading.Tasks;
using Unity.Entities;
using UnityEngine;

namespace Frixu.BouncyHero.Managers
{
    public class PlayerDataManager : ComponentSystem
    {
        public PlayerData Data { get; private set; } 
        /// <summary> Name of the save file. </summary>
        private const string SaveFileName = "bouncyherosave.xml";
        /// <summary> Full path to the save file. </summary>
        private static readonly string SavePath;

        static PlayerDataManager()
        {
            SavePath = Path.Combine(Application.persistentDataPath, SaveFileName);
        }

        public PlayerDataManager()
        {
            Data = new PlayerData();
        }

        protected override void OnCreate()
        {
            World.Active.GetExistingSystem<LifeManager>().Killed += async delegate
            {
                var time = World.Active.GetExistingSystem<TimeManager>().CurrentTime;
                if (time > Data.BestTime)
                {
                    Data.BestTime = time;
                    await Save();
                }
            };
        }

        /// <summary> Loads saved player data from a file. </summary>
        public async Task Load()
        {
            try
            {
                using (var reader = File.OpenText(SavePath))
                {
                    var content = await reader.ReadToEndAsync();
                    Data = await PlayerData.Deserialize(content);
                }
            }
            // First run? 
            catch (FileNotFoundException)
            {
                await Save();
            }
        }

        /// <summary> Saves player data to a file. </summary>
        public async Task Save()
        {
            using (var writer = new StreamWriter(SavePath, false))
            {
                await writer.WriteAsync(await Data.Serialize());
            }
        }

        protected override void OnUpdate()
        {
            
        }
    }
}
