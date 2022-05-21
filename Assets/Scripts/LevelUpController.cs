using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpController : MonoBehaviour {

    public ParticleSystem paritcleSystem;
    public GameObject bonusLevel;
    public GameObject giftPrize;

    Vector3 target;
    bool isActive = false;

    Text levelUpText;

	// Use this for initialization
	void Start () {
        levelUpText = GetComponent<Text>();
        target = new Vector3(0, 2.5f, 0);
	}
	
	// Update is called once per frame
	void LateUpdate () {
        if(isActive)
        {
            ParticleSystem.Particle[] particles = new ParticleSystem.Particle[paritcleSystem.particleCount];
            paritcleSystem.GetParticles(particles);

            for (int i = 0; i < particles.Length; i++)
            {
                particles[i].velocity = new Vector3(0, 0, 0);
                particles[i].position = Vector3.MoveTowards(particles[i].position, target, 0.5f);
                if(particles[i].position == target)
                {
                    GameObject.Find("Game controller").GetComponent<GameController>().AddCoins(true);
                    particles[i].position = new Vector3(150, 150, 150);
                }
            }


            paritcleSystem.SetParticles(particles, particles.Length);
        }
	}

    public IEnumerator LevelUp()
    {
        yield return new WaitForSeconds(1f);

        if (((PlayerPrefs.GetInt("Level") - 1))%5==0)
        {
            SoundManager.PlaySound("Box collect");
            levelUpText.text = "";
            giftPrize.SetActive(true);
            PlayerPrefs.SetInt("Gifts", PlayerPrefs.GetInt("Gifts") + 1);
        }
        else
        {
            levelUpText.text = "+ " + ((PlayerPrefs.GetInt("Level") - 1) * 5).ToString();
            paritcleSystem.Emit((PlayerPrefs.GetInt("Level") - 1) * 5);
        }

        yield return new WaitForSeconds(1f);

        if (((PlayerPrefs.GetInt("Level"))) % 5 == 0)
        {
            SoundManager.PlaySound("Bonus level");
            bonusLevel.SetActive(true);
        }

        isActive = true;
        levelUpText.text = "Level Up!";

        yield return new WaitForSeconds(2f);
        bonusLevel.SetActive(false);
        isActive = false;
        giftPrize.SetActive(false);
        this.gameObject.SetActive(false);
    }


}
