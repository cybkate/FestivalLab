using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FestivalWebApplication1.Models;

public partial class Fandom
{
    public int FandomId { get; set; }
    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Назва фандому")]

    public string FandomName { get; set; } = null!;

    public virtual Cosplayer? Cosplayer { get; set; }

    public virtual Picture FandomNavigation { get; set; } = null!;
}
