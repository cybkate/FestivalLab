using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FestivalWebApplication1.Models;

public partial class Participant
{
    public int ParticipantId { get; set; }
    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Ім'я")]

    public string ParticipantName { get; set; } = null!;
    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Дата народження")]

    public DateTime ParticipantBirthDate { get; set; }
    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Тип")]

    public byte ParticipantType { get; set; }

    public virtual ICollection<Ticket> Tickets { get; } = new List<Ticket>();
}
