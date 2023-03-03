using System.ComponentModel.DataAnnotations;

namespace TUF.Client.Client.Areas.ChatingRoom.Models;

public class AvatarModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ImgSrc { get; set; }
    public bool Select { get; set; }
    public string BackColor { get; set; }
    public string Desc { get; set; }
    [MaxLength(10, ErrorMessage = "10자까지")]
    [Required(ErrorMessage = "아이디 필수")]
    public string UserName { get; set; }
}

public static class GenericAvata
{
    public static List<AvatarModel> Makes()
    {
        string baseimgsrc = "images/avatar/";
        List<AvatarModel> lst = new();
        lst.Add(new AvatarModel() {Id=1, Name = "수아", ImgSrc = $"{baseimgsrc}av1.png", Select = false, BackColor = "", Desc="Ai 캐릭터1" });
        lst.Add(new AvatarModel() {Id=2, Name = "청하", ImgSrc = $"{baseimgsrc}av2.png", Select = false, BackColor = "", Desc = "Ai 캐릭터2" });
        lst.Add(new AvatarModel() {Id=3, Name = "미나", ImgSrc = $"{baseimgsrc}av3.png", Select = false, BackColor = "", Desc = "Ai 캐릭터3" });
        lst.Add(new AvatarModel() {Id=4, Name = "소희", ImgSrc = $"{baseimgsrc}av4.png", Select = false, BackColor = "" , Desc = "Ai 캐릭터4" });
        lst.Add(new AvatarModel() {Id=5, Name = "가희", ImgSrc = $"{baseimgsrc}av5.png", Select = false, BackColor = "" , Desc = "Ai 캐릭터5" });
        lst.Add(new AvatarModel() {Id=6, Name = "로라", ImgSrc = $"{baseimgsrc}av6.png", Select = false, BackColor = "" , Desc = "Ai 캐릭터6" });
        lst.Add(new AvatarModel() {Id=7, Name = "다현", ImgSrc = $"{baseimgsrc}av7.png", Select = false, BackColor = "", Desc = "Ai 캐릭터7" });
        lst.Add(new AvatarModel() {Id=8, Name = "지현", ImgSrc = $"{baseimgsrc}av8.png", Select = false, BackColor = "" , Desc = "Ai 캐릭터8" });
        lst.Add(new AvatarModel() {Id=9, Name = "미현", ImgSrc = $"{baseimgsrc}av9.png", Select = false, BackColor = "" , Desc = "Ai 캐릭터9" });
        lst.Add(new AvatarModel() {Id=10, Name = "은정", ImgSrc = $"{baseimgsrc}av10.png", Select = false, BackColor = "" , Desc = "Ai 캐릭터10" }); 

        return lst;
    }
}
