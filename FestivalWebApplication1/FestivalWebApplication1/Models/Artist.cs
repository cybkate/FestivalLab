using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FestivalWebApplication1.Models;

public partial class Artist
{
    public int ArtistId { get; set; }
    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Ім'я")]

    public string ArtistName { get; set; } = null!;
    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Дата народження")]

    public DateTime ArtistBirthDate { get; set; }

    public virtual Picture ArtistNavigation { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; } = new List<Ticket>();
}
