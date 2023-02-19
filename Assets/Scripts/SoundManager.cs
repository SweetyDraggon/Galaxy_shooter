using System;
using System.Collections.Generic;
using UnityEngine;
using zZ17;

public class SoundManager : MonoBehaviour
{
	public AudioClip[] bullets;

	public AudioClip clipButtonClick;

	public AudioClip clipEnemyHit;

	public AudioClip clipEnemyDie;

	public AudioClip clipSoundBgMenu;

	public AudioClip clipSoundBgLevel;

	public AudioClip clipSoundBgGamePlay;

	public AudioClip clipPlayerDie;

	public AudioClip clipBonusBullet;

	public AudioClip clipEnemySpaceDie;

	public static SoundManager Instance;

	private List<AudioSource> SoundEnemies;

	public  AudioSource ClickButton;

	internal AudioSource SoundBulletPlayer;

	internal AudioSource SoundBG;

	internal AudioSource SoundPlayer;

	private void Awake()
	{
		SoundManager.Instance = this;
		this.SoundEnemies = new List<AudioSource>();
		this.ClickButton = base.gameObject.AddComponent<AudioSource>();
		this.ClickButton.playOnAwake = false;
		this.SoundBG = base.gameObject.AddComponent<AudioSource>();
		this.SoundBG.playOnAwake = true;
		//false;
		this.SoundBG.loop = true;
		this.SoundBulletPlayer = base.gameObject.AddComponent<AudioSource>();
		this.SoundBulletPlayer.playOnAwake = false;
		this.SoundBulletPlayer.loop = false;
		this.SoundPlayer = base.gameObject.AddComponent<AudioSource>();
		this.SoundPlayer.playOnAwake = false;
	}

	internal void PlaySoundBg(Scene scene)
	{
		float volume = 1f;
		switch (scene)
		{
		case Scene.HOME:
			volume = 0.6f;
			this.SoundBG.clip = this.clipSoundBgMenu;
			break;
		case Scene.SELECTLEVEL:
			volume = 0.4f;
			this.SoundBG.clip = this.clipSoundBgLevel;
			break;
		case Scene.GAMEPLAY:
			volume = 0.3f;
			this.SoundBG.clip = this.clipSoundBgGamePlay;
			break;
		case Scene.SHOP:
			break;
		default:
			this.SoundBG.clip = null;
			break;
		}
		this.SoundBG.PlayAudio(1f);
	}

	internal void PlayClickButton()
	{
		this.ClickButton.clip = this.clipButtonClick;
		this.ClickButton.Play();
	}

	internal void PlaySoundPlayer(int id)
	{
		if (id != 0)
		{
			if (id != 1)
			{
				this.SoundPlayer.clip = null;
			}
			else
			{
				this.SoundPlayer.clip = this.clipBonusBullet;
			}
		}
		else
		{
			this.SoundPlayer.clip = this.clipPlayerDie;
		}
		this.SoundPlayer.PlayAudio(1f);
	}

	internal void PlaySoundEnemy(int id)
	{
		AudioSource audioSource = null;
		foreach (AudioSource current in this.SoundEnemies)
		{
			if (!current.isPlaying)
			{
				audioSource = current;
				break;
			}
		}
		if (audioSource == null)
		{
			audioSource = base.gameObject.AddComponent<AudioSource>();
			this.SoundEnemies.Add(audioSource);
		}
		switch (id)
		{
		case 0:
			audioSource.clip = this.clipEnemyHit;
			audioSource.PlayAudio(0.1f);
			break;
		case 1:
			audioSource.clip = this.clipEnemyDie;
			audioSource.PlayAudio(0.25f);
			break;
		case 2:
			audioSource.clip = this.clipEnemySpaceDie;
			audioSource.PlayAudio(0.25f);
			break;
		default:
			audioSource.clip = null;
			break;
		}
	}

	internal void PlaySoundBulletPlayer(int id = 0)
	{
		if (this.SoundBulletPlayer.isPlaying && id <= 2)
		{
			return;
		}
		this.SoundBulletPlayer.clip = this.bullets[id];
		this.SoundBulletPlayer.PlayAudio(0.25f);
	}
}
