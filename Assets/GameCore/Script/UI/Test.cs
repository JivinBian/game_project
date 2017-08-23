using GameCore.Script.Common.Interactive;
using GameCore.Script.Common.State.StateStruct;
using GameCore.Script.Managers.Object;
using GameCore.Script.SceneObject;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{

	private Button _btn;

	private Player _player;

	public Button Idle;
	// Use this for initialization
	void Start ()
	{
		_btn=GetComponent<Button>();
		_btn.onClick.AddListener(OnClick);
		_player=ObjectManager.GetInstance().GetPlayer(1);
		Idle.onClick.AddListener(OnClickIdle);
		GameObject pGo=GameObject.Find("Plane");
		new CollideMouseController(pGo.transform).ClickEvent+= (t,p) => { _player.MoveTo(p); };
	}

	private void OnClickIdle()
	{
		//_player.ChangeState(ObjectState.Idle);
		_player.StopMove();
	}
	private void OnClick()
	{
		//_player.ChangeState(ObjectState.Run);
		_player.MoveTo(new Vector3(10, 0f, 10));
		bool canMoveTo = true;
		_player.ReachToTargetEvent += (started) =>
		{
			if (started&&canMoveTo)
			{
				canMoveTo=_player.MoveTo(new Vector3(20,0.5f,20));
			}
			
		};
	}
}
