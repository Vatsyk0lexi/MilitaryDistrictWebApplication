using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;

public partial class MilitaryDistrictContext : DbContext
{
    public MilitaryDistrictContext()
    {
    }

    public MilitaryDistrictContext(DbContextOptions<MilitaryDistrictContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Army> Armies { get; set; }

    public virtual DbSet<Brigade> Brigades { get; set; }

    public virtual DbSet<Bulding> Buldings { get; set; }

    public virtual DbSet<BuldingsInMilitaryBase> BuldingsInMilitaryBases { get; set; }

    public virtual DbSet<CategoriesOfRank> CategoriesOfRanks { get; set; }

    public virtual DbSet<CategoryOfMilitaryEquipment> CategoryOfMilitaryEquipments { get; set; }

    public virtual DbSet<Commander> Commanders { get; set; }

    public virtual DbSet<Corps> Corps { get; set; }

    public virtual DbSet<Departament> Departaments { get; set; }

    public virtual DbSet<Division> Divisions { get; set; }

    public virtual DbSet<KindOfMilitaryEquipment> KindOfMilitaryEquipments { get; set; }

    public virtual DbSet<KindOfMilitaryWeapon> KindOfMilitaryWeapons { get; set; }

    public virtual DbSet<Military> Militaries { get; set; }

    public virtual DbSet<MilitaryBase> MilitaryBases { get; set; }

    public virtual DbSet<MilitaryDistrict> MilitaryDistricts { get; set; }

    public virtual DbSet<MilitaryEquipment> MilitaryEquipments { get; set; }

    public virtual DbSet<MilitaryEquipmentsInMilitaryBase> MilitaryEquipmentsInMilitaryBases { get; set; }

    public virtual DbSet<MilitaryWeapon> MilitaryWeapons { get; set; }

    public virtual DbSet<MilitaryWeaponsInMilitaryBase> MilitaryWeaponsInMilitaryBases { get; set; }

    public virtual DbSet<PartsOfMilitaryDistrict> PartsOfMilitaryDistricts { get; set; }

    public virtual DbSet<PlacesOfDeployment> PlacesOfDeployments { get; set; }

    public virtual DbSet<Platoon> Platoons { get; set; }

    public virtual DbSet<Rank> Ranks { get; set; }

    public virtual DbSet<Rotum> Rota { get; set; }

    public virtual DbSet<Specialty> Specialties { get; set; }

    public virtual DbSet<SpecialtyOfMilitary> SpecialtyOfMilitaries { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("data source=PALMA;initial catalog=MilitaryDistrict;trusted_connection=true;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Army>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Army__3214EC2707038DB4");

            entity.ToTable("Army");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.CommanderId).HasColumnName("CommanderID");
            entity.Property(e => e.Name)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.PartsOfMilDistrId).HasColumnName("PartsOfMilDistrID");

            entity.HasOne(d => d.Commander).WithMany(p => p.Armies)
                .HasForeignKey(d => d.CommanderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CommanderOfArmy");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Army)
                .HasForeignKey<Army>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PartsOfMilDistrInARMY");
        });

        modelBuilder.Entity<Brigade>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Brigades__3214EC2775F09600");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.CommanderId).HasColumnName("CommanderID");
            entity.Property(e => e.DivisionId).HasColumnName("DivisionID");
            entity.Property(e => e.Name)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.PartsOfMilDistrId).HasColumnName("PartsOfMilDistrID");

            entity.HasOne(d => d.Commander).WithMany(p => p.Brigades)
                .HasForeignKey(d => d.CommanderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CommanderOfBrigade");

            entity.HasOne(d => d.Division).WithMany(p => p.Brigades)
                .HasForeignKey(d => d.DivisionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DivisionOfBrigade");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Brigade)
                .HasForeignKey<Brigade>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PartsOfMilDistrInBrigade");
        });

        modelBuilder.Entity<Bulding>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Buldings__3214EC27BA07DF85");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ForAccommodation).HasDefaultValueSql("('0')");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        modelBuilder.Entity<BuldingsInMilitaryBase>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Buldings__3214EC27BF0CE172");

            entity.ToTable("BuldingsInMilitaryBase");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AmountOfDeployedPartsMb)
                .HasDefaultValueSql("('0')")
                .HasColumnName("AmountOfDeployedPartsMB");
            entity.Property(e => e.MilitaryBaseId).HasColumnName("MilitaryBaseID");

            entity.HasOne(d => d.Bulding).WithMany(p => p.BuldingsInMilitaryBases)
                .HasForeignKey(d => d.BuldingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BuldingId");

            entity.HasOne(d => d.MilitaryBase).WithMany(p => p.BuldingsInMilitaryBases)
                .HasForeignKey(d => d.MilitaryBaseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MilitaryBase");
        });

        modelBuilder.Entity<CategoriesOfRank>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3214EC27006032B5");

            entity.ToTable("CategoriesOfRank");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(80)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CategoryOfMilitaryEquipment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Category__3214EC2779908ECE");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Commander>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Commande__3214EC27AE8AFB29");

            entity.ToTable("Commander");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Fullname)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.RankId).HasColumnName("RankID");

            entity.HasOne(d => d.Rank).WithMany(p => p.Commanders)
                .HasForeignKey(d => d.RankId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RankOfCommander");
        });

        modelBuilder.Entity<Corps>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Corps__3214EC27113ACC25");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.ArmyId).HasColumnName("ArmyID");
            entity.Property(e => e.CommanderId).HasColumnName("CommanderID");
            entity.Property(e => e.Name)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.PartsOfMilDistrId).HasColumnName("PartsOfMilDistrID");

            entity.HasOne(d => d.Army).WithMany(p => p.Corps)
                .HasForeignKey(d => d.ArmyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ArmyOfCorps");

            entity.HasOne(d => d.Commander).WithMany(p => p.Corps)
                .HasForeignKey(d => d.CommanderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CommanderOfCorps");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Corps)
                .HasForeignKey<Corps>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PartsOfMilDistrInCorp");
        });

        modelBuilder.Entity<Departament>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Departam__3214EC27C5DA2121");

            entity.ToTable("Departament");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CommanderId).HasColumnName("CommanderID");
            entity.Property(e => e.MilitaryBaseId).HasColumnName("MilitaryBaseID");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.HasOne(d => d.Commander).WithMany(p => p.Departaments)
                .HasForeignKey(d => d.CommanderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CommanderOfDepartament");

            entity.HasOne(d => d.MilitaryBase).WithMany(p => p.Departaments)
                .HasForeignKey(d => d.MilitaryBaseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MilitaryBaseOfDepartament");

            entity.HasOne(d => d.Platoon).WithMany(p => p.Departaments)
                .HasForeignKey(d => d.PlatoonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PlatoonOfDepartament");
        });

        modelBuilder.Entity<Division>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Division__3214EC27303C2ECA");

            entity.ToTable("Division");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.CommanderId).HasColumnName("CommanderID");
            entity.Property(e => e.CorpId).HasColumnName("CorpID");
            entity.Property(e => e.Name)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.PartsOfMilDistrId).HasColumnName("PartsOfMilDistrID");

            entity.HasOne(d => d.Commander).WithMany(p => p.Divisions)
                .HasForeignKey(d => d.CommanderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CommanderOfDivision");

            entity.HasOne(d => d.Corp).WithMany(p => p.Divisions)
                .HasForeignKey(d => d.CorpId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CorpOfDivision");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Division)
                .HasForeignKey<Division>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PartsOfMilDistrInDivision");
        });

        modelBuilder.Entity<KindOfMilitaryEquipment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__KindOfMi__3214EC27484FD75D");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<KindOfMilitaryWeapon>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__KindOfMi__3214EC2708276B85");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Military>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Military__3214EC2764800FA0");

            entity.ToTable("Military");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FullName)
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.HasOne(d => d.Departament).WithMany(p => p.Militaries)
                .HasForeignKey(d => d.DepartamentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DepartamentOfMilitary");

            entity.HasOne(d => d.Rank).WithMany(p => p.Militaries)
                .HasForeignKey(d => d.RankId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RankOfMilitary");
        });

        modelBuilder.Entity<MilitaryBase>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Military__3214EC271C0DA52D");

            entity.ToTable("MilitaryBase");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CommanderId).HasColumnName("CommanderID");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.PlacesOfDeploymentId).HasColumnName("PlacesOfDeploymentID");
            entity.Property(e => e.SubjectId).HasColumnName("SubjectID");

            entity.HasOne(d => d.Commander).WithMany(p => p.MilitaryBases)
                .HasForeignKey(d => d.CommanderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CommanderOfMilBase");

            entity.HasOne(d => d.PlacesOfDeployment).WithMany(p => p.MilitaryBases)
                .HasForeignKey(d => d.PlacesOfDeploymentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PlacesOfDeplOfMilBase");

            entity.HasOne(d => d.Subject).WithMany(p => p.MilitaryBases)
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PartOfMilDisInMilBase");
        });

        modelBuilder.Entity<MilitaryDistrict>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Military__3214EC27CABC5495");

            entity.ToTable("MilitaryDistrict");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CommanderId).HasColumnName("CommanderID");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.HasOne(d => d.Commander).WithMany(p => p.MilitaryDistricts)
                .HasForeignKey(d => d.CommanderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CommanderOfMilitaryDistrict");
        });

        modelBuilder.Entity<MilitaryEquipment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Military__3214EC27CCF7ED41");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CategoryOfMilitaryEquipmentId).HasColumnName("CategoryOfMilitaryEquipmentID");
            entity.Property(e => e.KindOfMilitaryEquipmentId).HasColumnName("KindOfMilitaryEquipmentID");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.CategoryOfMilitaryEquipment).WithMany(p => p.MilitaryEquipments)
                .HasForeignKey(d => d.CategoryOfMilitaryEquipmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CategoryOfMilitaryEquipmentID");

            entity.HasOne(d => d.KindOfMilitaryEquipment).WithMany(p => p.MilitaryEquipments)
                .HasForeignKey(d => d.KindOfMilitaryEquipmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_KindOfMilitaryEquipmentID");
        });

        modelBuilder.Entity<MilitaryEquipmentsInMilitaryBase>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Military__3214EC275FF71ADC");

            entity.ToTable("MilitaryEquipmentsInMilitaryBase");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.MilitaryBaseId).HasColumnName("MilitaryBaseID");
            entity.Property(e => e.MilitaryEquipmentsId).HasColumnName("MilitaryEquipmentsID");

            entity.HasOne(d => d.MilitaryBase).WithMany(p => p.MilitaryEquipmentsInMilitaryBases)
                .HasForeignKey(d => d.MilitaryBaseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MilitaryBaseOfMilitaryEquipm");

            entity.HasOne(d => d.MilitaryEquipments).WithMany(p => p.MilitaryEquipmentsInMilitaryBases)
                .HasForeignKey(d => d.MilitaryEquipmentsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MilitaryEquipmentID");
        });

        modelBuilder.Entity<MilitaryWeapon>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Military__3214EC2713D7FAD0");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.KindOfMilitaryWeaponId).HasColumnName("KindOfMilitaryWeaponID");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.KindOfMilitaryWeapon).WithMany(p => p.MilitaryWeapons)
                .HasForeignKey(d => d.KindOfMilitaryWeaponId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_KindOfMilitaryWeaponID");
        });

        modelBuilder.Entity<MilitaryWeaponsInMilitaryBase>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Military__3214EC272E847CB8");

            entity.ToTable("MilitaryWeaponsInMilitaryBase");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.MilitaryBaseId).HasColumnName("MilitaryBaseID");
            entity.Property(e => e.MilitaryWeaponId).HasColumnName("MilitaryWeaponID");

            entity.HasOne(d => d.MilitaryBase).WithMany(p => p.MilitaryWeaponsInMilitaryBases)
                .HasForeignKey(d => d.MilitaryBaseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MilitaryBaseOfMilitaryWeapon");

            entity.HasOne(d => d.MilitaryWeapon).WithMany(p => p.MilitaryWeaponsInMilitaryBases)
                .HasForeignKey(d => d.MilitaryWeaponId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MilitaryWeaponID");
        });

        modelBuilder.Entity<PartsOfMilitaryDistrict>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PartsOfM__3214EC270A83CE8A");

            entity.ToTable("PartsOfMilitaryDistrict");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.MilitaryDistrictId).HasColumnName("MilitaryDistrictID");

            entity.HasOne(d => d.MilitaryDistrict).WithMany(p => p.PartsOfMilitaryDistricts)
                .HasForeignKey(d => d.MilitaryDistrictId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MilitaryDistrictInPartsOfMD");
        });

        modelBuilder.Entity<PlacesOfDeployment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PlacesOf__3214EC2738858E14");

            entity.ToTable("PlacesOfDeployment");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Settlement)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Platoon>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Platoon__3214EC27E10DA779");

            entity.ToTable("Platoon");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CommanderId).HasColumnName("CommanderID");
            entity.Property(e => e.MilitaryBaseId).HasColumnName("MilitaryBaseID");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.HasOne(d => d.Commander).WithMany(p => p.Platoons)
                .HasForeignKey(d => d.CommanderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CommanderOfPlatoon");

            entity.HasOne(d => d.MilitaryBase).WithMany(p => p.Platoons)
                .HasForeignKey(d => d.MilitaryBaseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MilitaryBaseOfPlatoon");

            entity.HasOne(d => d.Rota).WithMany(p => p.Platoons)
                .HasForeignKey(d => d.RotaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RotaOfPlatoon");
        });

        modelBuilder.Entity<Rank>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ranks__3214EC274FDB840B");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.Name)
                .HasMaxLength(80)
                .IsUnicode(false);

            entity.HasOne(d => d.Category).WithMany(p => p.Ranks)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CategoryOfRank");
        });

        modelBuilder.Entity<Rotum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Rota__3214EC27F85EB3AA");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CommanderId).HasColumnName("CommanderID");
            entity.Property(e => e.MilitaryBaseId).HasColumnName("MilitaryBaseID");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.HasOne(d => d.Commander).WithMany(p => p.Rota)
                .HasForeignKey(d => d.CommanderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CommanderOfRota");

            entity.HasOne(d => d.MilitaryBase).WithMany(p => p.Rota)
                .HasForeignKey(d => d.MilitaryBaseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MilitaryBaseOfRota");
        });

        modelBuilder.Entity<Specialty>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Specialt__3214EC27B34C27EA");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SpecialtyOfMilitary>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Specialt__3214EC27F03CE8E1");

            entity.ToTable("SpecialtyOfMilitary");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.MilitaryId).HasColumnName("MilitaryID");

            entity.HasOne(d => d.Military).WithMany(p => p.SpecialtyOfMilitaries)
                .HasForeignKey(d => d.MilitaryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Military");

            entity.HasOne(d => d.Specialty).WithMany(p => p.SpecialtyOfMilitaries)
                .HasForeignKey(d => d.SpecialtyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SpecialtyOfMilitary");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
