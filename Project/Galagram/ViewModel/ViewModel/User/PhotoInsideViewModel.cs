﻿using System.Windows.Input;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Galagram.ViewModel.ViewModel.User
{
    /// <summary>
    /// A logic class for <see cref="Galagram.Window.User.PhotoInside"/>
    /// </summary>
    public class PhotoInsideViewModel : ViewModelBase
    {
        // FIELDS
        DataAccess.Entities.Photo photo;
        int selectedCommentIndex;
        #warning set it to array after optimization in future milestones
        ObservableCollection<DataAccess.Entities.Comment> comments;
        string commentText;

        DataAccess.Structs.LikeDislikeAmount likeDislikeAmount;

        ICommand likePhotoCommand;
        ICommand likeCommentCommand;
        ICommand writeCommentCommand;
        ICommand deleteCommentCommand;
        // CONSTRUCTORS
        /// <summary>
        /// Initialize a new instance of <see cref="PhotoInsideViewModel"/>
        /// </summary>
        public PhotoInsideViewModel(DataAccess.Entities.Photo photo)
        {
            this.photo = photo;
            this.selectedCommentIndex = Core.Configuration.Constants.WRONG_INDEX;
            this.comments = new ObservableCollection<DataAccess.Entities.Comment>(photo.Comments);
            this.commentText = string.Empty;

            this.likeDislikeAmount = UnitOfWork.PhotoRepository.GetLikeDislikeAmount(photo);

            this.likePhotoCommand = new Commands.User.PhotoInside.LikePhotoCommand(this);
            this.likeCommentCommand = new Commands.User.PhotoInside.LikeCommentCommand(this);
            this.writeCommentCommand = new Commands.User.PhotoInside.WriteCommentCommand(this);
            this.deleteCommentCommand = new Commands.User.PhotoInside.DeleteCommentCommand(this);
        }

        // PROPERTIES
        /// <summary>
        /// Gets max comment length
        /// </summary>
        public int MaxCommentLength => Core.Configuration.DBConfig.COMMENT_TEXT_MAX_LENGTH;
        /// <summary>
        /// Gets like, dislike amount
        /// </summary>
        public DataAccess.Structs.LikeDislikeAmount LikeDislikeAmount
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(LikeDislikeAmount)}");
                Logger.LogAsync(Core.LogMode.Info, $"{nameof(LikeDislikeAmount)}. Like = {likeDislikeAmount.LikesAmount}, Dislikes = {likeDislikeAmount.DisLikesAmount}");

                return likeDislikeAmount;
            }
        }
        /// <summary>
        /// Gets opened photo
        /// </summary>
        public DataAccess.Entities.Photo Photo
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(Photo)}");
                return photo;
            }
        }
        /// <summary>
        /// Gets or sets selected index for photo
        /// </summary>
        public int SelectedCommentIndex
        {
            get
            {

                return selectedCommentIndex;
            }
            set
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Sets {nameof(SelectedCommentIndex)}");
                Logger.LogAsync(Core.LogMode.Info, $"{nameof(SelectedCommentIndex)}. Old value = {selectedCommentIndex}, new value = {value}");

                selectedCommentIndex = value;

                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Gets or sets comments to photo
        /// </summary>
        public ObservableCollection<DataAccess.Entities.Comment> Comments // => to []
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(Comments)}");

                return comments;
            }
            set
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Sets {nameof(Comments)}");
                comments = value;

                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Gets or sets new comment text
        /// </summary>
        public string CommentText
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(CommentText)}");

                return commentText;
            }
            set
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Sets {nameof(CommentText)}");
                Logger.LogAsync(Core.LogMode.Info, $"{nameof(CommentText)}. Old value = {commentText}, new value = {value}");

                commentText = value;

                OnPropertyChanged();
            }
        }

        // COMMAND
        /// <summary>
        /// Gets action to like or dislike photo
        /// </summary>
        public ICommand LikePhotoCommand
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(LikePhotoCommand)}");

                return likePhotoCommand;
            }
        }
        /// <summary>
        /// Gets action to like or dislike comment
        /// </summary>
        public ICommand LikeCommentCommand
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(LikeCommentCommand)}");

                return likeCommentCommand;
            }
        }
        /// <summary>
        /// Gets acion to write comment
        /// </summary>
        public ICommand WriteCommentCommand
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(WriteCommentCommand)}");

                return writeCommentCommand;
            }
        }
        /// <summary>
        /// Gets action to delete current comment
        /// </summary>
        public ICommand DeleteCommentCommand
        {
            get
            {
                Logger.LogAsync(Core.LogMode.Debug, $"Gets {nameof(DeleteCommentCommand)}");

                return deleteCommentCommand;
            }
        }

        // METHODS
        /// <summary>
        /// Raised event <see cref="PropertyChangedEventHandler"/> for <see cref="LikeDislikeAmount"/>
        /// </summary>
        public void UpdateLikes()
        {
            OnPropertyChanged(nameof(LikeDislikeAmount));
        }
    }
}
