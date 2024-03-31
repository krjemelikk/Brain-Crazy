using UnityEngine;

public class PlayMusic : MonoBehaviour {

	public AudioClip musicClip;

	private void Start()
	{
		MusicManager.Instance.PlayMusic(musicClip);
	}

}
