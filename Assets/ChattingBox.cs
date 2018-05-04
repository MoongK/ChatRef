using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ChattingBox : NetworkBehaviour {

    Text messageBox;
    InputField inputField;
    string nickName;

	void Start () {
        messageBox = GameObject.Find("ChattingBox/Text").GetComponent<Text>();
        inputField = GameObject.Find("ChattingBox/InputField").GetComponent<InputField>();

        inputField.onEndEdit.AddListener(str =>
        {
            if (!isLocalPlayer)
                return;

            if(str != string.Empty)
            {
                CmdSendChat(nickName + ": " + str);
                inputField.text = string.Empty;
            }
            inputField.ActivateInputField();
        });
    }

    public override void OnStartLocalPlayer()
    {
        nickName = "Player " + netId;
    }

    [Command]
    void CmdSendChat(string message)
    {
        RpcReceviedChat(message);
    }

    [ClientRpc]
    void RpcReceviedChat(string message)
    {
        messageBox.text += message + "\n";
    }
}
