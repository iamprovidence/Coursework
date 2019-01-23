﻿namespace Galagram.ViewModel.Commands.User.MainWindow
{
    /// <summary>
    /// Follow an user
    /// </summary>
    public class FollowCommand : CommandBase
    {
        // FIELDS
        ViewModel.User.MainWindowViewModel mainWindowViewModel;

        // CONSTRUCTORS
        /// <summary>
        /// Initialize a new instance of <see cref="FollowCommand"/>
        /// </summary>
        /// <param name="mainWindowViewModel">
        /// An instance of <see cref="ViewModel.User.MainWindowViewModel"/>
        /// </param>
        public FollowCommand(ViewModel.User.MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;
        }

        // METHODS
        /// <summary>
        /// Check if command can be executed
        /// </summary>
        /// <param name="parameter">
        /// Additionals parameters
        /// </param>
        /// <returns>
        /// True if command can be executed, otherwise — false
        /// </returns>
        public override bool CanExecute(object parameter)
        {
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Can execute {nameof(FollowCommand)}");
            return true;
        }
        /// <summary>
        /// Execute command
        /// </summary>
        /// <param name="parameter">
        /// Command parameters
        /// </param>
        public override void Execute(object parameter)
        {
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Execute {nameof(FollowCommand)}");

            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Gets to shwon user followers current user");

            bool isFollowing = mainWindowViewModel.IsFollowing;
            if (isFollowing)// unfollow
            {
                mainWindowViewModel.User.Followers.Remove(mainWindowViewModel.DataStorage.LoggedUser);
            }
            else // follow
            {
                mainWindowViewModel.User.Followers.Add(mainWindowViewModel.DataStorage.LoggedUser);
            }
            mainWindowViewModel.IsFollowing = !isFollowing;

            mainWindowViewModel.UnitOfWork.UserRepository.Update(mainWindowViewModel.User);
            mainWindowViewModel.UnitOfWork.UserRepository.Update(mainWindowViewModel.DataStorage.LoggedUser);
            mainWindowViewModel.UnitOfWork.Save();
        }
    }
}
