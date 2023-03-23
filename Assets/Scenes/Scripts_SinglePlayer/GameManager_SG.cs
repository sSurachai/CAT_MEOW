using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager_SG : MonoBehaviour
{
    public Cat_SG cat;
    public Transform foods;
    public Text Score_txt;
    public bool energyEaten;
    public GameObject winScene;
    public GameObject loseScene;
    public GameObject Joystick;
    public Text Score_win;
    public Text Score_lose;

    public int score;

    public Dog_SG[] dogs;
    private int dogsList_count = 0;
    private List<Vector3> dogs_position = new List<Vector3>() { 
        new Vector3(-12.5f, 14.42f, -5f), 
        new Vector3(10.9f, 14.42f, -5f), 
        new Vector3(-12.4f, -14.46f, -5f), 
        new Vector3(10.65f, -14.42f, -5f),
        new Vector3(0f, 14.42f, -5f), 
        new Vector3(0f, -14.46f, -5f), 
        new Vector3(10.65f, 0f, -5f)};

    public int lives { get; private set; }
    private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        if (this.lives <= 0 && Input.anyKeyDown)
        {
            NewGame();
        }
    }

    private void NewGame()
    {
        score = 0;
        SetScore(score);
        SetLives(3);
        NewRound();
    }

    private void NewRound()
    {
        foreach (Transform foods in this.foods)
        {
            foods.gameObject.SetActive(true);
        }
        for (int i = 0; i < this.dogs.Length; i++)
        {
            if (dogs[i].gameObject.name == "Dog_Red_SG")
            {
                dogs[i].transform.position = dogs_position[i];
                dogs[i].gameObject.SetActive(true);
                this.dogsList_count += 1;
            }
            else if (dogs[i].gameObject.name == "Dog_Green_SG")
            {
                dogs[i].transform.position = dogs_position[i];
                dogs[i].gameObject.SetActive(true);
                this.dogsList_count += 1;
            }
            else if (dogs[i].gameObject.name == "Dog_Blue_SG")
            {
                dogs[i].transform.position = dogs_position[i];
                dogs[i].gameObject.SetActive(true);
                this.dogsList_count += 1;
            }
            else if (dogs[i].gameObject.name == "Dog_Yellow_SG")
            {
                dogs[i].transform.position = dogs_position[i];
                dogs[i].gameObject.SetActive(true);
                this.dogsList_count += 1;
            }
        }

        RespawnCat();
    }

    private void GameOver()
    {
        for (int i = 0; i < this.dogs.Length; i++)
        {
            this.dogs[i].gameObject.SetActive(false);
        }
        this.cat.gameObject.SetActive(false);
    }

    private void SetScore(int score)
    {
        Score_txt.text = "Score : " + score;
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
    }

    public void DogEaten(Dog_SG dog)
    {
        SoundManager_SG.PlaySound("DogDied");
        dog.gameObject.SetActive(false);
        score += dog.points;
        SetScore(score);
        StartCoroutine(RespawnDog(dog));
    }

    public void CatEaten()
    {
        SoundManager_SG.PlaySound("CatDied");
        this.cat.gameObject.SetActive(false);
        SetLives(this.lives - 1);

        if (this.lives > 0)
        {
            Invoke("RespawnCat", 2.0f);
        }
        else
        {
            Case("lose");
        }
    }

    public void NormalFoodEaten(NormalFood_SG normal_food)
    {
        normal_food.gameObject.SetActive(false);
        score += normal_food.points;
        SetScore(score);
        if (CheckingAllFoods())
        {
            SoundManager_SG.PlaySound("Victory");
            this.cat.gameObject.SetActive(false);
            Case("win");
        }
    }

    public void CatEnergyEaten(CatEnergy_SG cat_energy)
    {
        SoundManager_SG.PlaySound("CatEnergyEaten");
        NormalFoodEaten(cat_energy);
        this.energyEaten = true;
        Invoke("ResetEnergy", cat_energy.duration);
    }

    private bool CheckingAllFoods()
    {
        foreach (Transform foods in this.foods)
        {
            if (foods.gameObject.activeSelf)
            {
                return false;
            }
        }
        return true;
    }

    private void ResetEnergy()
    {
        energyEaten = false;
    }

    private IEnumerator RespawnDog(Dog_SG dog)
    {
        yield return new WaitForSeconds(5);
        if (dog.gameObject.name == "Dog_Red_SG")
        {
            dog.transform.position = dogs_position[0];
        }
        else if (dog.gameObject.name == "Dog_Green_SG")
        {
            dog.transform.position = dogs_position[1];
        }
        else if (dog.gameObject.name == "Dog_Blue_SG")
        {
            dog.transform.position = dogs_position[2];
        }
        else if (dog.gameObject.name == "Dog_Yellow_SG")
        {
            dog.transform.position = dogs_position[3];
        }
        else if (dog.gameObject.name == "Dog_Red_NEW")
        {
            dog.transform.position = dogs_position[4];
        }
        else if (dog.gameObject.name == "Dog_Green_NEW")
        {
            dog.transform.position = dogs_position[5];
        }
        else if (dog.gameObject.name == "Dog_Yellow_NEW")
        {
            dog.transform.position = dogs_position[6];
        }
        dog.gameObject.SetActive(true);
    }

    public void RespawnCat()
    {
        this.cat.transform.position = new Vector3(-0.37f, -0.72f, -5f);
        this.cat.gameObject.SetActive(true);
    }

    private void Case(string casename)
    {
        Time.timeScale = 0f;
        Joystick.SetActive(false);
        if (casename == "win")
        {
            Debug.Log("win");
            winScene.SetActive(true);
            Score_win.text = "Score : " + score;
        }
        else if (casename == "lose")
        {
            Debug.Log("Lose");
            loseScene.SetActive(true);
            Score_lose.text = "Score : " + score;
        }
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
        Joystick.SetActive(true);
        Time.timeScale = 1f;
    }

    public void BacktoMenu()
    {
        SceneManager.LoadScene(0);
        Joystick.SetActive(true);
        Time.timeScale = 1f;
    }

    public void OnigiriEaten(Onigiri_SG onigiri)
    {
        onigiri.gameObject.SetActive(false);
        IncreaseDog();
    }

    public void FoodCanEaten(FoodCan_SG foodcan)
    {
        foodcan.gameObject.SetActive(false);
        DecreaseDog();
    }

    public void DangoEaten(Dango_SG dango)
    {
        dango.gameObject.SetActive(false);
        for (int i = 0; i < this.dogs.Length; i++)
        {
            dogs[i].movement.speed = 0;
        }
        Invoke("SetDogSpeed", dango.duration);
    }

    public void IncreaseDog()
    {
        dogs[this.dogsList_count].transform.position = dogs_position[this.dogsList_count];
        dogs[this.dogsList_count].gameObject.SetActive(true);
        this.dogsList_count += 1;
    }

    public void DecreaseDog()
    {
        this.dogs[dogsList_count - 1].gameObject.SetActive(false);
            this.dogsList_count -= 1;
    }

    public void SetDogSpeed()
    {
        for (int i = 0; i < this.dogs.Length; i++)
        {
            dogs[i].movement.speed = 4;
        }
    }


}

