using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LibManTest.Models;

public partial class BooksDbContext : DbContext
{
    public BooksDbContext()
    {
    }

    public BooksDbContext( DbContextOptions<BooksDbContext> options )
        : base( options )
    {
    }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder )
        => optionsBuilder.UseLazyLoadingProxies()
        .UseSqlServer( "Server=.\\SQLEXPRESS;Database=BooksDB;Trusted_Connection=True;MultipleActiveResultSets=True;Encrypt=false;" );

    protected override void OnModelCreating( ModelBuilder modelBuilder )
    {
        modelBuilder.Entity<Book>( entity =>
        {
            entity.HasKey( e => e.Isbn ).HasName( "PK__Books__447D36EB17914EBB" );

            entity.Property( e => e.Isbn )
                .HasMaxLength( 15 )
                .HasColumnName( "ISBN" );
            entity.Property( e => e.CategoryId ).HasColumnName( "CategoryID" );
            entity.Property( e => e.Title ).HasMaxLength( 150 );
            entity.Property( e => e.Year ).HasDefaultValueSql( "(getdate())" );

            entity.HasOne( d => d.Category ).WithMany( p => p.Books )
                .HasForeignKey( d => d.CategoryId )
                .OnDelete( DeleteBehavior.ClientSetNull )
                .HasConstraintName( "FK__Books__CategoryI__3A81B327" );
        } );

        modelBuilder.Entity<Category>( entity =>
        {
            entity.HasKey( e => e.CategoryId ).HasName( "PK__Categori__19093A2B43EFA2D5" );

            entity.Property( e => e.CategoryId )
                .ValueGeneratedNever()
                .HasColumnName( "CategoryID" );
            entity.Property( e => e.CategoryName ).HasMaxLength( 50 );
        } );

        OnModelCreatingPartial( modelBuilder );
    }

    partial void OnModelCreatingPartial( ModelBuilder modelBuilder );
}