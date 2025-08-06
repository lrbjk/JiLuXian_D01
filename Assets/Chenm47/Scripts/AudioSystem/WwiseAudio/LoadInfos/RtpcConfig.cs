using System.Collections.Generic;
using UnityEngine;
using AudioType = Audio.Sys.AudioType;

namespace Audio.WwiseAudio.LoadInfos
{

    [System.Serializable]
    public class AudioTypeEntry
    {
        public AudioType key;
        public uint rtpcID;
    }
    [CreateAssetMenu(menuName = "AudioSys/WwiseAudio/RtpcConfig")]
    /// <summary>
    /// 描述：
    /// </summary>
    public class RtpcConfig : ScriptableObject
    {
        public List<AudioTypeEntry> audioType2RTPCIDArray;

    }
}
