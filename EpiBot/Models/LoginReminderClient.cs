using System;

namespace EpiBot.Models
{
  public class LoginReminderClient
  {
    public int LoginReminderClientId { get; set; }
    public ulong UserId { get; set; }
    public DateTime LastAMReminder { get; set; }
    public DateTime LastPMReminder { get; set; }

    public LoginReminderClient()
    {
      LastAMReminder = DateTime.UnixEpoch;
      LastPMReminder = DateTime.UnixEpoch;
    }
  }
}