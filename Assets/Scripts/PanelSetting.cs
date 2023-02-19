using System;
using UnityEngine;
using UnityEngine.UI;
using zZ17;

public class PanelSetting : MonoBehaviour
{
	public Image checkSound;

	public Image checkMusic;

	private void OnEnable()
	{
		this.checkSound.sprite = GameManager.Instance.checks[GameManager.Instance.sound.GetHashCode()];
		this.checkMusic.sprite = GameManager.Instance.checks[GameManager.Instance.music.GetHashCode()];
	}

	public void ClickSound(Button btn)
	{
		GameManager.Instance.ChangeSound(btn.transform.GetComponent<Image>());
	}

	public void ClickMusic(Button btn)
	{
		GameManager.Instance.ChangeMusic(btn.transform.GetComponent<Image>());
	}

	public void ClickExit(Button btn)
	{
		btn.Click(delegate
		{
			this.Hide();
		});
	}
}
