﻿using System.Windows.Input;
using System.ComponentModel;

namespace ViewModel.ViewModel.User
{
    public class AskQuestionViewModel : INotifyPropertyChanged
    {
        // EVENT
        public event PropertyChangedEventHandler PropertyChanged;
        // CONSTRUCTORS
        public AskQuestionViewModel(Galagram.Services.WindowManager windowManager, DataAccess.Context.UnitOfWork unitOfWork, DataAccess.Entities.User user)
        {
            throw new System.NotImplementedException();
        }
        // PROPERTIES
        public int SelectedSubjectIndex
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }
        public string[] Subjects
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }
        public string Message
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }
        // COMMAND
        public ICommand AskCommand
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }
        // METHODS
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
    }
}