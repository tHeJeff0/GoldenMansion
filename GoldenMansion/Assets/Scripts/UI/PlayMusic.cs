using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    public AudioClip[] musicList; // 音乐列表
    private AudioSource audioSource;

    private void Start()
    {
        // 获取 AudioSource 组件
        audioSource = GetComponent<AudioSource>();

        // 检查是否有音乐资源
        if (musicList.Length == 0)
        {
            Debug.LogWarning("音乐列表为空，请添加音频资源！");
            return;
        }

        // 开始随机播放音乐
        PlayRandomMusic();
    }

    private void PlayRandomMusic()
    {
        // 从列表中随机选择一个音乐
        AudioClip selectedClip = musicList[Random.Range(0, musicList.Length)];

        // 设置当前音乐
        audioSource.clip = selectedClip;

        // 播放音乐
        audioSource.Play();

        // 调用协程，等待音乐播放结束后播放下一首
        StartCoroutine(PlayNextWhenFinished());
    }

    private System.Collections.IEnumerator PlayNextWhenFinished()
    {
        // 等待当前音乐播放完成
        yield return new WaitForSeconds(audioSource.clip.length);

        // 播放下一首随机音乐
        PlayRandomMusic();
    }
}
