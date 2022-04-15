using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TopLayer.UiControllers {
    public class InputManager: MonoBehaviour
    {
        [SerializeField]
        private Command[]Commands;

        public Text commentText, solutionText,LevelNumper;
        public bool IsPaused { get; private set; }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.anyKeyDown)
            {
                foreach(var command in this.Commands)
                {
                    if (Input.GetKeyDown(command.KeyCode))
                    {
                        this.executeCommand(command);
                        return;
                    }
                }
            }
        }

        public void HandleButtonClick(BaseEventData baseEventData)
        {
            var button = baseEventData.selectedObject.GetComponent<Button>();
            var command = this.Commands.FirstOrDefault(c => c.Button == button);
            this.executeCommand(command);
        }

        private void executeCommand(Command command)
        {
            if (this.IsPaused)
            {
                if (!this.isNonPausableCommand(command.UserCommand))
                {
                    return;
                }
            }

            command.Execute(command.UserCommand);
        }
        public void Pause()
        {
            this.IsPaused = true;
        }
        public void Resume()
        {
            this.IsPaused = false;
        }

        public void SetCommandAction(UserCommand userCommand,Action<UserCommand> executionAction)
        {
            var command = this.Commands.FirstOrDefault(c => c.UserCommand == userCommand);
           
            if (command == null)
            {
                return;
            }

            command.ExecutionAction = executionAction;
        }

        private bool isNonPausableCommand(UserCommand command)
        {
            switch (command)
            {
                case UserCommand.Restart:
                case UserCommand.NextPlevel:
                case UserCommand.PreviousLevel:
                    return true;
                default:
                    return false;
            }

        }
    }
}