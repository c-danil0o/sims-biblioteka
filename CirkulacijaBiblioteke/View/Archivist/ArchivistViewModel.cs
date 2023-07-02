using System.Windows.Input;

namespace CirkulacijaBiblioteke.View.Archivist;

public class ArchivistViewModel : ViewModelBase
{
    private readonly Core.HospitalSystem.Users.Models.Nurse _nurse;
    private object _currentView;
    private readonly IDoctorService _doctorService;
    private readonly IMedicalRecordService _medicalRecordService;
    private readonly IPatientService _patientService;
    private readonly IScheduleService _scheduleService;
    private readonly INurseService _nurseService;


    public ArchivistViewModel(User user)
    {
        _patientService = Injector.Container.Resolve<IPatientService>();
        _medicalRecordService = Injector.Container.Resolve<IMedicalRecordService>();
        _scheduleService = Injector.Container.Resolve<IScheduleService>();
        _doctorService = Injector.Container.Resolve<IDoctorService>();
        _nurseService = Injector.Container.Resolve<INurseService>();
        _nurse = _nurseService.GetByEmail(user.Email);

        NewPatientReceptionCommand = new DelegateCommand(o => NewPatientReception());
        UrgentAppointmentReservationCommand = new DelegateCommand(o => UrgentAppointmentReservation());
        LoadChatCommand = new DelegateCommand(o => LoadChat());
        _currentView = new PatientReceptionViewModel(_patientService, _scheduleService);
    }

    public ICommand NewPatientReceptionCommand { get; private set; }
    public ICommand UrgentAppointmentReservationCommand { get; private set; }
    public ICommand LoadChatCommand { get; private set; }



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
        CurrentView = new PatientReceptionViewModel(_patientService, _scheduleService);
    }

    public void UrgentAppointmentReservation()
    {
        CurrentView = new UrgentAppointmentViewModel(_medicalRecordService, _scheduleService, _doctorService);
    }
    public void LoadChat()
    {
        CurrentView = new ChatViewModel(_nurse.DiscordToken);

    }
}