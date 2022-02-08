using Microsoft.EntityFrameworkCore;


namespace UWS.Shared
{
    public class Chinook : DbContext
    {
        public Chinook(DbContextOptions<Chinook> options) : base(options) { }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Invoice_Item> Invoice_Items { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Media_Type> Media_Types { get; set; }
        public DbSet<Playlist_Track> Playlist_Tracks { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<Track> Tracks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          base.OnModelCreating(modelBuilder);

          //define primary key for Albums table
          modelBuilder.Entity<Album>()
          .HasKey(al => new {al.AlbumId});
          
          modelBuilder.Entity<Album>()
          .Property(al => al.Title)
          .IsRequired()
          .HasMaxLength(160);

          //define one-to-many relationship
          modelBuilder.Entity<Album>()
          .HasMany(al => al.Tracks)
          .WithOne(t => t.Album);

          //define foreign key ArtistId
          modelBuilder.Entity<Album>()
          .HasOne(al => al.Artist)
          .WithMany(ar => ar.Albums)
          .IsRequired().HasForeignKey(al => al.ArtistId)
          .OnDelete(DeleteBehavior.Cascade);

          //define primary key for Artists table
          modelBuilder.Entity<Artist>()
          .HasKey(ar => ar.ArtistId);

          modelBuilder.Entity<Artist>()
          .Property(ar => ar.Name)
          .HasMaxLength(120);

          modelBuilder.Entity<Artist>()
          .HasMany(ar => ar.Albums)
          .WithOne(ar => ar.Artist);

          //define primary key for Customers table
          modelBuilder.Entity<Customer>()
          .HasKey(c => c.CustomerId);

          modelBuilder.Entity<Customer>()
          .Property(c => c.FirstName)
          .IsRequired()
          .HasMaxLength(40);

          modelBuilder.Entity<Customer>()
          .Property(c => c.LastName)
          .IsRequired()
          .HasMaxLength(20);

          modelBuilder.Entity<Customer>()
          .Property(c => c.Company)
          .HasMaxLength(80);

          modelBuilder.Entity<Customer>()
          .Property(c => c.Address)
          .HasMaxLength(70);

          modelBuilder.Entity<Customer>()
          .Property(c => c.City)
          .HasMaxLength(40);

          modelBuilder.Entity<Customer>()
          .Property(c => c.State)
          .HasMaxLength(40);

          modelBuilder.Entity<Customer>()
          .Property(c => c.Country)
          .HasMaxLength(40);

          modelBuilder.Entity<Customer>()
          .Property(c => c.PostalCode)
          .HasMaxLength(10);

          modelBuilder.Entity<Customer>()
          .Property(c => c.Phone)
          .HasMaxLength(24);

          modelBuilder.Entity<Customer>()
          .Property(c => c.Fax)
          .HasMaxLength(24);

          modelBuilder.Entity<Customer>()
          .Property(c => c.Email)
          .IsRequired()
          .HasMaxLength(60);

          //define one-to-many relationship
          modelBuilder.Entity<Customer>()
          .HasMany(c => c.Invoices)
          .WithOne(i => i.Customer);

          //define PK for Employees table
          modelBuilder.Entity<Employee>()
          .HasKey(e => e.EmployeeId);

          modelBuilder.Entity<Employee>()
          .Property(e => e.LastName)
          .IsRequired()
          .HasMaxLength(20);

          modelBuilder.Entity<Employee>()
          .Property(e => e.FirstName)
          .IsRequired()
          .HasMaxLength(20);

          modelBuilder.Entity<Employee>()
          .Property(e => e.Title)
          .HasMaxLength(30);

          modelBuilder.Entity<Employee>()
          .Property(e => e.Address)
          .HasMaxLength(70);

          modelBuilder.Entity<Employee>()
          .Property(e => e.City)
          .HasMaxLength(40);

          modelBuilder.Entity<Employee>()
          .Property(e => e.State)
          .HasMaxLength(40);

          modelBuilder.Entity<Employee>()
          .Property(e => e.Country)
          .HasMaxLength(40);

          modelBuilder.Entity<Employee>()
          .Property(e => e.PostalCode)
          .HasMaxLength(24);

          modelBuilder.Entity<Employee>()
          .Property(e => e.Phone)
          .HasMaxLength(10);

          modelBuilder.Entity<Employee>()
          .Property(e => e.Fax)
          .HasMaxLength(24);

          modelBuilder.Entity<Employee>()
          .Property(e => e.Email)
          .HasMaxLength(60);

          // define a one-to-many relationship
          modelBuilder.Entity<Employee>()
          .HasMany(e => e.Customers)
          .WithOne(c => c.Employee);

          // define PK for Genres table
          modelBuilder.Entity<Genre>()
          .HasKey(g => g.GenreId);

          modelBuilder.Entity<Genre>()
          .Property(g => g.Name)
          .HasMaxLength(120);

          modelBuilder.Entity<Genre>()
          .HasMany(g => g.Tracks)
          .WithOne(t => t.Genre);

          // define primary key for Invoice_Items table
          modelBuilder.Entity<Invoice_Item>()
          .HasKey(ii => new {ii.InvoiceLineId});

          //define foreign key InvoiceId
          modelBuilder.Entity<Invoice_Item>()
          .HasOne(ii => ii.Invoice)
          .WithMany(i => i.Invoice_Items)
          .HasForeignKey(ii => ii.InvoiceId);

          // define foreign key TrackId
          modelBuilder.Entity<Invoice_Item>()
          .HasOne(ii => ii.Track)
          .WithMany(t => t.Invoice_Items)
          .HasForeignKey(ii => ii.TrackId);

          // define primary key for Invoices table
          modelBuilder.Entity<Invoice>()
          .HasKey(i => new {i.InvoiceId});

          //define foreign key CustomerId
          modelBuilder.Entity<Invoice>()
          .HasOne(i => i.Customer)
          .WithMany(c => c.Invoices)
          .HasForeignKey(ii => ii.CustomerId);

          modelBuilder.Entity<Invoice>()
          .Property(i=> i.InvoiceDate)
          .IsRequired();

          modelBuilder.Entity<Invoice>()
          .Property(i => i.BillingAddress)
          .HasMaxLength(70);

          modelBuilder.Entity<Invoice>()
          .Property(i => i.BillingCity)
          .HasMaxLength(40);

          modelBuilder.Entity<Invoice>()
          .Property(i => i.BillingState)
          .HasMaxLength(40);

          modelBuilder.Entity<Invoice>()
          .Property(i => i.BillingCountry)
          .HasMaxLength(40);

          modelBuilder.Entity<Invoice>()
          .Property(i => i.BillingPostalCode)
          .HasMaxLength(10);

          modelBuilder.Entity<Invoice>()
          .Property(i=> i.Total)
          .IsRequired();

          modelBuilder.Entity<Invoice>()
          .HasMany(i => i.Invoice_Items)
          .WithOne(ii => ii.Invoice);

          // define primary key for Media_Types table
          modelBuilder.Entity<Media_Type>()
          .HasKey(mt => new {mt.MediaTypeId});

          modelBuilder.Entity<Media_Type>()
          .Property(mt => mt.Name)
          .HasMaxLength(120);

          // define multi-column primary key
          // for Playlist_Track table 
          modelBuilder.Entity<Playlist_Track>()
          .HasKey(pt => new { pt.PlaylistId, pt.TrackId });

          // define PK for Playlists table
          modelBuilder.Entity<Playlist>()
          .HasKey(p => new {p.PlaylistId});

          modelBuilder.Entity<Playlist>()
          .Property(p => p.Name)
          .HasMaxLength(120);

          // define primary key for Tracks table
          modelBuilder.Entity<Track>()
          .HasKey(t => new { t.TrackId });
              
          modelBuilder.Entity<Track>()
          .Property(t => t.Name)
          .IsRequired()
          .HasMaxLength(200);

          //define foreign key AlbumId
          modelBuilder.Entity<Track>()
          .HasOne(t => t.Album)
          .WithMany(al => al.Tracks)
          .HasForeignKey(t => t.AlbumId);

          //define FK GenreId
          modelBuilder.Entity<Track>()
          .HasOne(t => t.Genre)
          .WithMany(g => g.Tracks)
          .HasForeignKey(t => t.GenreId);

          modelBuilder.Entity<Track>()
          .Property(t => t.Composer)
          .HasMaxLength(220);

          modelBuilder.Entity<Track>()
          .Property(t => t.Milliseconds)
          .IsRequired();

          modelBuilder.Entity<Track>()
          .Property(t => t.UnitPrice)
          .IsRequired();

          modelBuilder.Entity<Track>()
          .HasMany(t => t.Invoice_Items)
          .WithOne(ii => ii.Track);

          modelBuilder.Entity<Track>()
          .HasOne(t => t.Media_Type)
          .WithMany(mt => mt.Tracks);

          // define foreign key MediaTypeId
          modelBuilder.Entity<Track>()
          .HasOne(t => t.Media_Type)
          .WithMany(mt => mt.Tracks)
          .HasForeignKey(t => t.MediaTypeId);
        }

    }
}
