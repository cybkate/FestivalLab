using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FestivalWebApplication1.Models;

public partial class CosplayerTeam
{
    public int CosplayerTeamId { get; set; }
    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Назва команди")]

    public string CosplayerTeamName { get; set; } = null!;

    public virtual Cosplayer? Cosplayer { get; set; }
}
