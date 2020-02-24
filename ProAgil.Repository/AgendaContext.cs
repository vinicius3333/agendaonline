using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;
using ProAgil.Domain.Identity;

namespace ProAgil.Repository
{
    public class AgendaContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>,
                                                    UserRole, IdentityUserLogin<int>,
                                                    IdentityRoleClaim<int>, IdentityUserToken<int>>   
    {
         public AgendaContext(DbContextOptions<AgendaContext> options): base (options){}
         
         public DbSet<Agenda> Agendas { get; set; }    
         public DbSet<User> Usuarios { get; set; }    

         protected override void OnModelCreating(ModelBuilder modelBuilder)
         { 
             base.OnModelCreating(modelBuilder);
             
             modelBuilder.Entity<UserRole>(userRole => 
             {
             userRole.HasKey(ur => new {ur.UserId, ur.RoleId});

             userRole.HasOne(ur => ur.Role)
             .WithMany(r => r.UserRoles)
             .HasForeignKey(ur => ur.RoleId)
             .IsRequired();

             userRole.HasOne(ur => ur.User)
             .WithMany(r => r.UserRoles)
             .HasForeignKey(ur => ur.UserId)
             .IsRequired();
             }
             );
         }    
    } 
}