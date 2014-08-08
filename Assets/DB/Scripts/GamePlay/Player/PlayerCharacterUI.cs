/// <summary>
/// Player character UI.
/// Basic Player UI will show up any information of Player Character
/// Such as Inventory System , Skill System , etc...
/// </summary>
using UnityEngine;
using System.Collections;

public class PlayerCharacterUI : ItemUI
{
	public bool Active = true;
	public GUISkin skin;
	[HideInInspector]
	public CharacterSystem character;
	[HideInInspector]
	public CharacterSkillManager characterSkill;
	[HideInInspector]
	public PlayerQuestManager playerQuestManage;
	private Vector2 scrollPosition;
	private bool showItem, showStatus, showMouse;
	[HideInInspector]
	public StatusRenderer statueRenderer;
	[HideInInspector]
	public MainUIRenderer mainUIRenderer;
	[HideInInspector]
	public ItemRenderer itemRenderer;
	[HideInInspector]
	public QuestRenderer questRenderer;
	[HideInInspector]
	public ShotcutRenderer shotcutRenderer;
	[HideInInspector]
	public SkillRenderer skillRenderer;

	void Awake ()
	{
		statueRenderer = new StatusRenderer ();
		statueRenderer.Inz (new Vector2 (20, 20), this);
		
		mainUIRenderer = new MainUIRenderer ();
		mainUIRenderer.Inz (new Vector2 (20, Screen.height - 150), this);
		
		itemRenderer = new ItemRenderer ();
		itemRenderer.Inz (new Vector2 (Screen.width - 320, 20), this);
		
		questRenderer = new QuestRenderer ();
		questRenderer.Inz (new Vector2 (Screen.width - 320, 20), this);
		
		shotcutRenderer = new ShotcutRenderer ();
		shotcutRenderer.Inz (new Vector2 (450, Screen.height - 120), this);
		
		skillRenderer = new SkillRenderer ();
		skillRenderer.Inz (new Vector2 (Screen.width - 320, 20), this);
	}

	void Start ()
	{	
		base.SettingItemUI ();
		if (this.gameObject.GetComponent<CharacterSystem> ()) {
			character = this.gameObject.GetComponent<CharacterSystem> ();
		}
		if (this.gameObject.GetComponent<CharacterSkillManager> ()) {
			characterSkill = this.gameObject.GetComponent<CharacterSkillManager> ();	
		}
		if (this.gameObject.GetComponent<PlayerQuestManager> ()) {
			playerQuestManage = this.gameObject.GetComponent<PlayerQuestManager> ();	
		}
	}

	void Update ()
	{
	
		statueRenderer.Update ();
		mainUIRenderer.Update ();
		mainUIRenderer.Position = new Vector2 (20, Screen.height - 150);
		itemRenderer.Update ();
		questRenderer.Update ();
		shotcutRenderer.Update ();
		skillRenderer.Update ();
	}
	
	void OnGUI ()
	{
		if (!Active || character == null)
			return;
		if (skin)
			GUI.skin = skin;
		
		GUI.depth = 0;
		if (character.characterStatus) {
			mainUIRenderer.Draw (character.characterStatus);
			statueRenderer.Draw (character.characterStatus);
			itemRenderer.Draw (character.characterInventory.ItemSlots, character.characterInventory, ItemRenderMode.Player, "Inventory   " + character.characterInventory.Money + "$");
			shotcutRenderer.Draw ();
			skillRenderer.Draw (characterSkill);
			if (playerQuestManage) {
				questRenderer.Draw (playerQuestManage.Quests, "Quest", QuestRenderMode.Active);
			}
		}
		
		if (!Screen.lockCursor) {
			if (GUI.Button (new Rect (Screen.width - 130, 30, 100, 30), "Hide Cursor")) {
				Screen.lockCursor = true;
			}
			if (GUI.Button (new Rect (20, 20, 50, 50), "I I")) {
				GameManager gameManae = (GameManager)GameObject.FindObjectOfType(typeof(GameManager));
				if(gameManae){
					gameManae.IsPlaying = false;
					gameManae.Mode = 0;
				}
			}
		} else {
			GUI.skin.label.fontSize = 17;
			GUI.skin.label.normal.textColor = Color.white;
			GUI.skin.label.alignment = TextAnchor.UpperRight;
			GUI.Label (new Rect (Screen.width - 330, 30, 300, 30), "Press 'Tab' Show Cursor");		
		}
		
	}
}
