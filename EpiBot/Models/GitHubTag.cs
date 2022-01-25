namespace EpiBot.Models
{
  public class GitHubTag
  {
    public int GitHubTagId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }

    public override string ToString()
    {
      return $"Co-authored-by: {Name} <{Email}>";
    }
  }
}