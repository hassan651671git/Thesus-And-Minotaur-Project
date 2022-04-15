using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TopLayer.UiControllers {
    [Serializable]
    public class Command
    {
        public UserCommand UserCommand;
        public KeyCode KeyCode;
        public Button Button;
        public Action<UserCommand> ExecutionAction;
        public void Execute(UserCommand userCommand)
        {
            this.ExecutionAction?.Invoke(userCommand);
        }
    }
}