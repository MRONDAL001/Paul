#region Using

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartAdmin.Web.Models;
using SmartAdmin.Web.Models.Sistema;

#endregion

namespace SmartAdmin.Web.Data
{
    /// <summary>
    ///     Defines the Entity Framework database context instance used by the application.
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
       
        /// <summary>
        ///     Configures the schema needed for the application identity framework.
        /// </summary>
        /// <param name="builder">The builder being used to construct the model for this application context.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);           

            modelBuilder.Entity<CategoriasRiesgo>(entity =>
            {
                entity.HasKey(e => e.IdCategoriasRiesgo);

                entity.Property(e => e.Descripcion).HasColumnType("text");

                entity.HasOne(d => d.IdRiesgoNavigation)
                    .WithMany(p => p.CategoriasRiesgo)
                    .HasForeignKey(d => d.IdRiesgo)
                    .HasConstraintName("FK_CategoriasRiesgo_Riesgo");
            });

            modelBuilder.Entity<Persona>(entity =>
            {
                entity.HasKey(e => e.IdPersona);

                entity.Property(e => e.Apellido)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Cedula)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Problema>(entity =>
            {
                entity.HasKey(e => e.IdProblema);

                entity.Property(e => e.Descripcion).HasColumnType("text");
            });

            modelBuilder.Entity<ProblemaRiesgo>(entity =>
            {
                entity.HasKey(e => e.IdProblemaRiesgo);

                entity.Property(e => e.Descripcion).HasColumnType("text");

                entity.HasOne(d => d.IdCategoriaRiesgoNavigation)
                    .WithMany(p => p.ProblemaRiesgo)
                    .HasForeignKey(d => d.IdCategoriaRiesgo)
                    .HasConstraintName("FK_ProblemaRiesgo_CategoriasRiesgo");
            });

            modelBuilder.Entity<Riesgo>(entity =>
            {
                entity.HasKey(e => e.IdRiesgo);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Tecnico>(entity =>
            {
                entity.HasKey(e => e.IdTecnico);

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.Tecnico)
                    .HasForeignKey(d => d.IdPersona)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tecnico_Persona");
            });
        }

        /// <summary>
        ///     Configures the schema needed for the application identity framework.
        /// </summary>
        /// <param name="builder">The builder being used to construct the model for this application context.</param>
        public DbSet<SmartAdmin.Web.Models.Sistema.Persona> Persona { get; set; }

        /// <summary>
        ///     Configures the schema needed for the application identity framework.
        /// </summary>
        /// <param name="builder">The builder being used to construct the model for this application context.</param>
        public DbSet<SmartAdmin.Web.Models.Sistema.Problema> Problema { get; set; }

        /// <summary>
        ///     Configures the schema needed for the application identity framework.
        /// </summary>
        /// <param name="builder">The builder being used to construct the model for this application context.</param>
        public DbSet<SmartAdmin.Web.Models.Sistema.Riesgo> Riesgo { get; set; }

        /// <summary>
        ///     Configures the schema needed for the application identity framework.
        /// </summary>
        /// <param name="builder">The builder being used to construct the model for this application context.</param>
        public DbSet<SmartAdmin.Web.Models.Sistema.CategoriasRiesgo> CategoriasRiesgo { get; set; }

        /// <summary>
        ///     Configures the schema needed for the application identity framework.
        /// </summary>
        /// <param name="builder">The builder being used to construct the model for this application context.</param>
        public DbSet<SmartAdmin.Web.Models.Sistema.ProblemaRiesgo> ProblemaRiesgo { get; set; }

        /// <summary>
        ///     Configures the schema needed for the application identity framework.
        /// </summary>
        /// <param name="builder">The builder being used to construct the model for this application context.</param>
        public DbSet<SmartAdmin.Web.Models.Sistema.Tecnico> Tecnico { get; set; }
    }
}
