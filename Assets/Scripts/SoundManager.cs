using UnityEngine;

public class SoundManager : MonoBehaviour {

	[Header("Audio Clips")]
	[SerializeField] public AudioClip mainMenuSound;
	public AudioClip gamePlaySound,buttonClick,levelFailed,levelComplete;
	
	[Header("Audio Sources")]
	[SerializeField]
	private AudioSource musicSource;
	
	[SerializeField] private AudioSource sfxSource, levelFail_CompleteSource;
	
	[Header("Audio Listener")]
	[SerializeField] private AudioListener CurrentAudioListener;
	
	private void Awake()
	{
		VerifyAudioSources ();
	}

	public void SfxVolume()
	{
		sfxSource.volume = PlayerPrefs.GetFloat ("SFXVol",1);
	}

	public void ChangeVolume()
	{
		AudioListener.volume = PlayerPrefs.GetFloat ("Vol",1);
	}


	private void VerifyAudioSources()
	{
		musicSource.playOnAwake = false;
		musicSource.loop = true;
		sfxSource.playOnAwake = false;
		sfxSource.loop = false;
		levelFail_CompleteSource.playOnAwake = false;
		levelFail_CompleteSource.loop = false;
	}

	public void PlayMainMenuSounds()
	{
		musicSource.clip = mainMenuSound;
		musicSource.Play ();
	}

	public void PlayMainMenuSounds(float temp)
	{
		musicSource.clip = mainMenuSound;
		musicSource.Play ();
		musicSource.volume = temp;
	}

	public void PlayGameplaySounds()
	{
		musicSource.clip = gamePlaySound;
		musicSource.Play ();
	}

	public void PlayGameplaySounds(float temp)
	{
		musicSource.clip = gamePlaySound;
		musicSource.Play ();
		musicSource.volume = temp;
	}

	public void PlayButtonClickSound()
	{
		sfxSource.clip = buttonClick;
		sfxSource.Play ();
	}

	public void PlayLevelFailedSound()
	{
		levelFail_CompleteSource.clip = levelFailed;
		levelFail_CompleteSource.Play ();
	}
	public void PlayLevelCompleteSound()
	{
		levelFail_CompleteSource.clip = levelComplete;
		levelFail_CompleteSource.Play ();
	}
}
