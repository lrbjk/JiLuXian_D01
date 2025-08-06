using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

*/
namespace Audio.WwiseAudio.LoadInfos
{
    /// <summary>
    /// 描述：用于读取SoundBnksInfoJSON文件的模型类
    /// </summary>
    public class SoundBanksInfoRoot
    {
        [JsonProperty("SoundBanksInfo")]
        public SoundBanksInfo SoundBanksInfo { get; set; }
    }

    public class SoundBanksInfo
    {
        public string Platform { get; set; }
        public string BasePlatform { get; set; }
        public string SchemaVersion { get; set; }
        public string SoundBankVersion { get; set; }
        public RootPaths RootPaths { get; set; }
        public List<object> DialogueEvents { get; set; }
        public List<SoundBank> SoundBanks { get; set; }
        public string FileHash { get; set; }
    }

    public class RootPaths
    {
        public string ProjectRoot { get; set; }
        public string SourceFilesRoot { get; set; }
        public string SoundBanksRoot { get; set; }
        public string ExternalSourcesInputFile { get; set; }
        public string ExternalSourcesOutputRoot { get; set; }
    }

    public class SoundBank
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Language { get; set; }
        public string Hash { get; set; }
        public string ShortName { get; set; }
        public string Path { get; set; }
        public Plugins Plugins { get; set; }
        public List<Bus> Busses { get; set; }
        public List<GameParameter> GameParameters { get; set; }
        public List<StateGroup> StateGroups { get; set; }
        public List<AcousticTexture> AcousticTextures { get; set; }
        public List<Media> Media { get; set; }
        public List<Event> IncludedEvents { get; set; }
    }

    public class Plugins
    {
        public List<AudioDevice> AudioDevices { get; set; }
    }

    public class AudioDevice
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LibName { get; set; }
        public string LibId { get; set; }
    }

    public class Bus
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class GameParameter
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class StateGroup
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<State> States { get; set; }
    }

    public class State
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class AcousticTexture
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Property> Properties { get; set; }
    }

    public class Property
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }

    public class Media
    {
        public string Id { get; set; }
        public string Language { get; set; }
        public string Streaming { get; set; } // 使用字符串类型接收原始值
        public string Location { get; set; }
        public string ShortName { get; set; }
        public string CachePath { get; set; }
    }

    public class Event
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<MediaRef> MediaRefs { get; set; }
    }

    public class MediaRef
    {
        public string Id { get; set; }
    }
}
