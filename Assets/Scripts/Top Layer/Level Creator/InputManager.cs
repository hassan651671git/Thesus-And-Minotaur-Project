using CoreLayer.Enums;
using CoreLayer.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace TopLayer.LevelCreator {
    public class InputManager : MonoBehaviour
    {
        public InputField RowsInputField, ColumnsInputField, CommentInputField, LevelInputfield;
        [SerializeField]
        private Button generateBtn, saveBtn, solveBtn;
        [SerializeField]
        private Toggle leftBlock, rightBlock, upBlock, bottomBlock, isFinal, isThesus, isMioatur;
        [SerializeField]
         public Text solvingCommandsText;
        public Action GenerateBtnAction, SolveBtnAction, SaveBtnAction;

        public Action<bool> LeftBlockValueChanged, RightBlockValueChanged, UpBlockValueChanged, BottomBlockValueChanged, IsFinalValueChanged,
            IsThesusLocationValueChanged, IsMinotaurValueChanged;

        private void Awake()
        {
            this.disableUI();
        }
        public void BtnClicked(BaseEventData baseEventData)
        {
            if (baseEventData.selectedObject.GetComponent<Button>() == saveBtn)
            {
                this.SaveBtnAction.Invoke();
                return;
            }

            if (baseEventData.selectedObject.GetComponent<Button>() == solveBtn)
            {
                this.SolveBtnAction.Invoke();
                return;
            }

            if (baseEventData.selectedObject.GetComponent<Button>() == generateBtn)
            {
                this.GenerateBtnAction.Invoke();
                return;
            }
        }

        public void LeftBlockToggleHandler(bool isOn)
        {
            this.LeftBlockValueChanged.Invoke(isOn);
        }

        public void RightBlockToggleHandler(bool isOn)
        {
            this.RightBlockValueChanged.Invoke(isOn);
        }

        public void TopBlockToggleHandler(bool isOn)
        {
            this.UpBlockValueChanged.Invoke(isOn);
        }

        public void BottomBlockToggleHandler(bool isOn)
        {
            this.BottomBlockValueChanged.Invoke(isOn);
        }

        public void IsFinalToggleHandler(bool isOn)
        {
            this.IsFinalValueChanged.Invoke(isOn);
        }

        public void IsThesusToggleHandler(bool isOn)
        {
            this.IsThesusLocationValueChanged.Invoke(isOn);
        }

        public void IsMinatourToggleHandler(bool isOn)
        {
            this.IsMinotaurValueChanged.Invoke(isOn);
        }

        public void SetSelectedCell(ICell cell)
        {
            if (cell == null)
            {
                this.disableUI();
                return;
            }

            this.enableUI();
            this.leftBlock.isOn = cell.IsLeftBlocked;
            this.rightBlock.isOn = cell.IsRightBlocked;
            this.bottomBlock.isOn = cell.IsBottomBlocked;
            this.upBlock.isOn = cell.IsTopBlocked;
            this.isFinal.isOn = cell.IsFinal;
            this.isMioatur.isOn = cell.CurrentPlayer==PlayerType.Minatour;
            this.isThesus.isOn = cell.CurrentPlayer==PlayerType.Thesus;
        }

        private void disableUI()
        {
            return;
            this.isFinal.interactable = false;
            this.isThesus.interactable = false;
            this.isMioatur.interactable = false;
            this.leftBlock.interactable = false;
            this.rightBlock.interactable = false;
            this.upBlock.interactable = false;
            this.bottomBlock.interactable = false;
            this.saveBtn.interactable = false;
            this.solveBtn.interactable = false;
        }

        private void enableUI()
        {
            this.isFinal.interactable = true;
            this.isThesus.interactable = true;
            this.isMioatur.interactable = true;
            this.leftBlock.interactable = true;
            this.rightBlock.interactable = true;
            this.upBlock.interactable = true;
            this.bottomBlock.interactable = true;
            this.saveBtn.interactable = true;
            this.solveBtn.interactable = true;
        }
    
    

    }
}