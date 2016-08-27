using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class HumanPanel : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		action = transform.FindChild ("Action");
		CheckingPanel = action.FindChild ("CheckingAction");
		CallingPanel = action.FindChild ("CallingAction");
		slider = action.FindChild ("3").FindChild ("Slider").GetComponent<Slider> ();
		raiseAmount = action.FindChild ("3").FindChild ("raiseAmount").GetComponent<Text> ();
		UpdateControlling ();
	}

	public void UpdateControlling ()
	{
		if (GameObject.FindWithTag ("GameController") != null)
			human = GameObject.FindWithTag ("GameController").GetComponent<PlayerManager> ().playerImplementation;
	}

	public void fold ()
	{
		playerAction = new PlayerAction ();
		playerAction.set (PlayerAction.Action.Fold);
		human.setAction (playerAction);
	}

	public void call ()
	{
		playerAction = new PlayerAction ();
		playerAction.set (PlayerAction.Action.Call);
		human.setAction (playerAction);
	}

	public void check ()
	{
		playerAction = new PlayerAction ();
		playerAction.set (PlayerAction.Action.Check);
		human.setAction (playerAction);
	}

	public void raise ()
	{
		playerAction = new PlayerAction ();
		PlayerAction.Raise raise = new PlayerAction.Raise (int.Parse (raiseAmount.text.ToString ()));
		playerAction.raise (raise);
		human.setAction (playerAction);
	}


	public void UpdateRaiseAmount ()
	{
		int raise = Mathf.CeilToInt (slider.value * human.Chips);
		raiseAmount.text = raise + "";

	}

	public void disappear (bool status)
	{
		action.gameObject.SetActive (!status);
		if (status)
			turnOffPanels ();
	}

	public void showPanel (Context.CurrentAction currentAction)
	{
		this.currentAction = currentAction;
		slider.value = 0;
		raiseAmount.text = 0 + "";
		if (currentAction.Equals (Context.CurrentAction.checking))
			CheckingPanel.gameObject.SetActive (true);
		else
			CallingPanel.gameObject.SetActive (true);
	}

	private void turnOffPanels ()
	{
		if (currentAction.Equals (Context.CurrentAction.checking))
			CheckingPanel.gameObject.SetActive (false);
		else
			CallingPanel.gameObject.SetActive (false);
	}

	Context.CurrentAction currentAction;
	PlayerAction playerAction;
	Transform CheckingPanel;
	Transform CallingPanel;
	Transform action;
	Slider slider;
	Text raiseAmount;
	PlayerImplementation human;
}
