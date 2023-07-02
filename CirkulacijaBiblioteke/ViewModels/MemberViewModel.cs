﻿using System.Windows.Input;
using CirkulacijaBiblioteke.View;

namespace CirkulacijaBiblioteke.ViewModels;

public class MemberViewModel : ViewModelBase

{
    private object _currentView;


    

    public MemberViewModel()
    {
        

        ViewBooksCommand = new DelegateCommand(o => EquipmentView());

        //_currentView = new EquipmentPaneViewModel(_inventoryService);
    }

    public ICommand ViewBooksCommand { get; private set; }



    public object CurrentView
    {
        get => _currentView;
        set
        {
            _currentView = value;
            OnPropertyChanged();
        }
    }

    public void EquipmentView()
    {
       // CurrentView = new EquipmentPaneViewModel(_inventoryService);
    }

    
}