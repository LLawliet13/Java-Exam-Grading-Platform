namespace ChamThiDotnet5.Services
{
    public interface MailService
    {
        public abstract void SendMail(string _from, string _to, string _subject, string _body);
    }
}
