using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NEA_system.ViewModels;

internal abstract class VM_Base : INotifyPropertyChanged
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



    #region INotifyPropertyChanged
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        var changed = PropertyChanged;
        if (changed == null)
            return;

        changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion
}