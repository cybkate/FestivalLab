using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FestivalWebApplication1.Models;

public partial class Lab1Context : DbContext
{
    public Lab1Context()
    {
    }

    public Lab1Context(DbContextOptions<Lab1Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Artist> Artists { get; set; }

    public virtual DbSet<Cosplayer> Cosplayers { get; set; }

    public virtual DbSet<CosplayerTeam> CosplayerTeams { get; set; }

    public virtual DbSet<Fandom> Fandoms { get; set; }

    public virtual DbSet<Participant> Participants { get; set; }

    public virtual DbSet<Picture> Pictures { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server= DESKTOP-BF3F2G9\\SQLEXPRESS;\nDatabase=Lab1; Trusted_Connection=True; Trust Server Certificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Artist>(entity =>
        {
            entity.ToTable("ARTIST");

            entity.Property(e => e.ArtistId)
                .ValueGeneratedNever()
                .HasColumnName("ARTIST_ID");
            entity.Property(e => e.ArtistBirthDate)
                .HasColumnType("date")
                .HasColumnName("ARTIST_BIRTH_DATE");
            entity.Property(e => e.ArtistName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ARTIST_NAME");

            entity.HasOne(d => d.ArtistNavigation).WithOne(p => p.Artist)
                .HasForeignKey<Artist>(d => d.ArtistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ARTIST_PICTURE");
        });

        modelBuilder.Entity<Cosplayer>(entity =>
        {
            entity.ToTable("COSPLAYER");

            entity.Property(e => e.CosplayerId)
                .ValueGeneratedNever()
                .HasColumnName("COSPLAYER_ID");
            entity.Property(e => e.CosplayerBirthDate)
                .HasColumnType("date")
                .HasColumnName("COSPLAYER_BIRTH_DATE");
            entity.Property(e => e.CosplayerName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("COSPLAYER_NAME");
            entity.Property(e => e.CosplayerTeamId).HasColumnName("COSPLAYER_TEAM_ID");
            entity.Property(e => e.CosplayerType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("COSPLAYER_TYPE");
            entity.Property(e => e.FandomId).HasColumnName("FANDOM_ID");

            entity.HasOne(d => d.CosplayerNavigation).WithOne(p => p.Cosplayer)
                .HasForeignKey<Cosplayer>(d => d.CosplayerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_COSPLAYER_COSPLAYER_TEAM");

            entity.HasOne(d => d.Cosplayer1).WithOne(p => p.Cosplayer)
                .HasForeignKey<Cosplayer>(d => d.CosplayerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_COSPLAYER_FANDOM");
        });

        modelBuilder.Entity<CosplayerTeam>(entity =>
        {
            entity.ToTable("COSPLAYER_TEAM");

            entity.Property(e => e.CosplayerTeamId)
                .ValueGeneratedNever()
                .HasColumnName("COSPLAYER_TEAM_ID");
            entity.Property(e => e.CosplayerTeamName)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("COSPLAYER_TEAM_NAME");
        });

        modelBuilder.Entity<Fandom>(entity =>
        {
            entity.ToTable("FANDOM");

            entity.Property(e => e.FandomId)
                .ValueGeneratedNever()
                .HasColumnName("FANDOM_ID");
            entity.Property(e => e.FandomName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FANDOM_NAME");

            entity.HasOne(d => d.FandomNavigation).WithOne(p => p.Fandom)
                .HasForeignKey<Fandom>(d => d.FandomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FANDOM_PICTURE");
        });

        modelBuilder.Entity<Participant>(entity =>
        {
            entity.ToTable("PARTICIPANT");

            entity.Property(e => e.ParticipantId)
                .ValueGeneratedNever()
                .HasColumnName("PARTICIPANT_ID");
            entity.Property(e => e.ParticipantBirthDate)
                .HasColumnType("date")
                .HasColumnName("PARTICIPANT_BIRTH_DATE");
            entity.Property(e => e.ParticipantName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PARTICIPANT_NAME");
            entity.Property(e => e.ParticipantType).HasColumnName("PARTICIPANT_TYPE");
        });

        modelBuilder.Entity<Picture>(entity =>
        {
            entity.ToTable("PICTURE");

            entity.Property(e => e.PictureId)
                .ValueGeneratedNever()
                .HasColumnName("PICTURE_ID");
            entity.Property(e => e.FandomId).HasColumnName("FANDOM_ID");
            entity.Property(e => e.NumberOfPictures).HasColumnName("NUMBER_OF_PICTURES");
            entity.Property(e => e.PicturePrice)
                .HasColumnType("smallmoney")
                .HasColumnName("PICTURE_PRICE");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.ToTable("TICKET");

            entity.Property(e => e.TicketId).HasColumnName("TICKET_ID");
            entity.Property(e => e.GuestId).HasColumnName("GUEST_ID");
            entity.Property(e => e.TicketDate)
                .HasColumnType("date")
                .HasColumnName("TICKET_DATE");
            entity.Property(e => e.TicketPrice)
                .HasColumnType("smallmoney")
                .HasColumnName("TICKET_PRICE");
            entity.Property(e => e.TicketType).HasColumnName("TICKET_TYPE");

            entity.HasOne(d => d.Guest).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.GuestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TICKET_ARTIST1");

            entity.HasOne(d => d.GuestNavigation).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.GuestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TICKET_COSPLAYER1");

            entity.HasOne(d => d.Guest1).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.GuestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TICKET_PARTICIPANT1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
