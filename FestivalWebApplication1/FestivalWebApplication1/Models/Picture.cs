using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FestivalWebApplication1.Models;

public partial class Picture
{
    public int PictureId { get; set; }
    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Кількість малюнків")]

    public int NumberOfPictures { get; set; }
    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Ціна")]

    public decimal PicturePrice { get; set; }
    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Фандом")]

    public int FandomId { get; set; }

    public virtual Artist? Artist { get; set; }

    public virtual Fandom? Fandom { get; set; }
}
