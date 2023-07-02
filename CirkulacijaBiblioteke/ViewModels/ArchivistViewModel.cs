using System.Windows.Input;
using CirkulacijaBiblioteke.View;

namespace CirkulacijaBiblioteke.ViewModels;

public class ArchivistViewModel : ViewModelBase
{
    private object _currentView;


    public ArchivistViewModel()
    {
        


        //_currentView = new PatientReceptionViewModel(_patientService, _scheduleService);
    }

    public ICommand NewPatientReceptionCommand { get; private set; }




    public object CurrentView
    {
        get => _currentView;
        set
        {
            _currentView = value;
            OnPropertyChanged();
        }
    }

    public void NewPatientReception()
    {
        //CurrentView = new PatientReceptionViewModel(_patientService, _scheduleService);
    }

   
}