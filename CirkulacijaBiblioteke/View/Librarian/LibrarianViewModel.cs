using System.Windows.Input;

namespace CirkulacijaBiblioteke.View.Librarian;

public class LibrarianViewModel : ViewModelBase
{
    private object _currentView;
    private readonly Core.HospitalSystem.Users.Models.Doctor _doctor;
    private readonly IDoctorService _doctorService;
    private readonly IInventoryService _inventoryService;
    private readonly IMedicalRecordService _medicalRecordService;
    private readonly IPatientService _patientService;
    private readonly IRoomService _roomService;
    private readonly IScheduleService _scheduleService;
    private readonly ISpecialistsRefferalService _specialistsRefferalService;
    private readonly IAnnualLeaveService _annualLeaveService;
    private readonly User _user;

    public LibrarianViewModel(User user)
    {
        _doctorService = Injector.Container.Resolve<IDoctorService>();
        _inventoryService = Injector.Container.Resolve<IInventoryService>();
        _medicalRecordService = Injector.Container.Resolve<IMedicalRecordService>();    
        _patientService = Injector.Container.Resolve<IPatientService>();    
        _roomService = Injector.Container.Resolve<IRoomService>();
        _scheduleService = Injector.Container.Resolve<IScheduleService>();
        _specialistsRefferalService = Injector.Container.Resolve<ISpecialistsRefferalService>();
        _doctor = _doctorService.GetByEmail(user.Email);
        _annualLeaveService = Injector.Container.Resolve<IAnnualLeaveService>();
        
        var appointments = _scheduleService.GetDoctorAppointments(_doctor.Email);
        _user = user;

       

        LoadAppointmentCommand = new DelegateCommand(o => LoadAppointments());
        LoadPatientsCommand = new DelegateCommand(o => LoadPatinets());
        _currentView = new AppointmentShowViewModel(_user, _scheduleService, _doctorService, _patientService,
            _medicalRecordService, _inventoryService, _roomService);
        LoadChatCommand = new DelegateCommand(o => LoadChat());
        AddAnnualLeaveCommand = new DelegateCommand(o => AddAnnualLeave());
        LoadHospitalizedPatients = new DelegateCommand(o => LoadHospitalize());
    }

    public ICommand LoadAppointmentCommand { get; private set; }
    public ICommand LoadPatientsCommand { get; private set; }
    public ICommand LoadChatCommand { get; private set; }
    public ICommand AddAnnualLeaveCommand { get; private set; }
    public ICommand LoadHospitalizedPatients { get; private set; }

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
        CurrentView = new AppointmentShowViewModel(_user, _scheduleService, _doctorService, _patientService,
            _medicalRecordService, _inventoryService, _roomService);
    }

    public void LoadPatinets()
    {
        CurrentView = new PatientTableViewModel(_user, _scheduleService, _doctorService, _patientService,
            _medicalRecordService);
    }

    public void LoadChat()
    {
        CurrentView = new ChatViewModel(_doctor.DiscordToken);
    }

    public void AddAnnualLeave()
    {
        CurrentView = new DoctorAnnualLeaveView() { DataContext =new  DoctorAnnualLeaveViewModel(_annualLeaveService,_scheduleService,_user.Email)};
    }
    public void LoadHospitalize()
    {
        CurrentView = new HospitalizedPatientsView() { DataContext = new HospitalizedPatientsViewModel() };
    }
}