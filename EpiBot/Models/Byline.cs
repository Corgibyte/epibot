namespace EpiBot.Models
{
  public class Byline
  {
    public int BylineId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }

    public override string ToString()
    {
      return $"Co-authored-by: {Name} <{Email}>";
    }
  }
}