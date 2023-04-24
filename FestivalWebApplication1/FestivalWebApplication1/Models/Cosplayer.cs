using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FestivalWebApplication1.Models;

public partial class Cosplayer
{
    public int CosplayerId { get; set; }
    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Ім'я")]

    public string CosplayerName { get; set; } = null!;
    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Дата народження")]

    public DateTime CosplayerBirthDate { get; set; }
    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Тип (соло/команда)")]

    public string CosplayerType { get; set; } = null!;

    public int FandomId { get; set; }

    public int? CosplayerTeamId { get; set; }

    public virtual Fandom Cosplayer1 { get; set; } = null!;

    public virtual CosplayerTeam CosplayerNavigation { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; } = new List<Ticket>();
}
