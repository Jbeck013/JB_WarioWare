using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JB_GameController : MonoBehaviour
{
    public GameObject[] monsters;
    public Text[] actionsList;
    private string[] actions;
    private string[] time;

    public Text feedText;
    public Text timerText;
    public ParticleSystem particles;

    private bool monsterDefeated;
    private int timer;
    private int movement;
    private bool gameOn;
    private int monsterCount;
    private GameObject monster;
    private string attack;
    private int score;

    void Start()
    {
        gameOn = true;
        actions = new string[] { "Fire", "Blizzard", "Thunder", "Bio", "Heavy Swing", "Provoke", "Meditation", "Jump", "Barrage", "Holy", "Aero", "Stone", "Flare", "Esuna", "Energy Drain", "Miasma", "Protect", "Shell", "Mug", "Gravity", "Cure", "Carbuncle"};
        time = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
        timer = 9;
        StartCoroutine(PopulateActions());
        monsterCount = Mathf.RoundToInt(Random.Range(0, monsters.Length));
        monster = monsters[monsterCount];
        Instantiate(monster);
        UpdateFeed();
        movement = 0;
        InvokeRepeating("TimerTime", 1f, 1f);
    } 
    void Update()
    {
        actionsList[movement].color = Color.blue;
        if (Input.GetKeyUp(KeyCode.RightArrow) && movement != 21)
        {
            actionsList[movement].color = Color.white;
            movement += 1;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) && movement != 0)
        {
            actionsList[movement].color = Color.white;
            movement -= 1;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow) && movement >= 5)
        {
            actionsList[movement].color = Color.white;
            movement -= 5;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow) && movement <= 16)
        {
            actionsList[movement].color = Color.white;
            movement += 5;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            attack = actionsList[movement].text;
            if (monsterDefeated == false)
            {
                AttackMonster(attack);
            }
        }
    }
    private void FixedUpdate()
    {

    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        
    }
    void MonsterDeath()
    {
        CancelInvoke();
        AddScore(10);
        particles.Play(true);
        monsterDefeated = true;
        StartCoroutine(NextGame(2));
    }
    void UpdateFeed()
    {
        if (monster == monsters[0]) { feedText.text = "Watch out! A Fire is raging! Use the correct ability to put it out!"; }
        if (monster == monsters[1]) { feedText.text = "Watch out! A Skeleton is shambling towards you! Use the correct ability to defeat it!"; }
        if (monster == monsters[2]) { feedText.text = "Watch out! An Old Man is loitering! Use the correct ability to get rid of him!"; }
        if (monster == monsters[3]) { feedText.text = "Watch out! A Glass Window is... here? Use the correct ability to break it, I guess?"; }
        if (monster == monsters[4]) { feedText.text = "Watch out! A Dragon is attacking! Use the correct ability to slay it!"; }

    }
   
    void TimerTime()
   {
        if (timer >= 0)
        {
            timerText.text = time[timer];
            timer--;
        }
        if(timer == -1)
        {
            GameOver();
        }
    }

    void AttackMonster(string x)
    {
        //Fireball: Blizzard, Stone, Aero
        if(monster == monsters[0]  && (x  == "Blizzard" || x == "Stone" || x == "Aero"))
        {
            feedText.text = "You put out the Fire! +10 Points +25 EXP +10 GP";
            MonsterDeath();
        }
        //Skeleton: Holy Heavy Swing, Cure
        if (monster == monsters[1] && (x == "Holy" || x == "Heavy Swing" || x == "Cure"))
        {
            feedText.text = "You destroyed the Skeleton! +10 Points +25 EXP +10 GP";
            MonsterDeath();
        }
        //Old Man: Mug, Provoke, Miasma
        if (monster == monsters[2] && (x == "Mug" || x == "Provoke" || x == "Miasma"))
        {
            feedText.text = "You beat up the Old Man! You monster! +10 Points +25 EXP +10 GP";
            MonsterDeath();
        }
        //Glass Window: Heavy Swing, Barrage, Stone
        if (monster == monsters[3] && (x == "Heavy Swing" || x == "Barrage" || x == "Stone"))
        {
            feedText.text = "You broke the Window! Congratulations?? +10 Points +25 EXP +10 GP";
            MonsterDeath();
        }
        //Dragon: Jump, Thunder, Gravity
        if (monster == monsters[4] && (x == "Jump" || x == "Thunder" || x == "Gravity"))
        {
            feedText.text = "You slayed the Dragon! +10 Points +25 EXP +10 GP";
            MonsterDeath();

        }
    }

    IEnumerator PopulateActions()
    {
 
        for (int i = 0; i <= 22; i++ )
        {
            int x = Mathf.RoundToInt(Random.Range(0, actions.Length));
            if (actionsList[x].text == "")
            {
                actionsList[x].text = actions[i];
            }
            else { i--; }
            yield return null;
        }
    }

    public void GameOver()
    {
        feedText.text = "Oh no! You lose! Game Over!";
        StartCoroutine(NextGame(1));
    }
    IEnumerator NextGame(int time)
    {
        yield return new WaitForSeconds(time);
        gameOn = false;
    }
}