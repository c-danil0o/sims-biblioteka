using System.Windows.Input;
using CirkulacijaBiblioteke.View;

namespace CirkulacijaBiblioteke.ViewModels;

public class LibrarianViewModel : ViewModelBase
{
    private object _currentView;

    public LibrarianViewModel()
    {
      
        
     

       
        //_currentView = 

    }

    public ICommand LoadAppointmentCommand { get; private set; }
    

    public object CurrentView
    {
        get => _currentView;
        set
        {
            _currentView = value;
            OnPropertyChanged();
        }
    }

    public void LoadAppointments()
    {
        /*CurrentView = new AppointmentShowViewModel(_user, _scheduleService, _doctorService, _patientService,
            _medicalRecordService, _inventoryService, _roomService);*/
    }

   
}