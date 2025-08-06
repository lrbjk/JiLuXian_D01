using Common;
using UnityEngine;

/*

*/
namespace Audio.Sys
{
    /// <summary>
    /// 描述：音频的系统类
    ///提供公用音频相关接口
    ///避免后续各种音效中间件的换用
    /// </summary>
    public abstract class AudioSystem : MonoSingleton<AudioSystem>
    {
        /// <summary>
        /// 播放声音
        /// </summary>
        /// <param name="audioName">声音名称</param>
        /// <param name="targetObject">播放声音的物体</param>
        public abstract void PlayAudio(string audioName, GameObject targetObject);
        /// <summary>
        /// 停止播放指定物体的声音
        /// </summary>
        /// <param name="targetObject"></param>
        public abstract void StopAllAudio(GameObject targetObject);
        /// <summary>
        /// 停止播放所有的声音
        /// </summary>
        /// <param name="targetObject"></param>
        public abstract void StopAllAudio();
        /// <summary>
        /// 设置对应种类音频的音量
        /// </summary>
        /// <param name="audioType"></param>
        /// <param name="value"></param>
        public abstract void SetAudioVolume(AudioType audioType,float value);

    }
}
