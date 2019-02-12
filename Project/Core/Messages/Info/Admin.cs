﻿namespace Core.Messages.Info
{
    /// <summary>
    /// Consists of all messages happen in Admin Panel
    /// </summary>
    public static class Admin
    {
        /// <summary>
        /// Wrong subject length
        /// </summary>
        public static readonly string ADMIN_WRONG_SUBJECT_LENGTH = string.Concat("Current subject length is not allowed", 
                                                                                System.Environment.NewLine, 
                                                                                "Min length = ", Configuration.DBConfig.ADMIN_MESSAGE_SUBJECT_MIN_LENGTH, 
                                                                                ", max length = ", Configuration.DBConfig.ADMIN_MESSAGE_SUBJECT_MAX_LENGTH);
    }
}
