using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FestivalWebApplication1.Models;

public partial class Ticket
{
    public int TicketId { get; set; }
    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Ціна квитка")]

    public decimal TicketPrice { get; set; }
    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Тип квитка")]

    public byte TicketType { get; set; }
    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Дата фестивалю")]

    public DateTime TicketDate { get; set; }
    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Гість")]

    public int GuestId { get; set; }

    public virtual Artist Guest { get; set; } = null!;

    public virtual Participant Guest1 { get; set; } = null!;

    public virtual Cosplayer GuestNavigation { get; set; } = null!;
}
