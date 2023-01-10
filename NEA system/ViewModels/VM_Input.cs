﻿namespace NEA_system.ViewModels;

internal abstract class VM_Input : VM_Base
{
    protected string errorMessage;
    public string ErrorMessage
    {
        get { return errorMessage; }
        protected set
        {
            errorMessage = value;
            OnPropertyChanged(nameof(ErrorMessage));
        }
    }
}