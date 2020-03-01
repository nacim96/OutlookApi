using apiOutlook.Models;
using Microsoft.EntityFrameworkCore;

namespace apiOutlook.ContextDB
{

    public class apiOutlookContext : DbContext
    {
        public apiOutlookContext(DbContextOptions<apiOutlookContext> options)
            : base(options)
        {
        }

 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {}

         //one to many
     /* modelBuilder.Entity<Attachment>()
            .HasOne<MessageModel>(s => s.MessageModel)
            .WithMany(g => g.Attachments).            
            HasForeignKey(e => e.IdA);}
*/
  // Configure Student & StudentAddress entity
  //one to one 
   /* modelBuilder.Entity<Etudiant>()
                .HasOptional(s => s.Address) // Mark Address property optional in Student entity
                .WithRequired(ad => ad.Student); // mark Student property as required in StudentAddress entity. Cannot save StudentAddress without Student
*/

/* many to many 

  modelBuilder.Entity<Student>()
                .HasMany<Course>(s => s.Courses)
                .WithMany(c => c.Students)
                .Map(cs =>
                        {
                            cs.MapLeftKey("StudentRefId");
                            cs.MapRightKey("CourseRefId");
                            cs.ToTable("StudentCourse");
                        });*/

   // public DbSet<Grade> Grades { get; set; }


        //instance liée à la classe TodoItems(on peut gérer le BD à l'aide de cette instance)
        public DbSet<MessageModel> MessagesapiOutlook { get; set; }
      public DbSet<Attachment> Attachmentss { get; set; }

      //  public DbSet<EmployeeAddress> EmployeeAddress { get; set; }

       //instance liée à la classe Etudiant(on peut gérer le BD à l'aide de cette instance)
      // public DbSet<Etudiant> Etudiant {get;set;}

        
    }
}