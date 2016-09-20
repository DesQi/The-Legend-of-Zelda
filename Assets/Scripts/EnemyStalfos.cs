using UnityEngine;
using System.Collections;

public class EnemyStalfos : Enemy {
	public float changeDirectionChance = 0.005f;
	public Sprite[] stalfos_sprites;

	StateMachine animation_state_machine;

	// Use this for initialization
	void Start () {
		walking_velocity = 2.0f;
		knockback_velocity = 8.0f;
		enemy_health = 3;
		animation_state_machine = new StateMachine ();
		animation_state_machine.ChangeState(new StalfosStateAnimation (this, GetComponent<SpriteRenderer> (), stalfos_sprites, 6));
		GridBasedMove ();
		ChooseDirectionMove ();
	}

	// Update is called once per frame
	void Update () {
		if (damaged_times > 0) {
			KnockBackMove ();
			damaged_times--;
		} else if (damaged_times == 0) {
			knockback_direction = Direction.OTHER;
			current_state = EntityState.NORMAL;
			damaged_times--;
		} 
	}


	void FixedUpdate() {
		animation_state_machine.Update ();
		if (UnityEngine.Random.value < changeDirectionChance) {
			GridBasedMove ();
			ChooseDirectionMove ();
		}
	}
}

public class StalfosStateAnimation : State
{
	EnemyStalfos em;
	SpriteRenderer renderer;
	Sprite[] animation;
	int animation_length;
	float animation_progression;
	float animation_start_time;
	int fps;

	public StalfosStateAnimation(EnemyStalfos em, SpriteRenderer renderer, Sprite[] animation, int fps)
	{
		this.em = em;
		this.renderer = renderer;
		this.animation = animation;
		this.animation_length = animation.Length;
		this.fps = fps;

		if(this.animation_length <= 0)
			Debug.LogError("Empty animation submitted to state machine!");
	}

	public override void OnStart()
	{
		animation_start_time = Time.time;
	}


	public override void OnUpdate(float time_delta_fraction)
	{
		// Modulus is necessary so we don't overshoot the length of the animation.
		int current_frame_index = ((int)((Time.time - animation_start_time) / (1.0 / 60)) % animation_length);
		Debug.Log ((Time.time - animation_start_time) / (1.0 / 60));

		renderer.sprite = animation[current_frame_index];

		state_machine.ChangeState(new StalfosStateAnimation(em, renderer, em.stalfos_sprites, 6));
	}
}