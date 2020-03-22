﻿//This script keeps track of the player's health. It is also used to communicate with the GameManager

using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
	[Header("Health Properties")]
	[SerializeField] int maxHealth = 100;				//Player's maximum health

	[Header("Script References")]
	[SerializeField] PlayerMovement playerMovement;		//Reference to the player's movement script
	[SerializeField] PlayerAttack playerAttack;			//Reference to the player's attack script

	[Header("Components")]
	[SerializeField] Animator animator;					//Reference to the animator component
	
	[Header("UI")]
	[SerializeField] FlashFade damageImage;				//Reference to the FlashFade script on the DamageImage UI element
	[SerializeField] Slider healthSlider;				//The slider that will represent the player's health
		
	[Header("Debugging Properties")]				
	[SerializeField] bool isInvulnerable = false;		//Is the player invulnerable? Useful for debugging so the player won't take damage

	int currentHealth;                                  //The current health of the player

    [Header("FMOD")]
    //[FMODUnity.EventRef]
    public string playerHurtSound;
    public string playerDeathSound;
    public string DeathMusic;
    public MusicControl musicSystem;

    //FMODUnity.StudioEventEmitter emitter;

    //Reset() defines the default values for properties in the inspector
    void Reset ()
	{
		//Grab a reference to the needed components
		animator = GetComponent <Animator> ();

		playerMovement = GetComponent <PlayerMovement> ();
		playerAttack = GetComponent <PlayerAttack> ();
	}

	void Awake()
	{
		//Set the player's health
		currentHealth = maxHealth;
	}

	//This method allows the enemies to assign damage to the player
	public void TakeDamage (int amount)
	{
		//If the player isn't alive, leave
		if (!IsAlive())
			return;

		//If the player is not invulnerable, reduce the current health
		if(!isInvulnerable)
			currentHealth -= amount;

		//If there is a damage image, tell it to flash
		if(damageImage != null)
			damageImage.Flash();

		//If there is a health slider, update its value
		if(healthSlider != null)
			healthSlider.value = currentHealth / (float)maxHealth;

        if(currentHealth <= 50 && IsAlive())
        {
            musicSystem.IsHalfHealth();
        }

		//If the player has been defeated by this attack...
		if (!IsAlive())
		{
			//...if there is a player movement script, tell it to be defeated
			if (playerMovement != null)
				playerMovement.Defeated();

			//...if there is a player attack script, tell it to be defeated
			if (playerAttack != null)
				playerAttack.Defeated();

            //...set the Die parameter in the animator
            animator.SetTrigger ("Die");

            //...if there is an audio source, assign the deathclip to it
            FMODUnity.RuntimeManager.PlayOneShot(playerDeathSound, transform.position);
            FMODUnity.RuntimeManager.PlayOneShot(DeathMusic, transform.position);

            //Play the death music
            musicSystem.IsDeadMusic();

            //...finally, tell the GameManager that the player has been defeated
            GameManager.Instance.PlayerDied();
		}
        //If there is an audio source, tell it to play
        FMODUnity.RuntimeManager.PlayOneShot(playerHurtSound, transform.position);
	}

	public bool IsAlive()
	{
		//If the currentHealth is above 0 return true (the player is alive), otherwise return false
		return currentHealth > 0;
	}

	//This method is called by an event in the Death animation on the player
	void DeathComplete ()
	{
		//If this player is the registered player of the GameManager, tell the GameManager that this player
		//has finished its death animation
		if(GameManager.Instance.Player == this)
			GameManager.Instance.PlayerDeathComplete();
	}
}
