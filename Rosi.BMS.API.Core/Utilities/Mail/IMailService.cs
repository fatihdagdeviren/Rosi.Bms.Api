namespace Rosi.BMS.API.Core.Utilities.Mail
{
    public interface IMailService
    {
        void Send(EmailMessage emailMessage);
    }
}