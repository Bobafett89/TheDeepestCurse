using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    public Text t, h, r, s;
    private void Awake()
    {
        PlayerPrefs.SetFloat("Sound", PlayerPrefs.GetFloat("Sound", 25));
    }
    public void apply()
    {
        PlayerPrefs.SetFloat("Sound", int.Parse(s.text));
    }
    public void rules()
    {
        SceneManager.LoadScene("Rules");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void PlayOnClassic()
    {
        PlayerPrefs.SetFloat("Time", 10);
        PlayerPrefs.SetFloat("Hp", 50);
        PlayerPrefs.SetFloat("HpRune", 15);
        SceneManager.LoadScene("SampleScene");
    }
    public void PlayOnEasy()
    {
        PlayerPrefs.SetFloat("Time", 10);
        PlayerPrefs.SetFloat("Hp", 100);
        PlayerPrefs.SetFloat("HpRune", 30);
        SceneManager.LoadScene("SampleScene");
    }
    public void PlayOnCustom()
    {
        PlayerPrefs.SetFloat("Time", float.Parse(t.text));
        PlayerPrefs.SetFloat("Hp", float.Parse(h.text));
        PlayerPrefs.SetFloat("HpRune", float.Parse(r.text));
        SceneManager.LoadScene("SampleScene");
    }
    public void exit()
    {
        Application.Quit();
    }
}
