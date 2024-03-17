﻿namespace E_Commerce.Application.Common
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}