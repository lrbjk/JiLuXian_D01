using AK;
using Audio.Sys;
using Audio.WwiseAudio.LoadInfos;
using Common;
using Helper;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
namespace Audio.WwiseAudio
{
    /// <summary>
    /// 描述：wwise实现的音频系统
    /// </summary>
    public class WwiseAudioSystem : AudioSystem
    {
        private BiDictionary<uint, string> bankID2Name;
        private BiDictionary<uint, string> eventID2Name;
        private BiDictionary<uint, string> rtpcID2Name;
        private Dictionary<uint, uint> eventID2sbID;
        private Dictionary<Sys.AudioType, uint> audioType2RtpcID;
        private readonly Dictionary<uint, bool> bankStatus = new Dictionary<uint, bool>();
        private RtpcConfig rtpcConfig;

        protected override void Init()
        {
            base.Init();
            InitIDMaps();
            //默认先载入Initbank
            LoadBank(BANKS.INIT);
            Debug.Log("载入InitBank");
        }
        private void InitIDMaps()
        {
            bankID2Name = new BiDictionary<uint, string>();
            eventID2Name = new BiDictionary<uint, string>();
            rtpcID2Name = new BiDictionary<uint, string>();
            eventID2sbID = new Dictionary<uint, uint>(64);
            audioType2RtpcID = new Dictionary<Sys.AudioType, uint>(16);
            //利用反射直接获取Wwise_ids
            foreach (var item in typeof(BANKS).GetFields())
            {
                bankID2Name.Add((uint)item.GetValue(null), item.Name);
            }
            foreach (var item in typeof(EVENTS).GetFields())
            {
                eventID2Name.Add((uint)item.GetValue(null), item.Name);
            }
            foreach (var item in typeof(GAME_PARAMETERS).GetFields())//暂时只有rtpc
            {
                rtpcID2Name.Add((uint)item.GetValue(null), item.Name);
            }
            rtpcConfig = Common.ResourceManager.Load<RtpcConfig>("RtpcConfig");
            foreach (var item in rtpcConfig.audioType2RTPCIDArray)
            {
                audioType2RtpcID[item.key] = item.rtpcID;
            }

            //载入json文件
            string info = ConfigurationReader.GetConfigFile("Audio/GeneratedSoundBanks/Windows/SoundbanksInfo.json");
            SoundBanksInfoRoot config = JsonConvert.DeserializeObject<SoundBanksInfoRoot>(info);
            //生成event=>bank映射字典(跳过initbank)
            foreach (var sb in config.SoundBanksInfo.SoundBanks.GetRange(1, config.SoundBanksInfo.SoundBanks.Count - 1))
            {
                foreach (var evt in sb.IncludedEvents)
                    eventID2sbID.Add(uint.Parse(evt.Id), uint.Parse(sb.Id));
            }
        }

        public override void StopAllAudio(GameObject targetObject)
        {
            AkSoundEngine.StopAll(targetObject);
        }
        public override void StopAllAudio()
        {
            AkSoundEngine.StopAll();
        }
        public override void PlayAudio(string eventName, GameObject targetObject)
        {
            uint eventID = eventID2Name.GetByValue(eventName);
            PlayAudio(eventID, targetObject);
        }
        public override void SetAudioVolume(Sys.AudioType audioType, float value)
        {
            SetRTTCValue(audioType2RtpcID[audioType], value);
        }

        #region 使用字符串
        /// <summary>
        /// 加载指定bank，若已经加载则不重复加载
        /// </summary>
        /// <param name="bankName"></param>
        public void LoadBank(string bankName)
        {
            uint bankID = bankID2Name.GetByValue(bankName.ToUpper());
            LoadBank(bankID);
        }
        public void LoadBankAsync(string bankName, AkCallbackManager.BankCallback bankCallback)
        {
            uint sbID = bankID2Name.GetByValue(bankName.ToUpper());
            LoadBankAsync(sbID, bankCallback);
        }
        public void UnloadBank(string bankName)
        {
            uint bankID = bankID2Name.GetByValue(bankName.ToUpper());
            UnloadBank(bankID);
        }
        #endregion

        #region 使用id
        public void LoadBank(uint bankID)
        {
            if (!ShouldLoadBank(bankID)) { return; }
            AKRESULT res = AkSoundEngine.LoadBank(bankID);

            if (res == AKRESULT.AK_Success || res == AKRESULT.AK_BankAlreadyLoaded)
            {
                Debug.Log($"SoundBank {bankID2Name.GetByKey(bankID)} loaded successfully (ID: {bankID})");
                bankStatus[bankID] = true;
            }
            else
            {
                Debug.LogError($"Failed to load SoundBank {bankID2Name.GetByKey(bankID)}: {bankID} :AkRes:{res}");
                bankStatus[bankID] = false;
            }
        }
        public void LoadBankAsync(uint bankID, AkCallbackManager.BankCallback bankCallback)
        {
            if (!ShouldLoadBank(bankID)) { return; }

            bankStatus[bankID] = false;
            AkCallbackManager.BankCallback callback = (in_bankID, _, in_eLoadResult, _) =>
            {
                if (in_eLoadResult == AKRESULT.AK_Success || in_eLoadResult == AKRESULT.AK_BankAlreadyLoaded)
                {
                    Debug.Log($"SoundBank {bankID2Name.GetByKey(bankID)} loaded/alreadLoaded successfully (ID: {in_bankID})");
                    bankStatus[bankID] = true;
                }
                else
                    Debug.LogError($"Failed to load SoundBank {bankID2Name.GetByKey(bankID)}: AkRes: {in_eLoadResult}");
            };
            callback += bankCallback;
            AkSoundEngine.LoadBank(bankID, callback, null);
        }
        public void UnloadBank(uint bankID)
        {
            AkSoundEngine.UnloadBank(bankID, System.IntPtr.Zero);
            bankStatus[bankID] = false;
        }
        public void UnloadBankAsync(uint bankID, AkCallbackManager.BankCallback bankCallback)
        {
            AkCallbackManager.BankCallback callback = (in_bankID, in_InMemoryBankPtr, in_eLoadResult, in_Cookie) =>
            {
                if (in_eLoadResult == AKRESULT.AK_Success)
                {
                    Debug.Log($"SoundBank {bankID2Name.GetByKey(in_bankID)} unloaded successfully (ID: {in_bankID})");
                    bankStatus[bankID] = false;
                }
                else
                {
                    Debug.LogError($"Failed to unload SoundBank {bankID2Name.GetByKey(in_bankID)}: AkRes: {in_eLoadResult}");
                }
            };//先打印是否成功
            callback += bankCallback;
            AkSoundEngine.UnloadBank(bankID, System.IntPtr.Zero, callback, null);
        }
        public void PlayAudio(uint eventID, GameObject targetObject)
        {
            LoadBank(eventID2sbID[eventID]);
            AkSoundEngine.PostEvent(eventID, targetObject);
        }
        #endregion
        private bool ShouldLoadBank(uint bankID)
        {
            if (bankStatus.TryGetValue(bankID, out bool isLoaded) && isLoaded)
            {
                //Debug.Log($"SoundBank {bankName} is already loaded.");
                return false;
            }

            return true;
        }
        public void UnloadAllBanks(bool shouldUnloadInitBank = false)
        {
            var loadedBankIDs = bankStatus
                .Where(kv => kv.Value)
                .Select(kv => kv.Key)
                .ToList();
            if (!shouldUnloadInitBank)
            {
                loadedBankIDs = loadedBankIDs.FindAll(id => id != BANKS.INIT);
            }

            foreach (var bankID in loadedBankIDs)
            {
                UnloadBank(bankID);
                bankStatus[bankID] = false;
            }
        }
        public void UnloadAllBanksAsync(System.Action onAllUnloaded, bool shouldUnloadInitBank = false)
        {
            var loadedBankIDs = bankStatus
                .Where(kv => kv.Value)
                .Select(kv => kv.Key)
                .ToList();
            if (!shouldUnloadInitBank)
            {
                loadedBankIDs = loadedBankIDs.FindAll(id => id != BANKS.INIT);
            }

            if (loadedBankIDs.Count == 0)
            {
                onAllUnloaded?.Invoke();
                return;
            }

            int remaining = loadedBankIDs.Count;
            foreach (uint sbID in loadedBankIDs)
            {
                UnloadBankAsync(sbID, (in_bankID, _, result, _) =>
                {
                    // 所有回调完成后触发完成通知
                    if (System.Threading.Interlocked.Decrement(ref remaining) == 0)
                    {
                        onAllUnloaded?.Invoke();
                    }
                });
            }
        }
        public void SetRTTCValue(uint rtpcID, float value)
        {
            AkSoundEngine.SetRTPCValue(rtpcID, value);
        }

    }
}