using ITI.DesignPatterns.Foundation.Runtime.MVVM;
using TMPro;
using UnityEngine;

namespace ITI.DesignPatterns.CustomPackage.Runtime.New_Folder
{
    public sealed class NameSetterView : ViewBase<NameSetterViewModel>
    {
        [SerializeField]
        private TMP_InputField _nameInputField;

        protected override void Bind()
        {
            base.Bind();

            _nameInputField.onValueChanged.AddListener(newName => ViewModel.UpdateName(_nameInputField.text));
        }
    }
}