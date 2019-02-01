﻿namespace Galagram.ViewModel.Commands.Registration
{
    /// <summary>
    /// Check if name and password is correct
    /// <para/>
    /// If everything is correct, log in a user
    /// </summary>
    public class LogInCommand : CommandBase
    {
        // FIELDS
        ViewModel.RegistrationViewModel registrationViewModel;
        // CONSTRUCTORS
        /// <summary>
        /// Initialize a new instance of <see cref="LogInCommand"/>
        /// </summary>
        /// <param name="registrationViewModel">
        /// An instance of <see cref="ViewModel.RegistrationViewModel"/>
        /// </param>
        public LogInCommand(ViewModel.RegistrationViewModel registrationViewModel)
        {
            this.registrationViewModel = registrationViewModel;
        }

        // METHODS
        /// <summary>
        /// Check if command  can be executed
        /// </summary>
        /// <param name="parameter">
        /// Additionals parameters
        /// </param>
        /// <returns>
        /// True if command can be executed, otherwise — false
        /// </returns>
        public override bool CanExecute(object parameter)
        {
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Can Exetute {nameof(LogInCommand)}");

            return true;
        }
        /// <summary>
        /// Execute the command
        /// </summary>
        /// <param name="parameter">
        /// Command parameter
        /// </param>
        public override void Execute(object parameter)
        {
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Exetute {nameof(LogInCommand)}");

            // checking
            if (!registrationViewModel.IsDataValid()) return;

            // check if name and password is valid
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Check if user nickname and password is in DataBase");

            DataAccess.Structs.ValidNameAndPassword validNamaAndPassword = registrationViewModel.UnitOfWork.UserRepository.IsDataValid(registrationViewModel.Nickname, registrationViewModel.Password);

            if (!validNamaAndPassword.IsNameValid)
            {
                registrationViewModel.WindowManager.ShowMessageWindow(Core.Messages.Info.ViewModel.Command.Registration.NICKNAME_IS_WRONG);
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"User can not log in, because his nickname is wrong");
                return;
            }
            if (!validNamaAndPassword.IsPasswordValid)
            {
                registrationViewModel.WindowManager.ShowMessageWindow(Core.Messages.Info.ViewModel.Command.Registration.PASSWORD_IS_WRONG);
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"User can not log in, because his password is wrong");
                return;
            }

            // gets current user
            DataAccess.Entities.User user = registrationViewModel.UnitOfWork.UserRepository.Get(registrationViewModel.Nickname);
            registrationViewModel.DataStorage.LoggedUser = user;
            registrationViewModel.DataStorage.ShownUser = user;

            // check if want to log in as admin
            bool doLogInAsAdmin = false;
            if (user.IsAdmin)
            {
                doLogInAsAdmin = Services.WindowManager.Instance.ShowMessageWindow(
                    text: Core.Messages.Info.ViewModel.Command.Registration.IS_NEED_LOG_IN_AS_ADMIN, 
                    header: string.Empty, 
                    buttonType: Window.Dialogs.MessageBoxButton.YesNo).Value;
            }

            // log in
            if (doLogInAsAdmin)
            {
                // opens admin window
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "User logged in. Registration window close. Admin window opens.");
                registrationViewModel.WindowManager.SwitchMainWindow(
                    key: nameof(Window.Admin.AdminWindow),
                    viewModel: new ViewModel.Admin.AdminWindowViewModel());
            }
            else
            {
                // opens new window with current user
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "User logged in. Registration window close. Main window opens.");
                registrationViewModel.WindowManager.SwitchMainWindow(
                    key: nameof(Window.User.MainWindow),
                    viewModel: new ViewModel.User.MainWindowViewModel());
            }
            
        }
    }
}
