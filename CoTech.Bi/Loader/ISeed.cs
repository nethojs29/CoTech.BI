using CoTech.Bi.Entity;

namespace CoTech.Bi.Loader {
  public interface ISeed {
    int Version { get; }
    void Up(BiContext context);
    void Down(BiContext context);
  }
}